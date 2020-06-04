using AP.MobileToolkit.Mvvm;
using ReactiveUI;

namespace ToolkitDemo.ViewModels
{
    public class GravatarPageViewModel : APBaseViewModel
    {
        public GravatarPageViewModel(BaseServices baseServices)
            : base(baseServices)
        {
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }
    }
}
