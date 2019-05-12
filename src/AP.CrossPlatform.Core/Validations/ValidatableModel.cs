using AP.CrossPlatform.Collections;
using Newtonsoft.Json;

namespace AP.CrossPlatform.Validations
{
    public class ValidatableModel : ObservableObject, IValidatableModel
    {
        private bool _isValid;

        [JsonIgnore]
        public bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        private ObservableErrorCollection _errors = new ObservableErrorCollection();

        [JsonIgnore]
        public ObservableErrorCollection Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }
    }
}
