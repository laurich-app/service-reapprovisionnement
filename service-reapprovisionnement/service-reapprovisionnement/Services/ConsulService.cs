using Consul;
using Microsoft.Extensions.Options;
using Dtos;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Net.NetworkInformation;

namespace services {

public class ConsulService : IDisposable
{
		private static string SERVICE_ID;
    private string consulHost = Environment.GetEnvironmentVariable("CONSUL_HOST") ?? "localhost";
    private string consulPort = Environment.GetEnvironmentVariable("CONSUL_PORT") ?? "8500";

    public ConsulService(IOptions<ConsulOptions> optAccessor) { }

    public void Register() 
    {
			var registration = new AgentServiceRegistration
				{
						ID = Guid.NewGuid().ToString(), // Unique ID for the service
						Name = "reapprovisionnement",
						Port = 5200,
						Address = Dns.GetHostName(),
						Check = new AgentServiceCheck()
						{
								HTTP = "http://" + Dns.GetHostName() + ":5200/health", // URL pour vérifier la santé du service
								Interval = TimeSpan.FromSeconds(10),    // Vérification de la santé toutes les 10 secondes
								Timeout = TimeSpan.FromSeconds(5),     // Délai d'attente de 5 secondes pour chaque vérification
						}
				};

				using (var client = new ConsulClient(opt => {
					opt.Address = new Uri("http://"+consulHost+":"+consulPort);
				}))
				{
						var result = client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
						// Si l'enregistrement a réussi
						if (result.StatusCode == HttpStatusCode.OK)
						{
								// Récupérer l'ID du service enregistré
								ConsulService.SERVICE_ID = registration.ID;
								Console.WriteLine($"Service ID: {ConsulService.SERVICE_ID}");
						}
						else
						{
								Console.WriteLine("Failed to register service.");
						}
				}
    }

		// Implémentation de la méthode Dispose de l'interface IDisposable
    public void Dispose()
    {
        // Code pour nettoyer les ressources non managées, si nécessaire
        Console.WriteLine("Disposed ConsulService.");
				using (var client = new ConsulClient(opt => {
					opt.Address = new Uri("http://"+consulHost+":"+consulPort);
				}))
				{
						// TO DO
						string serviceId = ConsulService.SERVICE_ID;
						client.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
				}
    }
}
}