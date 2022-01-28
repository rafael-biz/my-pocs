# Azure Functions Demo

In this project you can find the following demos of Azure Functions.

## Http Demo

This demo shows a function that looks for a name parameter either in the query
string or the body of the HTTP request and return a friendly message.

## Service Bus Queue Demo

This demo shows a function that publishes a batch of 100 messages to a queue
and another function pulling out the messages as a queue.

In this demonstration, we can see that the delivery of messages is FIFO, but
the orderly delivery does not guarantee that the processing of messages occur
sequentially.

## Service Bus Topic Demo

This demo shows a function that publishes a message to a topic and two other
functions consuming the same message.

## Storage Queue Demo

This demo shows a function that publishes a message to a storage queue and
another function pulling out the messages.

## Storage Table Demo

This demo shows a set of functions that perform CRUD operations with an Azure
Storage Table.

## Timer Demo

This demo shows a log message every 5 minutes.

## Durable Function Orchestration Demo

This demo shows durable function orchestration.

# Endpoints

You may find the HTTP requests on this (link)[https://www.postman.com/rbbfcd/workspace/demos].