## HttpClient Extensions

The HttpClient Extensions provide a number of useful shortcuts for working with your API.

- GetJObjectAsync: returns the content as a JObject to work with directly.
- GetAsync&lt;T&gt;: returns a deserialized Json Response
- PostJsonObjectAsync: sends a post and serializes the provided object to json
- Adds convienent overloads for Put, Patch, and Delete

## HttpRequestHeaders Extensions

- Add(string, object): calls ToString for you

## JsonContent

Perhaps you've gone to create a custom message for the HttpClient before and noticed there is no easy way to send Json. With the Json Content it's a simple as passing in the object you want to serialize.

## StringExtensions

- string AddQueryStringParameters(this string uri, object queryObject): This will evaluate your given queryObject and add any properties that have a JsonProperty attribute as query parameters to the given uri.