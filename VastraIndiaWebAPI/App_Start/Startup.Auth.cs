using Owin;

namespace VastraIndiaWebAPI
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            //app.UseWindowsAzureActiveDirectoryBearerAuthentication(
            //    new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            //    {
            //        Tenant = ConfigurationManager.AppSettings["Tenant"],
            //        TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidAudience = ConfigurationManager.AppSettings["Audience"]
            //        },
            //    });
        }
    }
}
