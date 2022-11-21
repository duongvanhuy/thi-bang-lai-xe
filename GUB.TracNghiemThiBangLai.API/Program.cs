using GUB.TracNghiemThiBangLai.API.Mapper;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddControllersWithViews();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GUBDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GUBTracNghiemLaiXe")));

builder.Services.AddAutoMapper(typeof(AccountProfile));
/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                policy =>
                {
                    policy.WithOrigins(
                        "http://0.0.0.0:5233",
                       
                        "http://localhost:*",
                        "*"
                        ).AllowAnyHeader().AllowAnyMethod();

                });
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

/*app.UseCors("MyPolicy");*/
app.UseAuthorization();

app.MapControllers();
app.Run();
