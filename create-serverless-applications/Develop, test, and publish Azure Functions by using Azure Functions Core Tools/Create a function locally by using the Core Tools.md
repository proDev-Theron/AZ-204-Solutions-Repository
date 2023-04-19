# Create a function locally using Core Tools
Azure Functions Core Tools lets you develop functions locally on your own computer by:

Creating the files and folders necessary for a functions project.
Providing a Functions host that runs locally from the root directory of your project.

## Create a new directory called loan-wizard and go to that directory.
```
mkdir ~/loan-wizard
cd ~/loan-wizard
```

## Run `func init` to initialize the loan-wizard directory as a functions project folder.
`func init`

# Create an HTTP-triggered function
run `func new` to start the function creation wizard

Remember, you're running func new in the loan-wizard project folder you created, which is important.

# Implement the simple-interest function
In the editor's FILES pane, expand the simple-interest folder, and select index.js to open the file in the editor.

Replace the full contents of `index.js` with the following code:
```
module.exports = async function(context, req) {
  // Try to grab principal, rate, and term from the query string and
  // parse them as numbers
  const principal = parseFloat(req.query.principal);
  const rate = parseFloat(req.query.rate);
  const term = parseFloat(req.query.term);

  if ([principal, rate, term].some(isNaN)) {
    // If any empty or non-numeric values, return a 400 response with an
    // error message
    context.res = {
      status: 400,
      body: "Please supply principal, rate and term in the query string"
    };
  } else {
    // Otherwise set the response body to the product of the three values
    context.res = { body: principal * rate * term };
  }
};
```

# Run the function in Cloud Shell
Run the following command to start the Functions host silently in the background.
`func start &> ~/output.txt &`
As with func new, Cloud Shell should still be in the loan-wizard directory.
You can ignore the output of this command. The Functions host is now writing its output to the file ~/output.txt, and we can continue to use the command line while it's running.

Enter the following command to view the output log.
`cat ~/output.txt`

Near the end of the output, you'll see a message that lists Functions: simple-interest: is available as a GET or POST HTTP request http://localhost:7071/api/simple-interest.