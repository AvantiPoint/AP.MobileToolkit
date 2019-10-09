using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AP.CrossPlatform.Collections;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ToolkitDemo.Models;
using ToolkitDemo.Services;
using Xamarin.Essentials.Interfaces;

namespace ToolkitDemo.ViewModels
{
    public class ShowCodePageViewModel : ReactiveViewModelBase
    {
        protected ICodeSampleResolver CodeSampleResolver { get; set; }
        protected IClipboard Clipboard { get; set; }

        public string _pageName;
        public string PageName
        {
            get => _pageName;
            set => this.RaiseAndSetIfChanged(ref _pageName, value);
        }

        public string _resourceContent;
        public string ResourceContent
        {
            get => _resourceContent;
            set => this.RaiseAndSetIfChanged(ref _resourceContent, value);
        }

        public ObservableRangeCollection<SelectableItem> FileList { get; } = new ObservableRangeCollection<SelectableItem>();

        public DelegateCommand<string> TapCommand { get; }
        public DelegateCommand CopyTextToClipboardCommand { get; }

        public ShowCodePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger, ICodeSampleResolver codeSampleResolver, IClipboard clipboard)
            : base(navigationService, pageDialogService, logger)
        {
            CodeSampleResolver = codeSampleResolver;
            Clipboard = clipboard;

            TapCommand = new DelegateCommand<string>(OnTapCommandExecuted);
            CopyTextToClipboardCommand = new DelegateCommand(OnCopyTextToClipboardCommandExecuted);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnNavigatedTo(INavigationParameters parameters)
        {
            PageName = parameters.GetValue<string>("page_name");
            IEnumerable<string> pageFileNames = CodeSampleResolver.GetPageFilesName(PageName);

            foreach (var filename in pageFileNames)
            {
                var item = new SelectableItem();

                if (FileList.Count == 0)
                {
                    item.IsSelected = true;
                }

                item.Text = filename;
                FileList.Add(item);
            }

            if (FileList.Count() > 0)
            {
                ResourceContent = CodeSampleResolver.ReadEmbeddedResource(FileList.First(x => x.IsSelected == true).Text);
            }
        }

        private void OnTapCommandExecuted(string filename)
        {
            FileList.ToList().ForEach(c => c.IsSelected = false);
            FileList.FirstOrDefault(f => f.Text.Equals(filename)).IsSelected = true;
            ResourceContent = CodeSampleResolver.ReadEmbeddedResource(filename);
        }

        private async void OnCopyTextToClipboardCommandExecuted()
        {
            await Clipboard.SetTextAsync(ResourceContent);
        }
    }
}
