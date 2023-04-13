﻿using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace privatemessagereceiver
{
    class Program
    {

        const string ServiceBusConnectionString = "Endpoint=sb://testtesttesttest1.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=T3toDz2VMFnE2c4hgLiRC3JtrdwUh08uB+ASbEd9voE=";
        const string QueueName = "salesmessages";

        static void Main(string[] args)
        {

            ReceiveSalesMessageAsync().GetAwaiter().GetResult();

        }

        static async Task ReceiveSalesMessageAsync()
        {

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after receiving all the messages.");
            Console.WriteLine("======================================================");


            // Create a Service Bus client that will authenticate using a connection string
            var client = new ServiceBusClient(ServiceBusConnectionString);

            // Create the options to use for configuring the processor
            var processorOptions = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            };

            // Create a processor that we can use to process the messages
            await using ServiceBusProcessor processor = client.CreateProcessor(QueueName, processorOptions);

            // Configure the message and error handler to use
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;

            // Start processing
            await processor.StartProcessingAsync();

            Console.Read();

            // Close the processor here
            await processor.CloseAsync();

        }

        // handle received messages
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            // extract the message
            string body = args.Message.Body.ToString();

            // print the message
            Console.WriteLine($"Received: {body}");

            // complete the message so that message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            // print the exception message
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
