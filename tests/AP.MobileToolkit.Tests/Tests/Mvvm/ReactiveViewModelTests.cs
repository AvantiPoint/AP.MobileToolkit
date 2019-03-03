using AP.MobileToolkit.Tests.Mocks;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Mvvm
{
    public class ReactiveViewModelTests : TestBase
    {
        public ReactiveViewModelTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void TitleIsSetToTypeName()
        {
            var vm = new ReactiveViewModelMock();
            _testOutputHelper.WriteLine(vm.Title);
            Assert.Equal(nameof(ViewModelMock), vm.Title);
        }

        [Fact]
        public void TitleIsSetToSanitizedName()
        {
            var vm = new ReactiveViewModelMock();
            _testOutputHelper.WriteLine(vm.Title);
            Assert.Equal("Mock", vm.Title);
        }

        [Fact]
        public void InitialIsBusy_IsFalse()
        {
            var vm = new ReactiveViewModelMock();
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void InitialIsNotBusy_IsTrue()
        {
            var vm = new ReactiveViewModelMock();
            Assert.True(vm.IsNotBusy);
        }

        [Fact]
        public void SetIsBusy_UpdatesIsNotBusy()
        {
            var vm = new ReactiveViewModelMock();

            vm.ToggleIsBusyCommand.Execute();
            Assert.True(vm.IsBusy);
            Assert.False(vm.IsNotBusy);
        }

        [Fact]
        public async Task ValidNavigation_DoesNotUsePageDialogService()
        {
            var navService = new NavigationServiceMock();
            var dialogService = new PageDialogServiceMock();
            var logger = new XunitLogger(_testOutputHelper);
            var vm = new ReactiveViewModelMock(navService, dialogService, logger);

            //Assert.True(vm.NavigateCommand.CanExecute("good"));            
            await vm.NavigateCommand.Execute("good");
            Assert.Null(dialogService.Title);
            Assert.Null(dialogService.Message);
        }

        [Fact]
        public async Task InvalidNavigation_UsesPageDialogService()
        {
            var navService = new NavigationServiceMock();
            var dialogService = new PageDialogServiceMock();
            var logger = new XunitLogger(_testOutputHelper);
            var vm = new ReactiveViewModelMock(navService, dialogService, logger);

            //Assert.True(vm.NavigateCommand.CanExecute("bad"));            
            await vm.NavigateCommand.Execute("bad");

            Assert.Equal(nameof(Exception), dialogService.Title);
            Assert.Equal("bad", dialogService.Message);
        }

        [Fact]
        public async Task NavigateCommand_CanNotExecute_WhenIsBusy()
        {
            var vm = new ReactiveViewModelMock();
            await vm.ToggleIsBusyCommand.Execute();
            Assert.False(await vm.NavigateCommand.CanExecute);
            await vm.ToggleIsBusyCommand.Execute();
            Assert.True(await vm.NavigateCommand.CanExecute);
        }
    }
}
