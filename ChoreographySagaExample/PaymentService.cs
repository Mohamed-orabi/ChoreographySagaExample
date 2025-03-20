namespace ChoreographySagaExample
{
    public class PaymentService
    {
        private readonly EventBus _eventBus;

        public PaymentService(EventBus eventBus)
        {
            _eventBus = eventBus;

            // Subscribe to OrderCreated events
            _eventBus.Subscribe<OrderCreated>(HandleOrderCreated);
        }

        private async Task HandleOrderCreated(OrderCreated @event)
        {
            try
            {
                // Simulate payment processing
                Console.WriteLine($"Processing payment for order {@event.OrderId}...");
                await Task.Delay(1000); // Simulate async work

                // Publish PaymentProcessed event
                await _eventBus.Publish(new PaymentProcessed(@event.OrderId));
            }
            catch (Exception)
            {
                // Publish PaymentFailed event
                await _eventBus.Publish(new PaymentFailed(@event.OrderId));
            }
        }
    }
}
