# Unwired Commons

Open-source **Commons Features** developed to facilitate the creation of a new project in .Net, providing some commons features.

- 👉 [Nuget Package](https://www.nuget.org/packages/Unwired.Commons) - `nuget page`

<br />

> Something is missing? Submit a new `product feature request` using the [issues tracker](https://github.com/leandrocavalheiro/unwired.commons/issues)..

## ✨ Using the library

> 👉 **Step 1** - Install library into project

- **Package Manager**

```bash
$ Install-Package Unwired.Commons
```

- **.Net CLI**

```bash
$ dotnet add package Unwired.Commons
```

<br />

> 👉 **Step 2** - Register Service
In program.cs, add the call for services.AddUnwiredCommons. This register it's necessary for use Criptography Methods.
```bash
IHost host = Host.CreateDefaultBuilder(args)
                        .ConfigureServices(services =>
                        {
                            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                            services.AddUnwiredCommons();        
                            services.AddHostedService<Worker>();
                        })
                        .Build();
host.Run();
```

> 👉 **Step 3** - Configure your AppSeettings 
Add te group of properties em AppSetting.json file. 
Warning: For Production use Environment Variables and set differentes values for them.
```bash
  "Criptography": {
    "Salt": "dd528dc1-656b-4211-afb0-acd4066deebf",
    "Key": "72743784-74a8-44d4-8b76-437a8c4d1c0c",
    "Interations": 1000
  }
```

> 👉 **Step 4** - Using Criptography Methods
```bash
    private readonly ILogger<Worker> _logger;
    private readonly ICriptographyMethods _criptographyMethods; //private variable for use in object

    public Worker(ILogger<Worker> logger, ICriptographyMethods criptographyMethods)
    {
        _logger = logger;
        _criptographyMethods = criptographyMethods;  //Private variable receive te value from Dependency Injection 
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
            var myPlainText = "123456";
            var myCriptoText = _criptographyMethods.Encrypt(myPlainText); //text encryption
            var isEqual = _criptographyMethods.CompareText(myPlainText, myPlainText); //Plain text versus ciphertext comparison
    }
```

> 👉 **Step 5** - Using Other Methods
Add then using "using Unwired.Commons;"
```bash
    var myPathFile = @"~\Path\ImportXml.Xml";
    var result = UnwiredMethods.DeserializeXmlFromFile<MyXml>(myPathFile);
```
```bash
        var myStringXml = @"<?xml version=""1.0"" ?>
                            <MyXml>
                                <Name>John Constatine</frase>
                            </MyXml>";
        var result = UnwiredMethods.DeserializeXmlFromString<MyXml>(myStringXml);
```
All other existing methods and those that may be added in the future will be listed on the Wiki.

> 👉 **Step 6** - Using Extensions Methods

All other existing extensions methods and those that may be added in the future will be listed on the Wiki.
## ✨ Contacts

> 📧 **Email** - leo.cavalheiro.ti@gmail.com
