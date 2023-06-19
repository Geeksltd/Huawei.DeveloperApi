# Huawei.DeveloperApi

This is a simple C# library you can use to communicate with Huawei's Developer APIs with ease.

## Registration

To use this library, you need to add its dependencies using `AddHuaweiDeveloperApi` method. First of all, add the required using statement.

```c#
using Huawei.DeveloperApi;
```

Then make a call to `AddHuaweiDeveloperApi` extension method.

```c#
public override void ConfigureServices(IServiceCollection services)
{
    services.AddHuaweiDeveloperApi(Configuration);
}
```

This method will register an instance of the following classes.

`HuaweiOptions`: This is the options used in the library.

`HuaweiDeveloperService`: You will use this to call any of the provided endpoints.

`HuaweiInMemoryTokenStorage`: This is the default implementation of `IHuaweiTokenStorage`, which stores the authorization details in-memory. By using this, you'll have to re-authorize your app against Cafe Bazaar.

## Configuration

Here is the required properties:

```json
{
  "Huawei": {
    "ClientId": "<YOUR_CLIENT_ID>",
    "ClientSecret": "<YOUR_CLIENT_SECRET>"
  }
}
```

## Validate a subscription

You can use `ValidateSubscription` to validate a subscription.

```c#
[HttpGet("subscription/{subscriptionId}/{purchaseToken}")]
public async Task<IActionResult> Validate(string subscriptionId, string purchaseToken)
{
    var result = await _developerService.ValidateSubscription(new HuaweiValidateSubscriptionRequest
    {
        SubscriptionId = subscriptionId,
        PurchaseToken = purchaseToken
    });

    return new JsonResult(result);
}
```
