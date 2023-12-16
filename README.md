# NuGetGetter

### What is this

NuGetGetter is used to get information about various packages. Simply provide a NuGet Package name and the information about it will be returned to you

### Getting Started
*** NOTE THIS IS A POC - DO NOT USE IN PRODUCTION WORK LOADS ***

Currently this has not been tested as a container. To utilize this API you will need to clone the Repo and start it up in your IDE.

### Sample request
```System.Runtime.Caching```

### Sample Response
```
{
  "packageName": "System.Runtime.Caching",
  "versionInfos": [
    ...
    {
      "version": "7.0.0",
      "frameworkInfo": [
        {
          "product": ".NET",
          "frameworks_Compatible": [
            "net6.0",
            "net7.0"
          ]
        },
        {
          "product": ".NET Core",
          "frameworks_Compatible": []
        },
        {
          "product": ".NET Standard",
          "frameworks_Compatible": [
            "netstandard2.0"
          ]
        },
        {
          "product": ".NET Framework",
          "frameworks_Compatible": [
            "net462"
          ]
        }
      ]
    },
    ...
  ]
}
```

### Get in Touch
[GitHub](https://github.com/JFreeborn)
