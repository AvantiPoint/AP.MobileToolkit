# AP.MobileToolkit.AAD

The AAD package provides an easy to use wrapper for the MSAL library and works with both Azure Active Directory and Azure Active Directory B2C.

## Getting Started

To start you'll need to implement some options. The fastest way is to use the IB2COptions or IAADOptions.

```csharp
public class B2COptions : IB2COptions
{
    public string Tenant => Secrets.AADTenant;
    public string ClientId => Secrets.AADClientId;
    public LogLevel? LogLevel => Microsoft.Identity.Client.LogLevel.Error;
}
```

Now that you've added your options you can create Authentication Service by using the `MsalHelper`:

```csharp
var authService = MsalHelper.CreateB2C<B2COptions>();
var authResult = await authService.LoginAsync();
if(authResult.Success)
{
    // Do Something...
}
else
{
    // Do Something Else...
}
```

## Why use the AAD Package

If you've looked at the MSAL Docs... they're confusing to say the least. The AAD Package greatly simplifies what you need to do to successfully use MSAL correctly. It is also built with a proper understanding of best practices to minimize authentications against the AD Tenant which can reduce your costs and improve user performance since you're reducing Http calls.