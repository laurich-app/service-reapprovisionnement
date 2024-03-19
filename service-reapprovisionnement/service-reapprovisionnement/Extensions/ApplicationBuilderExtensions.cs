using services;

namespace Extensions {
	public static class ApplicationBuilderExtensions
	{
			public static ConsulService Service { get; set; }
			//the simplest way to store a single long-living object, just for example.
    	private static RabbitListener _listener { get; set; }

			public static IApplicationBuilder ConsulRegister(this IApplicationBuilder app)
			{
					//design ConsulService class as long-lived or store ApplicationServices instead
					Service = app.ApplicationServices.GetService<ConsulService>();
					_listener = app.ApplicationServices.GetService<RabbitListener>();

					var life = app.ApplicationServices.GetService<Microsoft.AspNetCore.Hosting.IApplicationLifetime>();

					life.ApplicationStarted.Register(OnStarted);
					life.ApplicationStopping.Register(OnStopping);

					return app;
			}

			private static void OnStarted()
			{
					Service.Register(); //finally, register the API in Consul
					_listener.Register();
			}

			private static void OnStopping()
			{
				Service.Dispose();
				_listener.Dispose();
			}
	}
}