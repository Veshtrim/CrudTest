using Crud.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;


namespace Crud.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CrudContext(
                serviceProvider.GetRequiredService<DbContextOptions<CrudContext>>())) {

                if (context.Puntori.Any())
                {
                    return;
                }
                context.Puntori.AddRange(
                    new Puntori
                    { 
                       Name = "Filan",
                       Description = "Test",
                       Departament = "IT"
                    
                    },
                    new Puntori
                    {
                        Name = "Filan2",
                        Description = "Test2",
                        Departament = "Cleaner"

                    },
                    new Puntori
                    {
                        Name = "Filan2",
                        Description = "Test2",
                        Departament = "Security"

                    }

                    );
                context.SaveChanges();
            }
           
        }
    }
}
