# ac.sprout.exam
Sprout's technical screening exam
This project contains a code sample implementation for Sprout's technical screening exam. It consists of an ASP.NET Core web API with layered architecture and unit test coverage.

Solution Architecture
The solution is organized into the following projects:

Sprout.Exam.WebApp - Contains API controllers and startup configuration. Depends on Sprout.Exam.Business to access business logic services.
Sprout.Exam.Business - Houses interfaces, services, view models, and business logic processing. Depends on Sprout.Exam.DataAccess to retrieve data.
Sprout.Exam.DataAccess - Implements data access via Entity Framework Core, including DbContext and repository pattern.
Sprout.Exam.Common - Provides shared types like data transfer objects and utility classes used by multiple layers.


Key Technologies
ASP.NET Core Web API
Entity Framework Core
SQL Server
xUnit for unit testing
Onion architecture and separation of concerns


Question:
If we are going to deploy this on production, what do you think is the next improvement that you will prioritize next? This can be a feature, a tech debt, or an architectural design.

Answer:
If this application were going into production, some of the high priority improvements I would focus on are:

Implement Repository Caching

To improve performance, I would have the EmployeeRepository implement caching of query results. This avoids unnecessary repeated queries to the database. The in-memory cache could be configured with expiration times.

Pagination for List Endpoints

Paginate results for any list endpoints to limit response size

Rate Limiting Policies

Implement rate limiting to prevent abuse and DDoS attacks

Custom Exception Middlewares

Currently exception handling is minimal. I would create custom exception handling middleware that would catch all unhandled exceptions, log the full details, and return appropriate error responses to clients rather than crashing the app.


CI/CD Pipeline

Setting up a continuous integration and deployment pipeline would enable automating testing and deployment processes. This ensures quality and makes delivery efficient.

Role-based Access Control

Additional authorization should be implemented for production via policies that grant access to endpoints based on user roles. Registering users and integrating with a central authentication provider would be part of this.

