var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

IServiceCollection services = builder.Services;
services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            // https://stackoverflow.com/questions/42859101/asp-net-core-cross-origin-request-to-services-preflight-returns-code-204-inst
            builder
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); 
app.UseCors();
app.Run();
