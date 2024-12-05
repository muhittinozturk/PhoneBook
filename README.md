# PhoneBook Microservice Project

## Person Microservice

- **Architecture:** Implementing CQRS and Onion Architecture with Best Practices
- **Design Pattern:** Developing CQRS design pattern using MediatR, FluentValidation, Generic Repostory and AutoMapper
- **Event Handling:** Consuming RabbitMQ report request event queue with a common event bus layer
- **Database:** Using PostgreSQL database
- **ORM:** Using Entity Framework Core

## Report Microservice

- **API Framework:** Using Minimal API
- **Design Pattern:**  Generic Repostory
- **Event Handling:** Consuming RabbitMQ for report request 
- **Database:** Using MongoDB database

## API Gateway

- **Implementation:** Implementing API Gateways with Ocelot

### Testing

- **Unit Tests:** Implemented using xUnit
- **Functional Tests:** Conducted to ensure system functionality using xUnit

### Tools

- **Message Broker:** RabbitMQ
  - **Default User:** guest
  - **Default Password:** guest
  - **Default Port:** 5672
  - **Default Port:** 15672
- **Database:** PostgreSQL
  - **Default User:** postgres
  - **Default Password:** 123456
  - **Default Port:** 5672
- **Database (MongoDB):**
  - **Default Port:** 27017

### Api Gateway -> http://localhost:5000
                    
Proccess       | Request Endpoint                  | Method
-------------  | -------------                     | -------------
Create Person  | http://localhost:5000/person | Post
Get Person  | http://localhost:5000/person/{id} | Get
Update Person  | http://localhost:5000/person | Put
Delete Person  | http://localhost:5000/person{id} | Delete
Get All Person  | http://localhost:5000/person/GetAll | Get
Get Report  | http://localhost:5000/report/{id} (this id ReportDetailId) | Get
Get All Report  | http://localhost:5000/report/GetAll/{id} (this id ReportId id) | Get
Create Report Request  | http://localhost:5000/reportrequest | Post
Get All Report Request  | http://localhost:5000/reportrequest | Get


### Person Api
                    
Proccess       | Request Endpoint                  | Method
-------------  | -------------                     | -------------
Create Person  | http://localhost:6060/api/person | Post
Get Person  | http://localhost:6060/api/person/{id} | Get
Update Person  | http://localhost:6060/api/person | Put
Delete Person  | http://localhost:6060/api/person{id} | Delete
Get All Person  | http://localhost:6060/api/person/GetAll | Get
Get Report  | http://localhost:6060/api/report/{id} | Get
Get All Report  | http://localhost:6060/api/report/GetAll/{id} | Get

### Report Api
                    
Proccess       | Request Endpoint                  | Method
-------------  | -------------                     | -------------
Create Report Request  | http://localhost:5050/api/report | Post
Get All Report Request  | http://localhost:5050/api/report | Get


#### Requirements 

Create Common Network

`$ docker network create --attachable --driver bridge net_common`

Build docker compose

`$ docker-compose build`

Run docker compose

`$ docker-compose up`


### Microservice Schema
```mermaid
graph TD
  subgraph Client
    A(Client) -->|HTTP Request| B(ApiGateway)
  end

  subgraph ApiGateway
    B -->|Forward to| C(ReportService)
    B -->|Forward to| D(PersonService)
  end

  C(ReportService) -->|Command Query| E(MongoDB)
  D(PersonService) -->|Command Query| G(PostgreSQL)

  C -->|Publish| H(RabbitMQ)
  D -->|Publish| H

  H(RabbitMQ) -->|Subscribe| C
  H -->|Subscribe| D



