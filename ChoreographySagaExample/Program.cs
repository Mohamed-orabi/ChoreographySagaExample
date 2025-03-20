
namespace ChoreographySagaExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register the Event Bus
            builder.Services.AddSingleton<EventBus>();

            // Register Services
            builder.Services.AddSingleton<OrderService>();
            builder.Services.AddSingleton<PaymentService>();
            builder.Services.AddSingleton<InventoryService>();

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            // Start the saga by creating an order
            var orderService = app.Services.GetRequiredService<OrderService>();
            var order = new Order { CustomerId = Guid.NewGuid(), TotalAmount = 100.00m };
            orderService.CreateOrder(order).Wait();

            app.Run();
        }
    }
}
