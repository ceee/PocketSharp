![PocketSharp](https://raw.github.com/ceee/PocketSharp/master/PocketSharp.Website/Assets/Images/github-header.png)

**PocketSharp** is a C#.NET portable class library that integrates the [Pocket API v3](http://getpocket.com/developer).

**Website:** [pocketsharp.frontendplay.com](http://pocketsharp.frontendplay.com/)
<br>
**NuGet:** [nuget.org/packages/PocketSharp](https://www.nuget.org/packages/PocketSharp/)


## Install using NuGet

```
Install-Package PocketSharp
```

## Documentation

See [wiki](https://github.com/ceee/PocketSharp/wiki) or [website](http://pocketsharp.frontendplay.com/).

---

## Usage Example:

A search for items containing `CSS`:

```csharp
using PocketSharp;
using PocketSharp.Models;

PocketClient _client = new PocketClient("[YOUR_CONSUMER_KEY]", "[YOUR_ACCESS_CODE]");

var items = await _client.Search("css");

items.ForEach(
	item => Debug.WriteLine(item.ID + " | " + item.Title)
);
```

Which will output:

    330361896 | CSS Front-end Frameworks with comparison : By usabli.ca
    345541438 | Editr - HTML, CSS, JavaScript playground
    251743431 | CSS Architecture
    343693149 | CSS3 Transitions - Thank God We Have A Specification!
	...

---


## Supported platforms

PocketSharp is a **Portable Class Library** (since 1.0.0), therefore it's compatible with multiple platforms:

- **.NET** >= 4.0.3 (including WPF)
- **Silverlight** >= 4
- **Windows Phone** >= 7.5
- **Windows Store**

You can find examples for Silverlight 5, WP8 and WPF in the `PocketSharp.Examples` ([@github](https://github.com/ceee/PocketSharp/tree/master/PocketSharp.Examples)) folder.

## What's next?

- `cancellationToken` support for all methods
- get account statistics on your current usage data (remaining requests, ...)

## Dependencies

- [Microsoft.Bcl.Async](https://www.nuget.org/packages/Microsoft.Bcl.Async/)
- [Microsoft.Net.Http](https://www.nuget.org/packages/Microsoft.Net.Http/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)

## Forked Dependencies

- [NReadability](https://github.com/marek-stoj/NReadability) - converted to a PCL with minor adaptations
- [SgmlReader](https://github.com/MindTouch/SGMLReader) - converted to a PCL

## Contributors
| [![twitter/artistandsocial](http://gravatar.com/avatar/9c61b1f4307425f12f05d3adb930ba66?s=70)](http://twitter.com/artistandsocial "Follow @artistandsocial on Twitter") |
|---|
| [Tobias Klika @ceee](https://github.com/ceee) |

## License

[MIT License](https://github.com/ceee/PocketSharp/blob/master/LICENSE-MIT)
