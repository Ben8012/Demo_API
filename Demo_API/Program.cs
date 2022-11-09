using System.Data.SqlClient;
using System.Reflection;
using Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// ajoute de la connectionString

string connectionString = builder.Configuration.GetConnectionString("API_DEMO");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// service de connection a la base de donnée
builder.Services.AddTransient<Connection>(sp => new Connection(SqlClientFactory.Instance,connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
