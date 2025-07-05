using Eventra.EmailMicroservice.API.Persistance;
using Eventra.EmailMicroservice.API.Repositories;
using Eventra.EmailMicroservice.API.Repositories.Interfaces;
using Eventra.EmailMicroservice.API.Services;
using Eventra.EmailMicroservice.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Use SQL server

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

#region Services

builder.Services.AddScoped<IMailPersistanceRepository, MailPersistanceRepository>();
builder.Services.AddScoped<IMailPersistanceService, MailPersistanceService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();

#endregion

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
