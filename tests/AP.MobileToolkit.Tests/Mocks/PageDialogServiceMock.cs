using System;
using System.Threading.Tasks;
using Prism.AppModel;
using Prism.Services;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class PageDialogServiceMock : IPageDialogService
    {
        public string Title { get; private set; }

        public string Message { get; private set; }

        public Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, params string[] otherButtons)
        {
            throw new NotImplementedException();
        }

        public Task DisplayActionSheetAsync(string title, params IActionSheetButton[] buttons)
        {
            throw new NotImplementedException();
        }

        public Task<string> DisplayActionSheetAsync(string title, string cancelButton, string destroyButton, FlowDirection flowDirection, params string[] otherButtons)
        {
            throw new NotImplementedException();
        }

        public Task DisplayActionSheetAsync(string title, FlowDirection flowDirection, params IActionSheetButton[] buttons)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton)
        {
            throw new NotImplementedException();
        }

        public Task DisplayAlertAsync(string title, string message, string cancelButton)
        {
            Title = title;
            Message = message;
            return Task.CompletedTask;
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string acceptButton, string cancelButton, FlowDirection flowDirection)
        {
            throw new NotImplementedException();
        }

        public Task DisplayAlertAsync(string title, string message, string cancelButton, FlowDirection flowDirection)
        {
            throw new NotImplementedException();
        }

        public Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, KeyboardType keyboardType = KeyboardType.Default, string initialValue = "")
        {
            throw new NotImplementedException();
        }
    }
}
