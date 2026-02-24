var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStockLogic, StockLogic>();
builder.Services.AddScoped<IMakeLogic, MakeLogic>();
builder.Services.AddScoped<ICityLogic, CityLogic>();

builder.Services.AddScoped<StockDataAccess>();
builder.Services.AddScoped<MakeDataAccess>();
builder.Services.AddScoped<CityDataAccess>();

builder.Services.AddScoped<IFiltersTranslator, FiltersTranslator>();
builder.Services.AddSingleton<StockMapper>();
builder.Services.AddSingleton<MakeMapper>();
builder.Services.AddSingleton<CityMapper>();
builder.Services.AddSingleton<FiltersMapper>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.Run();
