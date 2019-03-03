using AP.MobileToolkit.Authentication;
using Prism.Events;

namespace AP.MobileToolkit.Events
{
    public class AuthenticatedUserEvent : PubSubEvent<IUser>
    {
    }
}
