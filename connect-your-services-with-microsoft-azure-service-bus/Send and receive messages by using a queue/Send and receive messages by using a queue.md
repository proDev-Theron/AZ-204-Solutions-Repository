# Send and receive messages by using a queue
First create a topic and a queue under a service bus namespace. I created mine using Azure portal and put the service bus namespace under a resource group.

## Get the connection string to the Service Bus namespace
You must configure two pieces of information in your two console apps to access your Service Bus namespace and to use the queue within that namespace:
- Endpoint for your namespace
- Shared access key for authentication
You can get these values from the connection string.

In Azure Cloud Shell, run the following command, replacing <namespace-name> with the Service Bus namespace that you created in the last exercise.
```
az servicebus namespace authorization-rule keys list \
    --resource-group <resource-group-name> \
    --name RootManageSharedAccessKey \
    --query primaryConnectionString \
    --output tsv \
    --namespace-name <namespace-name>
```
The last line in the response is the connection string, which contains the endpoint for your namespace and the shared access key. It should resemble the following example:
`Endpoint=sb://testtesttesttest1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=T3toDz2VMFnE2c4hgLiRC3JtrdwUh08uB+ASbEd9voE=
`

## Clone and open the starter application
In a production application, you should use a configuration file or Azure Key Vault to store the connection string.

- Clone the git project using Azure Shell
```
cd ~
git clone https://github.com/MicrosoftDocs/mslearn-connect-services-together.git
```

## Write code to send a message to a queue
1. In the Cloud Shell editor, open privatemessagesender/Program.cs and locate the following line of code:
`const string ServiceBusConnectionString = "";`
Paste the connection string between the quotation marks.
2. Fill in the missing code. You can use the comments as guide.
3. `cd` to the `privatemessagesender` folder and use `dotnet run` to run the program and send a message to the queue.
4. When the app is finished running, run this command below to see how many messages are in the queue.
```
az servicebus queue show \
    --resource-group learn-f5d1d94c-9ea3-48fb-bf9c-4144fd040d9f \
    --name salesmessages \
    --query messageCount \
    --namespace-name <namespace-name>
```
5. Each time you run the dotnet app, a new message is added to the queue. You'll see the messageCount increase each time you run the Azure command.

## Write code to receive messages from the queue
1. In the editor, open privatemessagereceiver/Program.cs and find the following line of code:
`const string ServiceBusConnectionString = "";`
2. Fill in the missing code. You can use the comments as guide.
3. `cd` to the `privatemessagereceiver` folder and use `dotnet run` to run the program and receive the message/s from the queue.
4. When the app is finished running, run this command below to see how many messages are in the queue.
```
az servicebus queue show \
    --resource-group learn-f5d1d94c-9ea3-48fb-bf9c-4144fd040d9f \
    --name salesmessages \
    --query messageCount \
    --namespace-name <namespace-name>
```
The output will be `0` if all the messages have been removed.
