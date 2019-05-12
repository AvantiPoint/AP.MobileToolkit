using System;
using System.Threading.Tasks;
using AP.MobileToolkit.Resources;
using AP.MobileToolkit.Tests.Mocks;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Mvvm
{
    public class ViewModelBaseFixture : TestBase
    {
        public ViewModelBaseFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void TitleIsSetToTypeName()
        {
            var vm = new ViewModelMock();
            TestOutputHelper.WriteLine(vm.Title);
            Assert.Equal(nameof(ViewModelMock), vm.Title);
        }

        [Fact]
        public void TitleIsSetToSanitizedName()
        {
            var vm = new MockViewModel();
            TestOutputHelper.WriteLine(vm.Title);
            Assert.Equal("Mock", vm.Title);
        }

        [Fact]
        public void InitialIsBusy_IsFalse()
        {
            var vm = new ViewModelMock();
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void InitialIsNotBusy_IsTrue()
        {
            var vm = new ViewModelMock();
            Assert.True(vm.IsNotBusy);
        }

        [Fact]
        public void SetIsBusy_UpdatesIsNotBusy()
        {
            var vm = new ViewModelMock
            {
                IsBusy = true
            };

            Assert.True(vm.IsBusy);
            Assert.False(vm.IsNotBusy);
        }

        [Fact]
        public void SetIsNotBusy_UpdatesIsBusy()
        {
            var vm = new ViewModelMock
            {
                IsNotBusy = false
            };

            Assert.True(vm.IsBusy);
            Assert.False(vm.IsNotBusy);
        }

        [Fact]
        public async Task ValidNavigation_DoesNotUsePageDialogService()
        {
            var navService = new NavigationServiceMock();
            var dialogService = new PageDialogServiceMock();
            var logger = new XunitLogger(TestOutputHelper);
            var vm = new ViewModelMock(navService, dialogService, logger);

            Assert.True(vm.NavigateCommand.CanExecute("good"));
            vm.NavigateCommand.Execute("good");
            await Task.Run(() =>
            {
                while (vm.IsBusy)
                {
                }
            });
            Assert.Null(dialogService.Title);
            Assert.Null(dialogService.Message);
        }

        [Fact]
        public async Task InvalidNavigation_UsesPageDialogService()
        {
            var navService = new NavigationServiceMock();
            var dialogService = new PageDialogServiceMock();
            var logger = new XunitLogger(TestOutputHelper);
            var vm = new ViewModelMock(navService, dialogService, logger);

            Assert.True(vm.NavigateCommand.CanExecute("bad"));
            vm.NavigateCommand.Execute("bad");
            await Task.Run(() =>
            {
                while (vm.IsBusy)
                {
                }
            });

            Assert.Equal(ToolkitResources.Error, dialogService.Title);
            var dialogMessage = string.Format(ToolkitResources.AlertErrorMessageTemplate, nameof(Exception), vm.CorrelationId);
            Assert.NotNull(vm.CorrelationId);
            Assert.Contains(dialogMessage, dialogService.Message);
        }

        [Fact]
        public void NavigateCommand_CanNotExecute_WhenIsBusy()
        {
            var vm = new ViewModelMock
            {
                IsBusy = true
            };

            Assert.False(vm.NavigateCommand.CanExecute("good"));
            vm.IsBusy = false;
            Assert.True(vm.NavigateCommand.CanExecute("good"));
        }
    }
}
