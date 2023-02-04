using Contracts;
using MassTransit;

namespace ProductManagementSystem.Consumer
{
    public class ReceiveRequestConsumer : IConsumer<ProductRequest>
    {
        private readonly IRequestClient<ProductRequest> _client;
        //private readonly ISendEndpointProvider _sendEndpointProvider;
        //private readonly IPublishEndpoint _publishEndpoint;
        public ReceiveRequestConsumer(IRequestClient<ProductRequest> client)
        {
            _client = client;
            //_publishEndpoint = publishEndpoint;
            //_sendEndpointProvider = sendEndpointProvider;
        }
        public async Task Consume(ConsumeContext<ProductRequest> context)
        {

            //var sendToUrl = new Uri($"rabbitmq://localhost/{ Constants.SendServiceQueue}");

            //var endpoint = await _sendEndpointProvider.GetSendEndpoint(sendToUrl);

            //await endpoint.Send<Product>(new
            //{
            //    Id = context.Message.Id,
            //    ProductName = context.Message.ProductName,
            //    DateTime = context.Message.DateTime,
            //    Amount = context.Message.Amount,
            //});
            //var modelProductRequest = new ProductRequest
            //{
            //    Id = context.Message.Id,
            //    ProductName = context.Message.ProductName,
            //    DateTime = context.Message.DateTime,
            //    Amount = context.Message.Amount,
            //};
            Console.WriteLine($"Vorod {context.Message.Id}");

            var message =await _client.GetResponse<ResponseMessage>(new Product
            {
                Id = context.Message.Id,
                ProductName = context.Message.ProductName,
                DateTime = context.Message.DateTime,
                Amount = context.Message.Amount,
            });

          //  return message.Message.MessageName;
            var m = message.Message.MessageName;
           // var m = message.Result;
            //message.Message.MessageName
            Console.WriteLine($"receive {context.Message.Id}      ====>  ");

            var res = "";

            //var response = await _client.GetResponse<ResponseMessage>(new
            //{
            //    context.Message.Id,
            //    context.Message.ProductName,
            //    context.Message.DateTime,
            //    context.Message.Amount,
            //});

            //await context.Send(new Product
            //{
            //    Id = context.Message.Id,
            //    ProductName = context.Message.ProductName,
            //    DateTime = context.Message.DateTime,
            //    Amount = context.Message.Amount,
            //});

            //  var d = response;

            //await _sendEndpointProvider.Send
            //{

            //};

            //await _publishEndpoint.Publish(new Product
            //{
            //    Id = context.Message.Id,
            //    ProductName = context.Message.ProductName,
            //    DateTime = context.Message.DateTime,
            //    Amount = context.Message.Amount,
            //});

        }
    }
}
