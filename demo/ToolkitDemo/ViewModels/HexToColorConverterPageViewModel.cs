using System.Collections.Generic;
using AP.CrossPlatform.Collections;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace ToolkitDemo.ViewModels
{
    public class HexToColorConverterPageViewModel : DemoPageViewModelBase
    {
        public HexToColorConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            HexCodeList = new ObservableRangeCollection<string>()
            {
                "#00FF00",
                "#008000",
                "#00FFFF"
            };
        }

        public IEnumerable<string> HexCodeList { get; }

        public string HexCode
        {
            get => _hexCode;
            set => this.RaiseAndSetIfChanged(ref _hexCode, value);
        }

        private string _hexCode;
    }
}
