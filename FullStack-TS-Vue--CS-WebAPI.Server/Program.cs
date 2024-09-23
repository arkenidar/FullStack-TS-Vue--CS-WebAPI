using FullStack_TS_Vue__CS_WebAPI.Server.Controllers;

using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read the connection string from appsettings.json
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionForSqlite");
// "Data Source=books.db"

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=books.db", b => b.MigrationsAssembly("FullStack-TS-Vue--CS-WebAPI.Server"))); // DATABASE

/*
builder.Services.AddDbContext<AppDbContext>b => b.MigrationsAssembly("FullStack-TS-Vue--CS-WebAPI.Server")(

// options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

// disabled see library project for database
// ( C:\Users\dario\source\repos\DataAccess\AppDbContext.cs )
options => options.UseSqlite("Data Source=books.db")

	// dotnet add package Microsoft.EntityFrameworkCore.Sqlite
	
	// dotnet ef migrations add InitialCreate
	// dotnet ef database update
);
*/

            var factory = new BookContextFactory();

            using var context = factory.CreateDbContext(args);

            // Ensure database is created and seeded
            context.Database.EnsureCreated();
			
			if (!context.Books.Any())
            {
                var orwell = new Author("George Orwell");
                var huxley = new Author("Aldous Huxley");
                var bradbury = new Author("Ray Bradbury");

                context.Authors.AddRange(new List<Author> { orwell, huxley, bradbury });
                context.SaveChanges();

                context.Books.AddRange(new List<Book>
                {
                    new ("1984", orwell.Id, 1949),
                    new ("Animal Farm", orwell.Id, 1945),
                    new ("Brave New World", huxley.Id, 1932),
                    new ("Fahrenheit 451", bradbury.Id, 1953),
                    new ("The Martian Chronicles", bradbury.Id, 1950)
                });

                context.SaveChanges();
            }

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.MapBookEndpoints(); // !important

app.Run();
