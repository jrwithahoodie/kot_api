using BusinessLogic.Team;
using BusinessLogic.User;
using BusinessLogic.Player;
using BusinessLogic.Group;
using BusinessLogic.Game;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Dependencies

builder.Services.AddScoped<IUserBll, UserBLL>();
builder.Services.AddScoped<ITeamBll, TeamBLL>();
builder.Services.AddScoped<IPlayerBll, PlayerBLL>();
builder.Services.AddScoped<IGroupBll, GroupBLL>();
builder.Services.AddScoped<IGameBll, GameBLL>();

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors(builder =>
    builder.AllowAnyOrigin()
    .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
    .WithHeaders("Content-Type", "Authorization", "Content-Length", "X-Requested-With", "Origin")
    .WithExposedHeaders("Location"));

app.MapControllers();

app.Run();
