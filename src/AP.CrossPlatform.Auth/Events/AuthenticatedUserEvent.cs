using Prism.Events;

namespace AP.CrossPlatform.Auth.Events
{
    public class AuthenticatedUserEvent : PubSubEvent<IUser>
    {
    }
}
