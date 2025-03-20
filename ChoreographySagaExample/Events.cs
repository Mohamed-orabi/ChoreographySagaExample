namespace ChoreographySagaExample
{
    public abstract record SagaEvent(Guid OrderId);

    public record OrderCreated(Guid OrderId, Guid CustomerId, decimal TotalAmount) : SagaEvent(OrderId);

    public record PaymentProcessed(Guid OrderId) : SagaEvent(OrderId);

    public record PaymentFailed(Guid OrderId) : SagaEvent(OrderId);

    public record InventoryReserved(Guid OrderId) : SagaEvent(OrderId);

    public record InventoryReservationFailed(Guid OrderId) : SagaEvent(OrderId);

    public record OrderCompleted(Guid OrderId) : SagaEvent(OrderId);

    public record OrderFailed(Guid OrderId) : SagaEvent(OrderId);
}
