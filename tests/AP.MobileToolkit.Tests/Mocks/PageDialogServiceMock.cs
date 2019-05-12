using System;
using System.Threading.Tasks;
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
    }
}
