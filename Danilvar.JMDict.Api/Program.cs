
using Danilvar.JMDict.Api.Context;
using Danilvar.JMDict.Api.Endpoints;
using Danilvar.JMDict.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSql")));
    


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


builder.Services.AddScoped<DictionaryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(policy =>
        policy.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader()
);
/*const string jsonFilePath = "C:\\Users\\danil\\RiderProjects\\Danilvar.JMDict.Api\\jmdict-rus-3.5.0.json";
string jsonString = File.ReadAllText(jsonFilePath);

var data = JsonConvert.DeserializeObject<JMDictData>(jsonString);
using var context = new AppDbContext();
if (data != null)
    context.JmdictDatas.Add(data);
context.SaveChanges();*/
app.UseHttpsRedirection();

app.MapDictionaryEndpoints();

app.Run();