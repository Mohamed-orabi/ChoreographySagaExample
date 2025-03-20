# ChoreographySagaExample

- Choreography-Based Saga
  
  - Each service involved in the saga publishes events when it completes its local transaction.
  - Other services listen to these events and trigger their own local transactions.
  - No central coordinator is involved; the services communicate through events.
 
##  How It Works
  - Each service publishes events after completing its local transaction.
  - Other services subscribe to these events and execute their own transactions.
  - If a service fails, it publishes a failure event, and other services execute compensating transactions.

## Example: Order Processing Saga
Let’s say we have three services:

1. Order Service: Creates an order.
2. Payment Service: Processes the payment.
3. Inventory Service: Reserves items in the inventory.

Steps

1. Order Service creates an order and publishes an OrderCreated event.
2. Payment Service listens to OrderCreated, processes the payment, and publishes a PaymentProcessed event.
3. Inventory Service listens to PaymentProcessed, reserves the items, and publishes an InventoryReserved event.
4. If any step fails (e.g., payment fails), the service publishes a failure event (e.g., PaymentFailed), and other services execute compensating transactions (e.g., cancel the order, release inventory).
