# Web Application Documentation

## Technologies Choices

### Backend Components

- **ASP.NET Core MVC** I build the backend using ASP.NET Core MVC. It is a dotnet framework which separates the functionalities of the web application into 3 main components: Model, View, and Controller. This framework allows me to create professional web applications with the ability to utilize and incorporate many libraries and different services. 

- **.Net 8.0** I programmed the web application using .Net 8.0 which is the LTS (Long Term Support) version of the .Net framework. It is compatible with different operating systems and allows me to utilize new features and functionalities of the .Net framework. Being the LTS version it is patched frequently and complies with the latest security standards.

- **Azure SQL Database** I used Azure SQL Database to store customer information and order details. Azure SQL Database is a cloud-based sql database service provided by Microsoft. It is fully managed by Microsoft and is highly scalable. It can be accessed programmatically to allow users to inset, update, delete, and query data. Since it is fully managed by Microsoft I take on less risk and responsibility as Microsoft takes care of the database management, backups, and security. 

- **Azure Functions** I used Azure Functions for Blob, Table, Queue and File Share storage. Azure Functions is a serverless compute service which allows me to create small pieces of code which I am able to reuse and scale. I created HTTP triggers for Blob, Table, Queue and File Share storage. Triggers function like API endpoints which can be called to perform a specific task. I am able to interact programmatically with the storage services using the Azure Functions.

- **Azure Blob Storage** I used blob storage for my web application to store images and files. Azure blob storage allows for the storing of large amounts of unstructured data. It stores data in byte arrays and is highly scalable. I used Azure Blob Storage to store images of products and files uploaded by customers and users.

- **Azure Table Storage** I used table storage to store customer information such as name, surname and country. Azure Table Storage is a NoSQL data storage and it stores data in key-value pairs called Row-Keys and Partition-Keys. It is useful for storing data without a schema and structuring data in a way that is easy to query.
  
- **Azure Queue Storage** I used queue storage to store user messages and orders. Azure Queue Storage is a service for storing messages which can be accessed from anywhere in the world. The messages are stored in order from most recent to oldest and can be accessed programmatically. I used the message queue to store messages and orders entered by users and customers within the web application.
  
- **Azure File Share**  I used File Share to store files uploaded by customers. Azure File Share helps me to store files such as pdfs, images, and documents. It allows for the sharing of files which can be accessed from anywhere in the world. I used Azure File Share to store files uploaded by customers and users within the web application. 

### Frontend Components

- **CSHTML** I used CSHTML to create the frontend and the views of the web application. CSHTML is MVC syntax which allows me to create views and interact with the backend. I used CSHTML to create view for my backend services File Share, Blob, Table, Queue and Azure SQL Database services. The CSHTML allowed the user to insert and view data within the database and storage services.

- **CSS** I used CSS to style the frontend of my web application. It allows me to create animations and visually appealing effects and designs for the user to interact with. CSS allows me to add a fun component to the web application and make it more interactive and engaging for the user.
  
- **JavaScript** I used JavaScript to handle the frontend logic of the web application. JavaScript allows me to add interactive elements within the CSHTML views. JavaScript can also help me update the views and interact with the backend services.

## Database Replica  

I created a database replica for my Azure SQL Database for the following reasons:

**High Availability** By creating a database replica I have an additional copy of the database which can be used in case of failure or downtime. This ensures that the web application is always available to the users and customers.

**Load Balancing** A replica database can be used to balance the work load of the web application. The replica can be used to level out queries and requests to the database. This ensures that the web application runs smoothly and efficiently.

**Disaster Recovery** If the primary database fails the replica can be used to recover all the customer information and order details. This prevents data loss and ensures that the database can be restored or recovered in case of failure.

**Performance** The replica can be used to improve the speed and scalability of the web application. This ensures that the web application can handle a large number of requests and queries. This will improve the performance of the web application lowering the latency and improving the user experience.


## Application Requirements 

### Functional Requirements

1. **Upload Files** The user must be able to upload files to the web application. The files will be stored within the Azure File Share service.

2. **Upload Images** The user must be able to upload images to the web application. The images will be stored within Azure Blob Storage.

3. **Insert Data into Azure SQL Database** The user must be able to upload customer information into the Azure SQL Database. The customer information will include name, surname, and country.

4. **Upload messages/orders into Azure Queue Storage** The user must be able to upload messages and orders into Azure Queue Storage. The messages will be orders and customer queries.

5. **Insert Customer Information into Azure Table Storage** The user must be able to insert relevant customer data into table storage. The customer data will include name, surname, and country.

6. **View data from storage services** The user must be able to view there recently uploaded blobs, files, messages, and table data within the web application.


## Technology Choices

**Data Storage** I opted to use Azure Data storage services for the web application as they are highly scalable and secure. These services are managed by Microsoft and are fully managed relieving  some of the responsibility from me the developer. This reduces the risk of failures through human error and allows me to focus on the development of the web application.

## Hosting Model

**PaaS (Platform as a Service)** I chose to host the web application using Azure App Service which is a PaaS (Platform as a Service). PaaS is a cloud deployment model which allows me to deploy a web application without needing to manage the underlying infrastructure. Azure allows me to have multiple services and storage accounts which I can interact with programmatically. This simplifies the development process and allows the developer to focus on the code and not the infrastructure.

## Alternative Services

**Amazon S3** Amazon S3 is a blob storage service provided by Amazon Web Services rather than azure. It can be used the same as blob storage to store images and files. it is compatible with different operating systems and can be accessed programmatically.

**Snowflake** Snowflake is an azure table storage alternative. it is used to store unstructured data and can be accessed programmatically. It is a no SQL database service which can be used to store data in key-value pairs. 

**Oracle Database** Oracle Database is a relational database management which can be used as an alternative to Azure SQL Database. It allows the updating, querying, and deleting of data and can be accessed programmatically.

**Google Cloud Filestore** Google Cloud Filestore is an alternative to Azure File Share. It is used to store files which can be accessed from anywhere in the world by multiple authorized users. It is fully managed by Google and is highly scalable.


Links for Part 3.

Github: https://github.com/JacquesLife/WebApplication1/tree/master/WebApplication1

Link to website: https://jacqueswebapp.azurewebsites.net/

