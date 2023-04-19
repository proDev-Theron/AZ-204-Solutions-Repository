# Create an Azure function triggered by a webhook
you'll create your code to parse the GitHub wiki event messages in Azure Functions. You'll configure your function to run when a webhook message is received.
## Create a Function App

## Create a webhook triggered function
When your deployment is complete, select Go to resource. The Overview pane appears for your Function App.

- In the left menu pane, under Functions, select Functions. The Functions pane appears for your Function App.
- On the top menu bar bar, select Create. The Create function pane appears.
- Under Select a template, select HTTP trigger, and then select Create. The HttpTrigger1 pane appears for your Function, displaying essentials for your new trigger.
- In the left menu pane, under Developer, select Code + Test. The Code + Test pane appears for your Function, displaying the JavaScript file that was created from the template. It should look like the following code.
```
module.exports = async function (context, req) {
    context.log('JavaScript HTTP trigger function processed a request.');

    const name = (req.query.name || (req.body && req.body.name));
    const responseMessage = name
        ? "Hello, " + name + ". This HTTP triggered function executed successfully."
        : "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";

    context.res = {
        // status: 200, /* Defaults to 200 */
        body: responseMessage
    };
}
```

## Test triggering your function
1. In the top menu bar, select Get function URL.
2. In the Get function URL dialog box, in the Key dropdown list, under Function key, select default. In the URL field, select the Copy to clipboard icon.
3. Paste this URL into a browser, and at the end of URL, append the query string parameter: &name=<yourname>, for example &name=Dick and Jane.
4. To run the request, press Enter. The response returned by the function appears in the browser.