using HospitalAPI.siteClasses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCors(p => p.AddPolicy("CorsPolicy",
//    build =>
//    {
//        build.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();//.AllowCredentials();
//        //https://nahda-hospital.vercel.app
//    }
//    ));
builder.Services.AddCors(p => p.AddPolicy("CorsPolicy",
    build =>
    {
        build.WithOrigins("https://nahda-hospital.vercel.app", "http://localhost:5173", "http://127.0.0.1:5173", "http://127.0.0.1:5174").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        //https://nahda-hospital.vercel.app
        //https://nahda-hospital-rfqqzjh12-alieldeba.vercel.app/
    }
    ));
//builder.Services
//            .AddCors(options =>
//            {
//                options.AddPolicy("CorsPolicy",
//                    builder => builder
//                    .AllowAnyOrigin()
//                    .AllowAnyMethod()
//                    .AllowAnyHeader()
//                    );
//                options.AddPolicy("signalr",
//                    builder => builder
//                    .AllowAnyMethod()
//                    .AllowAnyHeader()
//                    .AllowCredentials()
//                    .SetIsOriginAllowed(hostName => true));
//            });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
//app.UseCors("signalr");
app.UseHttpsRedirection();
//app.UseDeveloperExceptionPage();
app.UseAuthorization();

app.MapControllers();
Sitemanager.Main();
app.Run();
