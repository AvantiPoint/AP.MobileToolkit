using System;
using System.Collections.Generic;

namespace AP.CrossPlatform.Auth
{
    public interface IUser : IDictionary<string, string>
    {
        string AccessToken { get; }
        string Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        DateTime? IssuedAt { get; }
        DateTime? AuthTime { get; }
        DateTime? Expiration { get; }
        DateTime? NotBefore { get;}
        bool IsNew { get; }

        void Update(string jwt);
    }
}