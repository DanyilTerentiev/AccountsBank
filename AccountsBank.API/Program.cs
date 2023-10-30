using AccountsBank.BLL.Consumers;
using AccountsBank.BLL.ServiceCollectionExtension;
using AccountsBank.DAL.ServiceCollectionExtension;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddMassTransit(configurator =>
{
    configurator.SetKebabCaseEndpointNameFormatter();

    configurator.AddConsumer<UserDeletedConsumer>();
    
    configurator.UsingRabbitMq((context, factoryConfigurator) =>
    {
        factoryConfigurator.Host("amqp://guest:guest@localhost:5672");
        
        factoryConfigurator.ConfigureEndpoints(context);

    });
    
});

// MassTransit-RabbitMQ Configuration
// builder.Services.AddMassTransit(config => {config.AddConsumer<UserDeletedConsumer>();
//     config.UsingRabbitMq((ctx, cfg) => {
//         cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
//  
//         cfg.ReceiveEndpoint("basketcheckout-queue", c => {
//             c.ConfigureConsumer<UserDeletedConsumer>(ctx);
//         });
//     });
// });

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