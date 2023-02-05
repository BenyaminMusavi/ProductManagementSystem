using Contracts;
using MassTransit;

namespace ProductManagementSystem.Consumer
{
    public class ReceiveRequestConsumer : IConsumer<Product>
    {
        private readonly IRequestClient<ProductRequest> _client;

        public ReceiveRequestConsumer(IRequestClient<ProductRequest> client)
        {
            _client = client;

        }
        public async Task Consume(ConsumeContext<Product> context)
        {
            try
            {
                Console.WriteLine($"Vorod {context.Message.Id}");

                var model = new ProductRequest
                {
                    Id = context.Message.Id,
                    ProductName = context.Message.ProductName,
                    DateTime = context.Message.DateTime,
                    Amount = context.Message.Amount,
                };


                var message = await _client.GetResponse<ResponseMessage>(new ProductRequest
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    DateTime = model.DateTime,
                    Amount = model.Amount,
                });

                var m = message.Message.MessageName;
                Console.WriteLine($"receive {context.Message.Id}      ====>  ");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
