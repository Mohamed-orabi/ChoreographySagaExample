namespace ChoreographySagaExample
{
    public class EventBus
    {
        private readonly Dictionary<Type, List<Func<object, Task>>> _handlers = new();

        public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : class
        {
            if (!_handlers.ContainsKey(typeof(TEvent)))
            {
                _handlers[typeof(TEvent)] = new List<Func<object, Task>>();
            }
            _handlers[typeof(TEvent)].Add(e => handler((TEvent)e));
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : class
        {
            if (_handlers.ContainsKey(typeof(TEvent)))
            {
                foreach (var handler in _handlers[typeof(TEvent)])
                {
                    await handler(@event);
                }
            }
        }
    }
}
