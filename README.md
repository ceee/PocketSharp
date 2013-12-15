![PocketSharp](https://raw.github.com/ceee/PocketSharp/master/Assets/github-header.png)

**PocketSharp** is a C#.NET portable class library that integrates the [Pocket API v3](http://getpocket.com/developer).

## Install [PocketSharp](https://www.nuget.org/packages/PocketSharp/) using NuGet

```
Install-Package PocketSharp
```

for the [PocketSharp.Reader](https://www.nuget.org/packages/PocketSharp.Reader/)

```
Install-Package PocketSharp.Reader
```

## Documentation

See [wiki](https://github.com/ceee/PocketSharp/wiki)

---

## Usage Example:

A search for items containing `CSS`:

```csharp
PocketClient client = new PocketClient("[YOUR_CONSUMER_KEY]", "[YOUR_ACCESS_CODE]");

List<PocketItem> items = await client.Search("css");

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

PocketSharp is a **Portable Class Library**, therefore it's compatible with multiple platforms:

- **.NET** >= 4.5 (including WPF)
- **Silverlight** >= 4
- **Windows Phone** >= 7.5
- **Windows Store**

You can find examples for Silverlight 5, WP8 and WPF in the `PocketSharp.Examples` ([@github](https://github.com/ceee/PocketSharp/tree/master/PocketSharp.Examples)) folder.

## What's new in the upcoming PocketSharp v3.0?

- [x] `cancellationToken` support for all methods
- [x] support HTML injection into content from PocketArticle (maybe remove title from Article)
- [x] make setters for inline objects in PocketItem (images, videos, ...)
- [x] IPocketClient interface
- [x] PreRequest callback allows injection of `Action<string>` before every request
- [x] Submit multiple actions in one request
- [x] Split PocketReader into own NuGet package
- [x] bugfixes, for sure!

## Dependencies

- [Microsoft.Bcl.Async](https://www.nuget.org/packages/Microsoft.Bcl.Async/) _(used in PocketSharp & PocketSharp.Reader)_
- [Microsoft.Net.Http](https://www.nuget.org/packages/Microsoft.Net.Http/) _(used in PocketSharp & PocketSharp.Reader)_
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/) _(only for PocketSharp project)_
- [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) _(only for PocketSharp project)_

## Forked Dependencies

_used in PocketSharp.Reader_

- [NReadability](https://github.com/marek-stoj/NReadability) - converted to a PCL with minor adaptations
- [SgmlReader](https://github.com/MindTouch/SGMLReader) - converted to a PCL

## Contributors
| [![ceee](http://gravatar.com/avatar/9c61b1f4307425f12f05d3adb930ba66?s=70)](https://github.com/ceee "Tobias Klika") | [![ScottIsAFool](http://gravatar.com/avatar/6df656872a87b09a7470feb4867ed927?s=70)](https://github.com/ScottIsAFool "Scott Lovegrove") |
|---|---|
| [ceee](https://github.com/ceee) | [ScottIsAFool](https://github.com/ScottIsAFool) |

## License

[MIT License](https://github.com/ceee/PocketSharp/blob/master/LICENSE-MIT)
