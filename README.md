![PocketSharp](https://raw.github.com/ceee/PocketSharp/master/Assets/github-header.png)

**PocketSharp** is a .NET Standard library that integrates the [Pocket API v3](http://getpocket.com/developer).

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

## Dependencies

- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)

## Contributors

| [![ceee](http://gravatar.com/avatar/9c61b1f4307425f12f05d3adb930ba66?s=70)](https://github.com/ceee "Tobias Klika") | [![ScottIsAFool](http://gravatar.com/avatar/6df656872a87b09a7470feb4867ed927?s=70)](https://github.com/ScottIsAFool "Scott Lovegrove") |
|---|---|
| [ceee](https://github.com/ceee) | [ScottIsAFool](https://github.com/ScottIsAFool) |

## License

[MIT License](https://github.com/ceee/PocketSharp/blob/master/LICENSE-MIT)
