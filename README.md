![PocketSharp](https://raw.github.com/ceee/PocketSharp/master/PocketSharp.Website/Assets/Images/github-header.png)

**PocketSharp** is a C#.NET portable class library that integrates the [Pocket API v3](http://getpocket.com/developer).

## Install using [NuGet](https://www.nuget.org/packages/PocketSharp/)

```
Install-Package PocketSharp
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

PocketSharp is a **Portable Class Library** (since 1.0.0), therefore it's compatible with multiple platforms:

- **.NET** >= 4.5 (including WPF)
- **Silverlight** >= 4
- **Windows Phone** >= 7.5
- **Windows Store**

You can find examples for Silverlight 5, WP8 and WPF in the `PocketSharp.Examples` ([@github](https://github.com/ceee/PocketSharp/tree/master/PocketSharp.Examples)) folder.

## What's next?

- `cancellationToken` support for all methods
- support HTML injection into content from PocketArticle (maybe remove title from Article)
- benchmarking download of large lists
- benchmarking of search algorithm
- Update website and merge into plugin repository site

## Dependencies

- [Microsoft.Bcl.Async](https://www.nuget.org/packages/Microsoft.Bcl.Async/)
- [Microsoft.Net.Http](https://www.nuget.org/packages/Microsoft.Net.Http/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
- [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged)

## Forked Dependencies

- [NReadability](https://github.com/marek-stoj/NReadability) - converted to a PCL with minor adaptations
- [SgmlReader](https://github.com/MindTouch/SGMLReader) - converted to a PCL

## Contributors
| [![ceee](http://gravatar.com/avatar/9c61b1f4307425f12f05d3adb930ba66?s=70)](https://github.com/ceee "Tobias Klika") | [![ScottIsAFool](http://gravatar.com/avatar/6df656872a87b09a7470feb4867ed927?s=70)](https://github.com/ScottIsAFool "Scott Lovegrove") |
|---|---|
| [ceee](https://github.com/ceee) | [ScottIsAFool](https://github.com/ScottIsAFool) |

## License

[MIT License](https://github.com/ceee/PocketSharp/blob/master/LICENSE-MIT)
