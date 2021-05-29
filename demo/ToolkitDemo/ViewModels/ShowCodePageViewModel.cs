using System.Collections.Generic;
using System.Linq;
using AP.CrossPlatform.Collections;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Navigation;
using ReactiveUI;
using ToolkitDemo.Models;
using ToolkitDemo.Services;
using Xamarin.Essentials.Interfaces;

namespace ToolkitDemo.ViewModels
{
    public class ShowCodePageViewModel : APBaseViewModel
    {
        private ICodeSampleResolver CodeSampleResolver { get; }

        private IClipboard Clipboard { get; }

        public ShowCodePageViewModel(BaseServices baseServices, ICodeSampleResolver codeSampleResolver, IClipboard clipboard)
            : base(baseServices)
        {
            CodeSampleResolver = codeSampleResolver;
            Clipboard = clipboard;

            TapCommand = new DelegateCommand<string>(OnTapCommandExecuted);
            CopyTextToClipboardCommand = new DelegateCommand(OnCopyTextToClipboardCommandExecuted);
        }

        public ObservableRangeCollection<SelectableItem> FileList { get; } = new ObservableRangeCollection<SelectableItem>();

        public string PageName
        {
            get => _pageName;
            set => this.RaiseAndSetIfChanged(ref _pageName, value);
        }
        private string _pageName;

        public string ResourceContent
        {
            get => _resourceContent;
            set => this.RaiseAndSetIfChanged(ref _resourceContent, value);
        }
        private string _resourceContent;

        public DelegateCommand<string> TapCommand { get; }

        public DelegateCommand CopyTextToClipboardCommand { get; }

        protected override void Initialize(INavigationParameters parameters)
        {
            if (!parameters.TryGetValue<string>("page_name", out var pageName))
            {
                throw new System.Exception("Parameter page_name was not passed in the navigation parameters.");
            }

            PageName = pageName;

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
