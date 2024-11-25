namespace APIPJF;



public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        
        // Redis-Verbindung herstellen
        var logService = new LogService("localhost:6379");

        // UserManager mit Logging
        var userManager = new UserManager(logService);

        using (UserDbContext context = new UserDbContext())
        {
            // Testbenutzer 1
            context.Users.Add(new User
            {
                FirstName = "Max",
                LastName = "Mustermann",
                Department = "IT",
                Email = "max.mustermann@example.com",
                Tel = "123456789",
                Rights = 1,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            // Testbenutzer 2
            context.Users.Add(new User
            {
                FirstName = "Anna",
                LastName = "Schmidt",
                Department = "HR",
                Email = "anna.schmidt@example.com",
                Tel = "987654321",
                Rights = 2,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            // Testbenutzer 3
            context.Users.Add(new User
            {
                FirstName = "John",
                LastName = "Doe",
                Department = "Finance",
                Email = "john.doe@example.com",
                Tel = "555666777",
                Rights = 3,
                IsActive = false, // Benutzer deaktiviert
                CreatedAt = DateTime.UtcNow
            });
            // Änderungen speichern
            context.SaveChanges();
        }
        

        

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        using (UserDbContext context = new UserDbContext())
        {
            
        }

        app.MapPost("/users", (HttpContext httpContext) =>
        {
            UserManager u = new UserManager(logService);
            return u.EditUser(0, null, null, null, null, null, null);
            
        });
        app.MapGet("/users", (HttpContext httpContext) => 
            {
                UserManager u = new UserManager(logService);
                
                return u.ListUsers();

    
            })
            
            .WithName("SIMS")
            .WithOpenApi();

        app.Run();
    }

    
    
    
    
}