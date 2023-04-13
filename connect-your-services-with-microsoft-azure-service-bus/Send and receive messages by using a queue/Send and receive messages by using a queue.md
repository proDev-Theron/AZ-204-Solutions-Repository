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