using Owin;
using Serilog;
using Serilog.Extras.Web.Enrichers;

namespace MiniWeb
{
    public static class LogConfig
    {
        public static void Register(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341/")
                .Enrich.WithProperty("System", "Web")
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<UserNameEnricher>()
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}