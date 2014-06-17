![PocketSharp](https://raw.github.com/ceee/PocketSharp/master/Assets/github-header.png)

**PocketSharp** is a C#.NET portable class library that integrates the [Pocket API v3](http://getpocket.com/developer).

## Install PocketSharp using [NuGet](https://www.nuget.org/packages/PocketSharp/)

```
Install-Package PocketSharp
```


## Documentation

See [wiki](https://github.com/ceee/PocketSharp/wiki)

## Where's the Article View API?

You can either use the open source [ReadSharp](https://github.com/ceee/ReadSharp) parser or if you want to use the official API by Pocket, you have to request access to it.<br>
Afterwards you can use the access information to query the endpoint with PocketSharp. Instructions [here](https://github.com/ceee/PocketSharp/wiki/Article-parser).


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

PocketSharp is a **Portable Class Library**, therefore it's compatible with multiple platforms and Universal Apps:

- **.NET** >= 4.5 (including WPF)
- **Windows Phone** (Silverlight + WinPRT) >= 8
- **Windows Store** >= 8
- **Xamarin** iOS + Android
- _WP7 and Silverlight are dropped in 4.0, use PocketSharp < 4.0, if you want to support them_

You can find examples for WP8 and WPF in the `PocketSharp.Examples` ([@github](https://github.com/ceee/PocketSharp/tree/master/PocketSharp.Examples)) folder.

## Dependencies

- [Microsoft.Bcl.Async](https://www.nuget.org/packages/Microsoft.Bcl.Async/)
- [Microsoft.Net.Http](https://www.nuget.org/packages/Microsoft.Net.Http/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
- [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged)

## Contributors

| [![ceee](http://gravatar.com/avatar/9c61b1f4307425f12f05d3adb930ba66?s=70)](https://github.com/ceee "Tobias Klika") | [![ScottIsAFool](http://gravatar.com/avatar/6df656872a87b09a7470feb4867ed927?s=70)](https://github.com/ScottIsAFool "Scott Lovegrove") | [![StephenErstad](http://gravatar.com/avatar/3cfe2c5dbc5bc26697e0fe2c428e46e7?s=70)](https://github.com/StephenErstad "Stephen Erstad") |
|---|---|---|
| [ceee](https://github.com/ceee) | [ScottIsAFool](https://github.com/ScottIsAFool) | [StephenErstad](https://github.com/StephenErstad) |

## License

[MIT License](https://github.com/ceee/PocketSharp/blob/master/LICENSE-MIT)
