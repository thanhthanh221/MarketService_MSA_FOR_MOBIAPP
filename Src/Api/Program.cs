using Market.Application.Products.Queries.GetProductPagging;
using Market.Infrastructure.Dependency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>
{
    option.SuppressAsyncSuffixInActionNames = false; //Phương thức xóa bỏ hậu tố bất đồng bộ (Async)
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(GetProductPaggingQuery).Assembly));
builder.Services.InstallerServiceInAssembly(builder.Configuration);

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
