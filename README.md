# eShop - Tuttorial-based Project

### *Ports (HTTP-HTTPS)*

| Service            | Local Env | Docker Env | Docker Inside(?) |
| -------------------| --------- | ----------- | ----------------|
| Catalog            | 5000 - 5050 | 6000 - 6060 | 8080 - 8081 |
| Basket             | 5001 - 5051 | 6001 - 6061 | 8080 - 8081 |
| Discount           | 5002 - 5052 | 6002 - 6062 | 8080 - 8081 |
| Ordering           | 5003 - 5053 | 6003 - 6063 | ***N/A***   |

## *Phase 1: Analysis*
### Domain Analysis of Catalog Microservice
1. Domain Models
- Primary domain model is _Product_, associated with _Category_(a _Product_ can be included in multiple (at least 1) _Categories_
- Domain events: Can be _Price Changes_ (leading to integration events)
2. Application Use Cases
- Listing Products & Categories
- Searching products
  - List _Products_ and _Categories_
  - Get _Product_ by id
  - Get _Products_ by _Category/ Categories_
  - Create/ Update/ Delete _Product(s)_
3. REST API Endpoints

| Method | URI              | Return            | Description |
| ------ | ---------------- | ----------------- | ----------- |
| GET    | /products        | ProductDTO[]      | Return list of products. Can have query string for filters |
| GET    | /products/{id}   | ProductDTO        | Return the product with associated ID |
| POST   | /products        | ProductDTO        | Return the newly created product |
| PUT    | /products/{id}   | ProductDTO        | Return the newly updated product |
| DELETE | /products/{id}   | bool              | Delete the product with associated ID |
4. Underlying Data Structures
  - Document Database for Store Catalog JSON data
  - 2 options: MongoDB / PostgreSQL DB JSON columns
    - Tutorial: Choose PostgreSQL with the ***Marten*** library (transform PostgreSQL into a .NET Transactional Document DB)
    - Can store and query data as JSON documents
    - Combines the complexity of a document database with the reliability of relational PostgreSQL database
### Technical Analysis
1. Application Architecture Style
  - Veritcal Slice Architecture: Organize our code into feature folders, each feature is encapsulated in a single .cs file
2. Patterns & Principles
  - CQRS: Command on Write DB, Query on Read DB (take care of Eventual Consistency as well)
  - Mediator Pattern: Facilitate object interaction through a Mediator
  - DI in ASP.NET Core
  - Minimal APIs & Routings
  - ORM Pattern
3. Libraries
  - MediatR for CQRS
  - Carter for API Endpoints
  - Marten for PostgreSQL Interaction
  - Mapster for Object Mapping
  - FluentValidation for Input Validation
4. Project Folder Structure
  - Model, Features, Data, Abstractions
  - Features (e.g. CreateProduct, GetProduct, etc.) have dedicated handlers and endpoint definitions
  - Feature folder will be Products
    - A snapshot of the folder: Products > (CreateProduct, GetProductById, etc.) > Endpoint & Handler
  - Data folder and Context objects manage DB interactions
5. Deployment
  - Containerize with Docker, ensuring deployment and integration with PostgreSQL
  - Implement Dockerfile & docker-compose file for running the microservice and PostgreSQL database in Docker env.
#### Appendix A. CQRS - Read and Write Operations
  - Read DB uses NoSQL databases with denormalized data
  - Write DB uses Relational databases with fully normalized and supports strong data consistency
  - Separate the read (query) operations from the write (command) operations at the code level, but not necessarily at the DB level
    - If the same DB is used, the paths for reading and writing data are distinct
    - If separate DBs are used, we need to ensure data consistency and synchronization
  - Using MediatR:
    - Should create custom abstractions for clarity, instead of using IRequest for both read & write operations
    - 
