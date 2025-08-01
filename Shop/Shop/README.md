`ShopService` is the entrypoint of the app's business logic.
It exposes 2 functionalities:
 - adding items to bucket,
 - removing items from bucket.

Task 1. Please do the following and write down your findings:
1. Find bugs in the application.
2. Define the problems that exist in the app's core code and how can they influence further development.
3. Refactor the code so that it follows CQRS/DDD patterns and is robust and easily expandable/maintainable. Write down steps taken in order to refactor the code. Do not add database support, focus on the domain logic.

Task 2. Please expand the app with following features:
1. Keep audit of all changes done to the bucket.
2. Add "Clear" feature to the bucket. It should remove all items at once.
3. Implement sending e-mails to system admin when doing all actions on the bucket (Add Item / Remove Item / Clear). E-mail sending should be stub implementation, please only show the mechanics how you would like to trigger the process.