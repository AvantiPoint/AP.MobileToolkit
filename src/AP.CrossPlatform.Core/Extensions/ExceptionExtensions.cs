using System;
using System.Linq;

namespace AP.CrossPlatform.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToErrorMessage(this Exception ex)
        {
            var root = GetRootException(ex);
            var message = root.Message;
            if (root.GetType().Name != nameof(Exception))
            {
                return $"{root.GetType().Name}: {message}";
            }
            else if (ex.GetType().Name != nameof(ex))
            {
                return $"{ex.GetType().Name}: {message}";
            }

            return message;
        }

        private static Exception GetRootException(Exception ex)
        {
            switch (ex)
            {
                case AggregateException ae:
                    return GetRootException(ae.InnerExceptions.First());
                default:
                    if (ex.InnerException is null)
                        return ex;
                    return GetRootException(ex.InnerException);
            }
        }
    }
}
