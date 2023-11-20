# PhoneBook

### Person Api
                    
Proccess       | Request Endpoint                  | Method
-------------  | -------------                     | -------------
Create Person  | http://localhost:6060/api/person | Post
Get Person  | http://localhost:6060/api/person/{id} | Get
Update Person  | http://localhost:6060/api/person | Put
Delete Person  | http://localhost:6060/api/person | Delete
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
