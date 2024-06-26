using BusinessLogic.Team;
using BusinessLogic.User;
using BusinessLogic.Player;
using BusinessLogic.Group;
using BusinessLogic.Game;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.MappingProfiles;
using BusinessLogic.Edition;
using BusinessLogic.Category;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Dependencies

builder.Services.AddScoped<IUserBll, UserBLL>();
builder.Services.AddScoped<ITeamBll, TeamBLL>();
builder.Services.AddScoped<IPlayerBll, PlayerBLL>();
builder.Services.AddScoped<IGroupBll, GroupBLL>();
builder.Services.AddScoped<IGameBll, GameBLL>();
builder.Services.AddScoped<IEditionBll, EditionBLL>();
builder.Services.AddScoped<ICategoryBll, CategoryBLL>();

#endregion

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "KingOfTheTower3x3 Backend Management",
        Description = "API REST on .NET 6 to manage different spaces of a basketball tournament",
        Contact = new OpenApiContact
        {
            Name = "José Ramón",
            Email = "jrgxllego@proton.me",
            Url = new Uri("https://jrwithahoodie.github.io/react-portfolio-ui")
        }
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KingOfTheTower3x3 Backend Management API V1");
    });
}

app.UseAuthorization();

app.UseCors(builder =>
    builder.AllowAnyOrigin()
    .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
    .WithHeaders("Content-Type", "Authorization", "Content-Length", "X-Requested-With", "Origin")
    .WithExposedHeaders("Location"));

app.MapControllers();

app.Run();
