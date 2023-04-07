## Create a Storage Account using CLI
You usually create a storage account only in the beginning of the project
```
az storage account create \
  --resource-group learn-113363ec-07d4-4aa7-9961-352b8eae4b3e \
  --location southeastasia \
  --sku Standard_LRS \
  --name <name>
```

## Create a .NET Core Application
Not providing the name of the app will create files in the current directory. If you provide the name of the app, cd to that new folder. Use `dotnet run` to run the application.
`dotnet new console --name <NameOfTheApp>`

## Add a Azure Storage NuGet package
Azure.Storage.Blobs in this example
`dotnet add package Azure.Storage.Blobs`