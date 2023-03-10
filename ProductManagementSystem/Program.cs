using Contracts;
using MassTransit;
using ProductManagementSystem.Consumer;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Product Management System");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReceiveRequestConsumer>();
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        //cfg.ReceiveEndpoint(c =>
        //{
        //    c.ConcurrentMessageLimit = 1;
        //});
        cfg.UseConcurrencyLimit(1);

        // cfg.Host(builder.Configuration.GetValue<string>("RabbitConnection"));
        cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
        {
            hst.Username(Constants.UserName);
            hst.Password(Constants.Password);
        });
        cfg.ConfigureEndpoints(context);
    });

    x.AddRequestClient<ProductRequest>();
});


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

app.Run();
