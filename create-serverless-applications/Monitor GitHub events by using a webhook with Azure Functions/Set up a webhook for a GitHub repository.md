# Set up a webhook for a GitHub repository
In this exercise, you'll set up a webhook for a GitHub repository. You'll learn how to listen for specific events, and how to make your webhook callback to your function when the event is triggered.

## Setup
1. Using your web browser, sign in to your GitHub account.

2. Create a new repository by selecting New in the left menu pane. The Create a new repository page appears.

3. In the Repository name box, enter a meaningful name such as LearnWebhookTest.

4. Select Public to activate your wiki module, and find it in the menu.

5. Select Create repository. The Quick setup page appears.

6. Select the creating a new file link.

7. In the top menu bar, select Wiki to display the pages in your repository (or repo). A Welcome page appears.

8. Select Create the first page. The Create new page template appears.

9. Add some text, and select Save Page. The first page in a Wiki is the Home page.

## Add a webhook for the Gollum event
`Gollum` is the name of a GitHub event that is fired whenever a page in a repository's wiki is created or updated.