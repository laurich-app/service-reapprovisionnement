using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using services;
using Dtos.Rabbits;
using Newtonsoft.Json;

namespace services {
	public class RabbitListener
{
		private string host = Environment.GetEnvironmentVariable("RABBIT_HOST") ?? "localhost";
		private string port = Environment.GetEnvironmentVariable("RABBIT_PORT") ?? "5672";
		private string user = Environment.GetEnvironmentVariable("RABBIT_USER") ?? "guest";
		private string pswd = Environment.GetEnvironmentVariable("RABBIT_PSWD") ?? "guest";

    public static ConnectionFactory factory { get; set; }
    IConnection connection { get; set; }
    IModel channel { get; set; }

		private readonly BonDeCommandeService _bonDeCommandeService;

    public void Register()
    {
			this.registerStockSupprimer();
			this.registerStockManquant();
		}

		private void registerStockManquant()
		{
				channel.ExchangeDeclare("catalogue.stock.manquant", ExchangeType.Direct, durable: true);
				//declare the queue after mentioning name and a few property related to that
				channel.QueueDeclare("reapprovisionnement.stock.manquant", exclusive: false, durable: true, autoDelete: false);
				channel.QueueBind("reapprovisionnement.stock.manquant", "catalogue.stock.manquant", "catalogue.stock.manquantKey");
				
				//Set Event object which listen message from chanel which is sent by producer
				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += (model, eventArgs) => {
						var body = eventArgs.Body.ToArray();
						var message = Encoding.UTF8.GetString(body);

						// Désérialiser le message en un objet
    				var produit = JsonConvert.DeserializeObject<ProduitCatalogueManquantDTO>(message);

						this._bonDeCommandeService.StockReapproDemande(produit);
				};
				//read the message
				channel.BasicConsume(queue: "reapprovisionnement.stock.manquant", autoAck:true, consumer: consumer, exclusive: false);

		}

		private void registerStockSupprimer()
		{
				channel.ExchangeDeclare("catalogue.stock.supprimer", ExchangeType.Direct, durable: true);
				//declare the queue after mentioning name and a few property related to that
				channel.QueueDeclare("reapprovisionnement.stock.supprimer", exclusive: false, durable: true, autoDelete: false);
				channel.QueueBind("reapprovisionnement.stock.supprimer", "catalogue.stock.supprimer", "catalogue.stock.supprimerKey");
				
				//Set Event object which listen message from chanel which is sent by producer
				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += (model, eventArgs) => {
						var body = eventArgs.Body.ToArray();
						var message = Encoding.UTF8.GetString(body);

						// Désérialiser le message en un objet
    				var produit = JsonConvert.DeserializeObject<ProduitCatalogueDTO>(message);

						this._bonDeCommandeService.SupprimerCommande(produit.id_produit, produit.couleur);
				};
				//read the message
				channel.BasicConsume(queue: "reapprovisionnement.stock.supprimer", autoAck:true, consumer: consumer, exclusive: false);
		}

    public void Dispose()
    {
        this.connection.Close();
    }

    public RabbitListener(BonDeCommandeService bonDeCommandeService)
    {
				_bonDeCommandeService = bonDeCommandeService;
        RabbitListener.factory = new ConnectionFactory {
						HostName = host,
						UserName = user,
						Password = pswd,
						Port     = int.Parse(port),
						ClientProvidedName = "Service Reapprovisionnement",
						ClientProperties = new Dictionary<string, object>
						{
								{ "product", "service-reapprovisionnement" },
								{ "version", "1.1.1" }
						}
				};
        this.connection = RabbitListener.factory.CreateConnection();
        this.channel = connection.CreateModel();
    }
}
}