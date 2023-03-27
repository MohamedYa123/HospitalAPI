using Microsoft.AspNetCore.Hosting;
namespace HospitalAPI
{
    public class LambdaFunction : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        [Obsolete]
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<IStartup>()
                .UseLambdaServer();
        }
    }
}
