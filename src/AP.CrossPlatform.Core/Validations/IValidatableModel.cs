using AP.CrossPlatform.Collections;

namespace AP.CrossPlatform.Validations
{
    public interface IValidatableModel
    {
        bool IsValid { get; set; }

        ObservableErrorCollection Errors { get; set; }
    }
}
