using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AP.CrossPlatform.Collections;
using AP.MobileToolkit.Mvvm;
using Prism.AppModel;
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
    public class ShowCodePageViewModel : ReactiveViewModelBase, IAutoInitialize
    {
        private ICodeSampleResolver CodeSampleResolver { get; }
        private IClipboard Clipboard { get; }

        public string _pageName;
        [AutoInitialize("page_name", true)]
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

        protected override void Initialize(INavigationParameters parameters)
        {
            IEnumerable<string> pageFileNames = CodeSampleResolver.GetPageFilesName(PageName);

            int i = 0;
            FileList.AddRange(pageFileNames.Select(x => new SelectableItem
            {
                IsSelected = i++ == 0,
                Text = x
            }));

            if (FileList.Any())
            {
                ResourceContent = CodeSampleResolver.ReadEmbeddedResource(FileList.First(x => x.IsSelected == true).Text);
            }
        }

        private void OnTapCommandExecuted(string filename)
        {
            FileList.ToList().ForEach(f => f.IsSelected = f.Text.Equals(filename));
            ResourceContent = CodeSampleResolver.ReadEmbeddedResource(filename);
        }

        private async void OnCopyTextToClipboardCommandExecuted()
        {
            await Clipboard.SetTextAsync(ResourceContent);
        }
    }
}
