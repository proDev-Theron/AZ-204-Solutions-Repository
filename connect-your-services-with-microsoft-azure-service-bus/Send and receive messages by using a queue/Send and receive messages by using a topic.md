# Send and receive messages by using a topic
First create a topic under a service bus namespace. I created mine using Azure portal and put the service bus namespace under a resource group.

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

## Write code to send a message to a topic
1. In the Azure Cloud Shell editor, open `performancemessagesender/Program.cs` and find the following line of code:
`const string ServiceBusConnectionString = "";`
Paste the connection string between the quotation marks.
2. Fill in the missing code. You can use the comments as guide.
3. `cd` to the `performancemessagesender` folder and use `dotnet run` to run the program and send a message to the queue.
4. When the app is finished running, run this command below to see how many messages are in the topic. You can also go the Azure portal to check it visually.
```
az servicebus topic subscription show \
    --resource-group learn-f5d1d94c-9ea3-48fb-bf9c-4144fd040d9f \
    --topic-name salesperformancemessages \
    --name Americas \
    --query messageCount \
    --namespace-name <namespace-name>
```
5. If you replace Americas with EuropeAndAsia and run the command again, you'll see that both subscriptions have the same number of messages.

## Write code to retrieve a topic message for a subscription
1. In the editor, open `performancemessagereceiver/Program.cs` and find the following line of code:
`const string ServiceBusConnectionString = "";`
2. Fill in the missing code. You can use the comments as guide.
3. `cd` to the `performancemessagereceiver` folder and use `dotnet run` to run the program and receive the message/s from the topic subscription "Americas".
4. When the app is finished running, run this command below to see how many messages are in the topic.
```
az servicebus topic subscription show \
     --resource-group learn-f5d1d94c-9ea3-48fb-bf9c-4144fd040d9f \
     --topic-name salesperformancemessages \
     --name Americas \
     --query messageCount \
     --namespace-name <namespace-name> \
```
The output will be `0` if all the messages have been removed in "Americas". 
If you replace Americas with EuropeAndAsia in this code to see the current message count for the `EuropeAndAsia` subscription, you'll see that the message count is still there. In the preceding code, only Americas was set to retrieve topic messages, so that message is still waiting for EuropeAndAsia to retrieve it.
