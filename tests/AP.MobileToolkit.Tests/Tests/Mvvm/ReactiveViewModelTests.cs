﻿using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AP.CrossPlatform.Extensions;
using AP.CrossPlatform.i18n;
using AP.MobileToolkit.Mvvm;
using AP.MobileToolkit.Resources;
using AP.MobileToolkit.Tests.Mocks;
using Humanizer;
using Moq;
using Prism.Events;
using Prism.Services;
using Prism.Services.Dialogs;
using ReactiveUI;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Mvvm
{
    public class ReactiveViewModelTests : TestBase, IObserver<Exception>
    {
        public ReactiveViewModelTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            RxApp.DefaultExceptionHandler = this;
        }

        [Fact]
        public void InitializesWithoutException()
        {
            var ex = Record.Exception(() => new ReactiveMockViewModel(TestOutputHelper));
            Assert.Null(ex);
        }

        [Fact]
        public void TitleDoesNotContainPage()
        {
            var vm = new ReactivePageViewModel(TestOutputHelper);
            TestOutputHelper.WriteLine(vm.Title);
            Assert.Equal("Reactive", vm.Title);
        }

        [Fact]
        public void TitleIsSetToTypeName()
        {
            var vm = new ReactiveViewModelMock(TestOutputHelper);
            TestOutputHelper.WriteLine(vm.Title);
            Assert.Equal(nameof(ReactiveViewModelMock).Humanize(LetterCasing.Title), vm.Title);
        }

        [Fact]
        public void TitleIsSetToSanitizedName()
        {
            var vm = new ReactiveMockViewModel(TestOutputHelper);
            TestOutputHelper.WriteLine(vm.Title);
            Assert.Equal("Reactive Mock", vm.Title);
        }

        [Fact]
        public void InitialIsBusy_IsFalse()
        {
            var vm = new ReactiveMockViewModel(TestOutputHelper);
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void InitialIsNotBusy_IsTrue()
        {
            var vm = new ReactiveMockViewModel(TestOutputHelper);
            Assert.True(vm.IsNotBusy);
        }

        [Fact]
        public async Task SetIsBusy_UpdatesIsNotBusy()
        {
            var vm = new ReactiveMockViewModel(TestOutputHelper);
            Assert.False(vm.IsToggled);
            await vm.ToggleIsBusyCommand.Execute();
            Assert.True(vm.IsToggled);
            Assert.True(vm.IsBusy);
            Assert.False(vm.IsNotBusy);

            await vm.ToggleIsBusyCommand.Execute();
            Assert.False(vm.IsToggled);
            Assert.False(vm.IsBusy);
            Assert.True(vm.IsNotBusy);
        }

        [Fact]
        public async Task ValidNavigation_DoesNotUsePageDialogService()
        {
            var navService = new NavigationServiceMock();
            var dialogService = new PageDialogServiceMock();
            var logger = new XunitLogger(TestOutputHelper);
            var baseServices = new BaseServices(navService, Mock.Of<IDialogService>(), dialogService, logger, Mock.Of<IEventAggregator>(), Mock.Of<IDeviceService>(), new ResxLocalize());
            var vm = new ReactiveMockViewModel(baseServices);

            // Assert.True(vm.NavigateCommand.CanExecute("good"));
            await vm.NavigateCommand.Execute("good");
            Assert.Null(dialogService.Title);
            Assert.Null(dialogService.Message);
        }

        [Fact]
        public async Task InvalidNavigation_UsesPageDialogService()
        {
            var navService = new NavigationServiceMock();
            var dialogService = new PageDialogServiceMock();
            var logger = new XunitLogger(TestOutputHelper);
            var baseServices = new BaseServices(navService, Mock.Of<IDialogService>(), dialogService, logger, Mock.Of<IEventAggregator>(), Mock.Of<IDeviceService>(), new ResxLocalize());
            var vm = new ReactiveMockViewModel(baseServices);

            // Assert.True(vm.NavigateCommand.CanExecute("bad"));
            await vm.NavigateCommand.Execute("bad");

            Assert.Equal(ToolkitResources.Error, dialogService.Title);
            var errorMessage = new Exception("bad").ToErrorMessage();
            var dialogMessage = string.Format(ToolkitResources.AlertErrorMessageTemplate, errorMessage, vm.CorrelationId);
            Assert.NotNull(vm.CorrelationId);
            Assert.Contains(dialogMessage, dialogService.Message);
        }

        // [Fact]
        // public async Task NavigateCommand_CanNotExecute_WhenIsBusy()
        // {
        //    var vm = new ReactiveViewModelMock();
        //    await vm.ToggleIsBusyCommand.Execute();
        //    Assert.False(await vm.NavigateCommand.CanExecute);
        //    await vm.ToggleIsBusyCommand.Execute();
        //    Assert.True(await vm.NavigateCommand.CanExecute);
        // }

        void IObserver<Exception>.OnCompleted()
        {
            TestOutputHelper.WriteLine("OnCompleted");
        }

        void IObserver<Exception>.OnError(Exception error)
        {
            TestOutputHelper.WriteLine(error.ToString());
        }

        void IObserver<Exception>.OnNext(Exception value)
        {
            TestOutputHelper.WriteLine(value.ToString());
        }
    }
}
