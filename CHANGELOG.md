### 3.0.0 (2013-12-15)

- `cancellationToken` support for all methods
- Toggle inclusion of title in PocketArticle.Content
- Make setters for inline objects in PocketItem (images, videos, ...)
- IPocketClient interface
- PreRequest callback allows injection of `Action<string>` before every request
- `lastHeaders` and `lastRequestData` in PocketClient for better debugging
- Submit multiple actions in one request with `SendActions`
- New `IsVideo`, `IsImage` and `ResolvedId` as new properties in PocketItem
- Split PocketReader into own NuGet package
- A lot of bugfixes

### 2.2.2 (2013-11-01)

- Password validation in RegisterAccount

### 2.2.1 (2013-10-27)

- Fix NReadability parsing issue

### 2.2.0 (2013-10-26)

- Made `GetAccessCode` obsolete
- Return username after authentication with `GetUser`

### 2.1.0 (2013-10-25)

- Rename `Statistics()` to `GetUserStatistics()`
- Made `CallbackUri` public
- Added Fody/PropertyChanged
- Method `GetUsageLimits()` to retrieve API usage limits
- Add PORTABLE constant to SgmlReader 

### 2.0.1 (2013-10-19)

- Catch HttpRequestExceptions and provide as InnerException for PocketException
- Read Article from URI (method overload)

### 2.0.0 (2013-10-17)

- Add Reader API _(does not use the official Article View API, which is private. The PocketReader is based on a custom PCL port of NReadability and SgmlReader)_

### 1.5.1 (2013-09-30) 

- `RetrieveFilter.All` didn't work
- improve search speed

### 1.5.0 (2013-09-28) 

- add statistics API
- add registration API

### 1.4.0 (2013-09-21) 

- rename `Retrieve` to `Get`
- update IntelliSense documentation
- add `GetTags` method

### 1.3.0 (2013-09-19) 

- get Item by ID 
- tag modification bugfixes

### 1.2.1 (2013-09-18) 

- correct parameter conversion for DateTime and Boolean

### 1.2.0 (2013-09-17) 

- simplified retrieve methods

### 1.1.0 (2013-09-17) 

- fix modification requests

### 1.0.0 (2013-09-15) 

- convert to PCL 
- implement async

### 0.3.2 (2013-08-16) 

- tag modification fixed
- full retrieval of items for Retrieve method

### 0.3.1 (2013-07-07) 

- authentication fixes

### 0.3.0 (2013-07-02) 

- update authentication process 

### 0.2.0 (2013-06-27) 

- add, modify item
- modify tags

### 0.1.0 (2013-06-26) 

- authentication 
- retrieve functionality
