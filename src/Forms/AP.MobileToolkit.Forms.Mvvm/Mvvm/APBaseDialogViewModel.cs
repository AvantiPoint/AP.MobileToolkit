using System;
using System.Reactive.Disposables;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services.Dialogs;

namespace AP.MobileToolkit.Mvvm
{
    public abstract class APBaseDialogViewModel : IAutoInitialize, IDialogAware, IDestructible
    {
        protected CompositeDisposable Disposables { get; private set; }

        protected APBaseDialogViewModel()
        {
            Disposables = new CompositeDisposable();
        }

        public event Action<IDialogParameters> RequestClose;

        public virtual bool CanCloseDialog() => true;

        public virtual void OnDialogClosed()
        {
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
        }

        protected void RaiseRequestClose(IDialogParameters parameters = null) =>
            RequestClose?.Invoke(parameters ?? new DialogParameters());

        protected virtual void Destroy()
        {
        }

        void IDestructible.Destroy()
        {
            Destroy();
            Disposables.Dispose();
            Disposables = null;
        }
    }
}
