using GITHUBCommitAnalyzer.BaseUtilities;
using GITHUBCommitAnalyzer.Interfaces;
using GITHUBCommitAnalyzer.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IBaseResponse<>), typeof(BaseResponse<>));
builder.Services.AddScoped<ICommitAnalyzerService, CommitAnalyzerService>();
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
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
