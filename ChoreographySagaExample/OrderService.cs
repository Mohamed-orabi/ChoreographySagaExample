namespace ChoreographySagaExample
{
    public class OrderService
    {
        private readonly EventBus _eventBus;
        private readonly List<Order> _orders = new();

        public OrderService(EventBus eventBus)
        {
            _eventBus = eventBus;

            // Subscribe to InventoryReserved and InventoryReservationFailed events
            _eventBus.Subscribe<InventoryReserved>(HandleInventoryReserved);
            _eventBus.Subscribe<InventoryReservationFailed>(HandleInventoryReservationFailed);
        }

        private async Task HandleInventoryReserved(InventoryReserved @event)
        {
            await CompleteOrder(@event.OrderId);
        }

        private async Task HandleInventoryReservationFailed(InventoryReservationFailed @event)
        {
            await FailOrder(@event.OrderId);
        }

        public async Task CreateOrder(Order order)
        {
            order.Id = Guid.NewGuid();
            _orders.Add(order);

            // Publish OrderCreated event
            await _eventBus.Publish(new OrderCreated(order.Id, order.CustomerId, order.TotalAmount));
        }

        public async Task CompleteOrder(Guid orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = "Completed";
                await _eventBus.Publish(new OrderCompleted(orderId));
            }
        }

        public async Task FailOrder(Guid orderId)
        {
            var order = _orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                order.Status = "Failed";
                await _eventBus.Publish(new OrderFailed(orderId));
            }
        }
    }
}
