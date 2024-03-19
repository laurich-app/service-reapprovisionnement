using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Dtos.Rabbits;
using Microsoft.Extensions.Logging;

namespace services {
    public class RabbitMQProducerService {
					private readonly ILogger<RabbitMQProducerService> _logger;
				public RabbitMQProducerService(ILogger<RabbitMQProducerService> logger)
				{
        		_logger = logger;
				}

        public void SendStockReappro(ProduitReapprovisionnementDTO message) {
						_logger.LogInformation("Stock doit être réapprovisionné : " + message);
						using (var connection = RabbitListener.factory.CreateConnection()){
							using (var channel = connection.CreateModel()){
								//Serialize the message
								var json = JsonConvert.SerializeObject(message);
								var body = Encoding.UTF8.GetBytes(json);

								_logger.LogInformation("Publication du message");
								//put the data on to the product queue
								channel.BasicPublish(exchange: "reapprovisionnement.stock.reappro", routingKey: "reapprovisionnement.stock.reapproKey", body: body);
							}
						}
        }
    }
}