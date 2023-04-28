using CRUDWebAPIYoutube.BusinessLogic;
using CRUDWebAPIYoutube.Data;
using CRUDWebAPIYoutube.Mappings;
using CRUDWebAPIYoutube.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Logs.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Information()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IDetailRepository, DetailRepository>();
builder.Services.AddScoped(typeof(ImageWebBridge));
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
//builder.Services.TryAddScoped<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
//add another authdbcontext
builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDbCon")));


builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("")
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options => { 
    options.Password.RequireDigit=false;
    options.Password.RequiredLength=1;
    options.Password.RequireUppercase=false;
    options.Password.RequireNonAlphanumeric=false;
    options.Password.RequireLowercase=false;
    options.Password.RequireUppercase=false;
    options.Password.RequireDigit = false;


});
//jwt tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=> options.TokenValidationParameters = new TokenValidationParameters { 
    ValidateIssuer=true,
    ValidateAudience=true,
    ValidateLifetime=true,
    ValidIssuer= builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    ////to disable swagger
    // app.UseDefaultFiles();
    // app.UseStaticFiles();
    //to enable swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Images")),
    RequestPath = "/Images"
    
    
 }) ;

app.MapControllers();

app.Run();
