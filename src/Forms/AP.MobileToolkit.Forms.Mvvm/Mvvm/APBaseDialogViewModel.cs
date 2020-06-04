using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ReactiveUI;

namespace AP.MobileToolkit.Mvvm
{
    public abstract class APBaseDialogViewModel : ReactiveObject, IAutoInitialize, IDialogAware, IDestructible
    {
        protected CompositeDisposable Disposables { get; private set; }

        protected APBaseDialogViewModel()
        {
            Disposables = new CompositeDisposable();
            DismissCommand = ReactiveCommand.Create(DismissCommandExecuted, this.WhenAnyValue(x => x.CanClose))
                .DisposeWith(Disposables);
        }

        public event Action<IDialogParameters> RequestClose;

        private bool _canClose;
        public bool CanClose
        {
            get => _canClose;
            set => this.RaiseAndSetIfChanged(ref _canClose, value);
        }

        public ReactiveCommand<Unit, Unit> DismissCommand { get; }

        public bool CanCloseDialog() => CanClose;

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

        private void DismissCommandExecuted() =>
            RaiseRequestClose();
    }
}
