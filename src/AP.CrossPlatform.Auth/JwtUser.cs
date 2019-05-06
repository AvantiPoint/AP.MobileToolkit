using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace AP.CrossPlatform.Auth
{
    public class JwtUser : Dictionary<string, string>, IUser
    {
        public JwtUser()
        {
        }

        public JwtUser(string jwt)
        {
            Update(jwt);
        }

        private string _accessToken;
        public string AccessToken 
        { 
            get => _accessToken;
            protected set => Update(value);
        }

        public virtual string Id => this.GetStringValue("oid");

        public virtual string FirstName => this.GetStringValue("given_name");

        public virtual string LastName => this.GetStringValue("family_name");

        public virtual string Email => this.GetStringArrayValue("emails").FirstOrDefault();

        public DateTime? IssuedAt => this.GetDateTimeValue("iat");

        public DateTime? AuthTime => this.GetDateTimeValue("auth_time");

        public DateTime? Expiration => this.GetDateTimeValue("exp");

        public DateTime? NotBefore => this.GetDateTimeValue("nbf");

        public bool IsNew => this.GetBoolValue("newUser") ?? false;

        public void Update(string jwt)
        {
            _accessToken = jwt;
            foreach (var claim in new JwtSecurityToken(jwt).Claims)
            {
                Add(claim.Type, claim.Value);
            }
        }
    }
}