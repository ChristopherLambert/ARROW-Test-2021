using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Arrow.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var _serviceProvider = CreateHostBuilder(args).Build();
            Console.WriteLine("Execute PaymentService ...");

            IServiceScope scope = _serviceProvider.Services.CreateScope();
            MakePaymentRequest request = new MakePaymentRequest();
            scope.ServiceProvider.GetRequiredService<Execute>().MakePay(request);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureServices((_, services) =>
                  services.AddSingleton<IPaymentService, PaymentService>());
    }
}
