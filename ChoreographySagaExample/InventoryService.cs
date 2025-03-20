namespace ChoreographySagaExample
{
    public class InventoryService
    {
        private readonly EventBus _eventBus;

        public InventoryService(EventBus eventBus)
        {
            _eventBus = eventBus;

            // Subscribe to PaymentProcessed events
            _eventBus.Subscribe<PaymentProcessed>(HandlePaymentProcessed);
        }

        private async Task HandlePaymentProcessed(PaymentProcessed @event)
        {
            try
            {
                // Simulate inventory reservation
                Console.WriteLine($"Reserving inventory for order {@event.OrderId}...");
                await Task.Delay(1000); // Simulate async work

                // Publish InventoryReserved event
                await _eventBus.Publish(new InventoryReserved(@event.OrderId));
            }
            catch (Exception)
            {
                // Publish InventoryReservationFailed event
                await _eventBus.Publish(new InventoryReservationFailed(@event.OrderId));
            }
        }
    }
}
