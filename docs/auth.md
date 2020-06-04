# AP.CrossPlatform.Auth

The Auth package is a lightweight Authentication abstraction layer. This includes a reference to Prism.Core and provides some base events that can be used by the Prism EventAggregator.

This is largely meant to help when dealing with a JWT and you need to be able to see the claims of your user contained in the JWT. Let's assume you had the following JWT:

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwiZ2l2ZW5fbmFt
ZSI6IkpvaG4iLCJmYW1pbHlfbmFtZSI6IkRvZSIsIm9pZCI6IjdkOTU5NGUyLTZmMGUtNDI4NS05N
DkzLWUyYzRlY2Y4OThlMiIsImVtYWlscyI6WyJqb2huLmRvZUBnbWFpbC5jb20iXSwiaWF0IjoxNT
E2MjM5MDIyLCJhdXRoX3RpbWUiOjE1MTYyMzkwMjIsIm5iZiI6MTUxNjIzOTAyMiwibmV3VXNlciI
6ZmFsc2V9.ysgzPbgqvQr4pY-C3PD-wDEI7yd6evWjeGfi8KcvkYQ
```

In the Payload of the JWT we would see the following:

```json
{
  "sub": "1234567890",
  "given_name": "John",
  "family_name": "Doe",
  "oid": "7d9594e2-6f0e-4285-9493-e2c4ecf898e2",
  "emails": [
    "john.doe@gmail.com"
  ],
  "iat": 1516239022,
  "auth_time": 1516239022,
  "nbf": 1516239022,
  "newUser": false
}
```

We can easily parse the JWT and get the user claims provided like:

```csharp
var user = new JwtUser(jwt);
Name = $"{user.FirstName} {user.LastName}";
OnBoardUser = user.IsNew;
```

### More Reading

For more information be sure to check out the Azure Active Directory support via the MSAL library:

- [MSAL Support](aad.md)