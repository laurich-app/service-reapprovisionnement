using services;

namespace Extensions {
	public static class ApplicationBuilderExtensions
	{
			public static ConsulService Service { get; set; }

			public static IApplicationBuilder ConsulRegister(this IApplicationBuilder app)
			{
					//design ConsulService class as long-lived or store ApplicationServices instead
					Service = app.ApplicationServices.GetService<ConsulService>();

					var life = app.ApplicationServices.GetService<Microsoft.AspNetCore.Hosting.IApplicationLifetime>();

					life.ApplicationStarted.Register(OnStarted);
					life.ApplicationStopping.Register(OnStopping);

					return app;
			}

			private static void OnStarted()
			{
					Service.Register(); //finally, register the API in Consul
			}

			private static void OnStopping()
			{
				Service.Dispose();
			}
	}
}