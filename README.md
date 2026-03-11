# Microservices Communication Demo (.NET)

This project demonstrates **microservices communication using RabbitMQ and Kafka** in a .NET environment.

The system contains:

* API Gateway
* User Service
* Transaction Service

UserService publishes events when a user logs in.
TransactionService consumes these events and records the transaction.

---

# Architecture

API Gateway → UserService → Message Broker → TransactionService

Communication:

RabbitMQ → asynchronous messaging
Kafka → event streaming

Each service has:

* Domain Layer
* Application Layer
* Infrastructure Layer
* API Layer

---

# Prerequisites

Install the following:

* .NET 8 SDK
* Docker Desktop
* RabbitMQ
* Kafka

---

# RabbitMQ Setup (Local)

Run RabbitMQ with Docker:

docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

RabbitMQ Management UI:

http://localhost:15672

Username: guest
Password: guest

---

# Kafka Setup (Local)

Start Zookeeper:

docker run -d --name zookeeper -p 2181:2181 wurstmeister/zookeeper

Start Kafka:

docker run -d --name kafka -p 9092:9092 
-e KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 
-e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 
wurstmeister/kafka
docker run -d --name kafka -p 9092:9092 -e KAFKA_ZOOKEEPER_CONNECT=zookeeper:2181 -e 
KAFKA_LISTENERS=PLAINTEXT://0.0.0.0:9092 -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://localhost:9092 -e 
KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 --link zookeeper confluentinc/cp-kafka
---

# NuGet Packages

Install required packages:

RabbitMQ:

dotnet add package RabbitMQ.Client

Kafka:

dotnet add package Confluent.Kafka

Entity Framework Core:

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.InMemory

---

# Running the Services

Start the services:

1. Run RabbitMQ
2. Run Kafka
3. Start TransactionService
4. Start UserService
5. Start API Gateway

---

# Testing the System

1. Call login endpoint in UserService

POST /api/users/login

2. UserService publishes login event

3. TransactionService consumes the event

4. Transaction is stored in database (or InMemory)

5. Check transactions:

GET /api/transactions

---

# RabbitMQ Monitoring

Open:

http://localhost:15672

Check:

* Connections
* Channels
* Queues
* Messages

---

# Kafka Monitoring

Kafka runs on:

localhost:9092

You can consume events using:

kafka-console-consumer --bootstrap-server localhost:9092 --topic user-events --from-beginning

---

# Free Cloud Deployment Options

RabbitMQ Free Cloud Options:

CloudAMQP (Free Tier)
https://www.cloudamqp.com/

Upstash Redis Queue (Alternative)
https://upstash.com/

Kafka Free Cloud Options:

Confluent Cloud (Free Tier)
https://confluent.cloud/

Upstash Kafka (Free Tier)
https://upstash.com/kafka

These services allow running RabbitMQ and Kafka without hosting your own servers.


