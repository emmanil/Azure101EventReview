using EventReview.Shared.Models;
using EventReview.Shared.Services;
using Microsoft.Azure.Cosmos;

namespace EventReview.BackendAPI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IRepository<Event>>(provider =>
            {
                string endpointUri = "https://cosff33f9393.documents.azure.com:443/";
                string primaryKey = "KzEqHqR3qlpKbcpqv7fCHcLrqBYLhfyh4N7SMVq9GR2YHwJoE5lfADbrRfXaRVuL00L9QbF4PriVACDbOuDp3w==";
                var client = new CosmosClient(endpointUri, primaryKey);

                var databaseName = "reviewDB";
                var containerName = "reviewContainer";

                var database = client.CreateDatabaseIfNotExistsAsync(databaseName).GetAwaiter().GetResult();

                var container = database.Database.CreateContainerIfNotExistsAsync(containerName, "/partitionKey").GetAwaiter().GetResult();

                return new CosmosDbRepository<Event>(container.Container);
            });

            builder.Services.AddControllers();
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


            app.MapControllers();

            app.Run();
        }
    }
}