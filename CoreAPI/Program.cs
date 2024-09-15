using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("V1", new OpenApiInfo { Title = "TokenAPI", Version = "V1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { Name = "Authrization", Type = SecuritySchemeType.ApiKey, Scheme = "Bearer", BearerFormat = "Jwt", In = ParameterLocation.Header, Description = "Jwt Authorization token in header" });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {{
    new OpenApiSecurityScheme {
        Reference = new OpenApiReference {
            Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    }, new string[] { } } });
});

//builder.Services.AddTransient<IConfiguration, ConfigurationBinder>();    


var myConfigSetting = builder.Configuration.GetValue<string>("Jwt:Issuer");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = myConfigSetting,// app.Configuration["Jwt:Issuer"],
        ValidAudience = myConfigSetting,//app.Configuration["Jwt:Issuer"],
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(app.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddMvc();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json"; // Customize the route
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
