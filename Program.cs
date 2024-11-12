using CelularesAPI.Config;
using CelularesAPI.Repositories;
using CelularesAPI.Services;
using CelularesAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var JWTSettings = builder.Configuration.GetSection("JWTSettings");

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional:  false, reloadOnChange: true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CelularesAPI",
        Description = "API de celulares"
    });
    options.AddSecurityDefinition("Token", new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        Description = "JWT Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Name = "Authorize",
        Scheme = "bearer"
    });
    options.OperationFilter<AuthFilter>();
});

builder.Services.AddScoped<CelularServices>();
builder.Services.AddScoped<MarcaServices>();
builder.Services.AddScoped<SistemaServices>();
builder.Services.AddScoped<UsuarioServices>();
builder.Services.AddScoped<ColorServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IHashingServices, HashingServices>(); 
builder.Services.AddScoped<RolServices>();

builder.Services.AddScoped<ICelularRepository, CelularRepository> ();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<ISistemaRepository, SistemaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IColoresCelularRepository, ColoresCelularRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateConverter());
});

builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings["Secret"])),

    };
});

builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
