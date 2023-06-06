
using Microsoft.AspNetCore.Builder;
using DataAccess.Repositories;
using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi;
using Application.Interfaces;
using Application;
using Application.Factories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connstr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddMvc().AddControllersAsServices();
builder.Services.AddDbContext<BikeStoreContext>(options => options.UseSqlServer(connstr));
builder.Services.AddScoped<DbContext, BikeStoreContext>()
    .AddScoped<IUnitOfWork, UnitOfWork>()
    .AddTransient<IGenericRepository<Customer>, GenericRepository<Customer>>()
    .AddTransient<IGenericRepository<Staff>, GenericRepository<Staff>>()
    .AddTransient<IGenericRepository<Stock>, GenericRepository<Stock>>()
    .AddTransient<IGenericRepository<Brand>, GenericRepository<Brand>>()
    .AddTransient<IGenericRepository<Store>, GenericRepository<Store>>()
    .AddTransient<IGenericRepository<Order>, GenericRepository<Order>>()
    .AddTransient<IGenericRepository<OrderItem>, GenericRepository<OrderItem>>()
    .AddTransient<ICreateProducts, ProductCreator>()
    .AddTransient<ICreateOrders, OrderCreator>()
    .AddTransient<ISeedBrands, BrandSeeder>()
    .AddTransient<ISeedCategories, CategorySeeder>()
    .AddTransient<ISeedProducts, ProductSeeder>()
    .AddTransient<ISeedCustomers, CustomerSeeder>()
    .AddTransient<ISeedStores, StoreSeeder>()
    .AddScoped<IStockStrategy, AllStoresStockStrategy>()
    .AddScoped<IStockStrategy, StoreStockStrategy>()
    .AddTransient<IStockStrategyFactory, StockStrategyFactory>();



builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();