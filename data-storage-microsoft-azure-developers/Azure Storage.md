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

## Add the Azure Storage NuGet package
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

## Add code to Connect your application to your Azure Storage account
Edit Program.cs
```
//Add this import you downloaded from NuGet earlier
using Azure.Storage.Blobs;

// create the BlobContainerClient object in the main section of the program
var connectionString = configuration.GetConnectionString("StorageAccount");
string containerName = "photos";

BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

//At this point, the client library has not attempted to connect to Azure or validate the connection string and access key being used. It has simply constructed a lightweight client object used to perform operations against Azure Blob Storage. Only when an operation is invoked against the storage account will a network call be made.
container.CreateIfNotExists();
```

The whole Program.cs will look like this now:
```
using System;    
using Microsoft.Extensions.Configuration;
using System.IO;
using Azure.Storage.Blobs;


    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("StorageAccount");
            string containerName = "photos";

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

            container.CreateIfNotExists();
        }
    }

```

# Upload an image to an Azure Storage account
To work with individual blob objects in your Azure Storage account, you use a BlobClient object. To get a BlobClient object is, call the GetBlobClient method on the BlobContainerClient object of the container where the blob will be stored. When calling the GetBlobClient method, you also supply a name for the blob in the container. For our example, the name of the blob will be the same as the name of our file.

```
string blobName = "docs-and-friends-selfie-stick";
string fileName = "docs-and-friends-selfie-stick.png";
BlobClient blobClient = container.GetBlobClient(blobName);
blobClient.Upload(fileName, true);
```
The second argument in the Upload method specifies if an existing blob object with the same name can be overwritten. By default, this value is false. In this case, we're specifying true to allow the program to be run multiple times.

## List objects in an Azure Blob Storage container
```
var blobs = container.GetBlobs();
foreach (var blob in blobs)
{
    Console.WriteLine($"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}");
}
```
