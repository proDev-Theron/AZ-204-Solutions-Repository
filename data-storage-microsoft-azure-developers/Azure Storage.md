## Create a Storage Account using CLI
You usually create a storage account only in the beginning of the project
```
az storage account create \
  --resource-group learn-113363ec-07d4-4aa7-9961-352b8eae4b3e \
  --location southeastasia \
  --sku Standard_LRS \
  --name <name>
```
**name** = Storage Account Name

## Create a .NET Core Application
Not providing the name of the app will create files in the current directory. If you provide the name of the app, cd to that new folder. Use `dotnet run` to run the application.
`dotnet new console --name <NameOfTheApp>`

## Add a Azure Storage NuGet package
Azure.Storage.Blobs in this example
`dotnet add package Azure.Storage.Blobs`

## Create a connection string (appsettings.json)
Create a file called `appsettings.json` with this content:
```
{
    "ConnectionStrings": {
        "StorageAccount": "<value>"
    }
}
```
Then replace <value> with the output of this command:
```
az storage account show-connection-string \
  --resource-group learn-113363ec-07d4-4aa7-9961-352b8eae4b3e \
  --query connectionString \
  --name <name>
```

The output is something like:
`"DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=sdfsdkgpkdsgpksdg;AccountKey=QuEr8gMg0cp5pD4aIZGLHubEtpU79ntU5JKd0E20tvPWnz9txQDa8vwt3vY1OyuaMqvr8LN2CFkT+AStpD7qmA==;BlobEndpoint=https://sdfsdkgpkdsgpksdg.blob.core.windows.net/;FileEndpoint=https://sdfsdkgpkdsgpksdg.file.core.windows.net/;QueueEndpoint=https://sdfsdkgpkdsgpksdg.queue.core.windows.net/;TableEndpoint=https://sdfsdkgpkdsgpksdg.table.core.windows.net/"`


Add this <ItemGroup> in the existing `.csproj` file:
```
<ItemGroup>
    <None Update="appsettings.json">
        <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
</ItemGroup>
```

## Add support to read a JSON configuration file
run `dotnet add package Microsoft.Extensions.Configuration.Json`

## Add code to read the configuration file
Edit Program.cs
```
using System;    
using Microsoft.Extensions.Configuration;
using System.IO;


    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
        }
    }

```