  # Maestro

Maestro is web based solution, build to fullfil the requirements for Software University course - "Architecture of ASP .NET Core Microservices Applications"

# What's it about
 Maestro is a plaform which can be used to track request and orchestrate work among employees. 
 It uses ASP.Core 3.1 and multiple microservices.

# Roles
 - Adminisrator
 - Employee
 - User


Everyone can register in the website using the register form. When the registration is successfull they become an :

### User 
Users can create new request in "New request" page. They hae to give some description, and category.
They can also track the progress in the "My requests" tab.

### Administrator 
 Logging as administrator you should be able to see the "Admin Panel"
It consists of two items: 
 - Users page
 - Employees page

In Users page, the Admin sees a list of all registered users. He has the option to promote a user to employee.

In Employees page, the Admin sees a list of all the Employees, some basic information like name, how many open work items they have and how much time they take to complete a request.


### Employee 
 An Employee can see a list of all requests in "Available Work" page. There, using the button they can take this work.
 Once taken, the work will be visualized in "My work" page. From there the employee can close the work, thus completing the request.


## So, how does this definitely not useful system is structured you may ask

![Nenujno slojna arhitektura](https://i.imgur.com/vf0lbIg.png)

# But how does it work man ??
Magic, moving on.


# pls tell
Ok well, when user creates a new request,  RabbitMQ ( MassTransit ) fires an event, which is consumed by the Employees Microservices. A new work item is created and is listed available under "Available work" page.

When a user is promoted to employee the same thing happens. Identity microservice fires an event -> Employees microservices consumes the event, creating new Employee in the Employees database.

When employee finishes given work, few things happens. 
 - An event is fired, and is being consumed by the Requests microservice thus updating the requests table.
 - Another event is fired, consumed by the Statistics microservice, doing some calculations showing the average time an employee takes to complete a request.
