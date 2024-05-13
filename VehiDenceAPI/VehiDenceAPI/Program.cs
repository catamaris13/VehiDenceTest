using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NETCore.MailKit;
using NETCore.MailKit.Core;
using NETCore.MailKit.Infrastructure.Internal;
using User.Management.Service.Models;
using User.Management.Service.Services;
using VehiDenceAPI.Data;
using VehiDenceAPI.Models;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;



builder.Services.AddDbContext<AplicatieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VehiDenceConnectionString")));



builder.Services.AddScoped<IEmailServices, EmailServices>();

// Add CORS policy
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire((sp,config)=>
{
    var connectionstring = sp.GetRequiredService<IConfiguration>().GetConnectionString("VehiDenceConnectionString");
    config.UseSqlServerStorage(connectionstring);

});
builder.Services.AddHangfireServer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
});
// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();