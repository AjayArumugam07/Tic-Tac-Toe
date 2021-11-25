using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tic_Tac_Toe.Data
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                MigrateData(serviceScope.ServiceProvider.GetService<GamesDBContext>());
            }
        }

        public static void MigrateData(GamesDBContext context)
        {
            System.Console.WriteLine("Applying Migrations...");

            context.Database.Migrate();
        }
    }
}
