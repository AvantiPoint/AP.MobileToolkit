using AP.MobileToolkit.Mvvm;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class MockViewModel : APBaseViewModel
    {
        public MockViewModel()
            : this(new BaseServices(default, default, default, default, default, default))
        {
        }

        public MockViewModel(BaseServices baseServices)
            : base(baseServices)
        {
        }
    }
}
