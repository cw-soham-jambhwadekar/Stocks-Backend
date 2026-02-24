var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IMakeRepository, MakeRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddScoped<IStockGrpcLogic, StockGrpcLogic>();
builder.Services.AddScoped<IMakeGrpcLogic, MakeGrpcLogic>();
builder.Services.AddScoped<ICityGrpcLogic, CityGrpcLogic>();

builder.Services.AddScoped<IQueryBuilder, QueryBuilder>();
builder.Services.AddSingleton<StockMapper>();
builder.Services.AddSingleton<MakeMapper>();
builder.Services.AddSingleton<CityMapper>();
builder.Services.AddSingleton<FiltersMapper>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<StockGrpcService>();
app.MapGrpcService<MakeGrpcService>();
app.MapGrpcService<CityGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
