using Domain.Contracts;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstraction;
using Services.Mapping_Profiles;
using Shared.ErrorModels;
using StoreApp.API.Extensions;
using StoreApp.API.Middlewares;
using AssemblyMapping= Services.AssemblyRefrence;

namespace StoreApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            
            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
