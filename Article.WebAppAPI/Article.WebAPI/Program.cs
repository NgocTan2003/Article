using Amazon.S3;
using Article.Application.AutoConfiguration;
using Article.Application.Repositories.Implements;
using Article.Application.Repositories.Interfaces;
using Article.Application.Services.Implements;
using Article.Application.Services.Interfaces;
using Article.Data.EF;
using Article.Data.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// cấu hình Swangger 
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT" // Thêm định dạng token
    });
    // Thêm yêu cầu bảo mật cho token
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddIdentity<ArticleAppUser, ArticleAppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add config for Required Email(bắt buộc phải xác minh email cho lần đầu login)
builder.Services.Configure<IdentityOptions>(opts => opts.SignIn.RequireConfirmedEmail = true);

// set thời gian sống mã OTP cho dvu gửi mail 
builder.Services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = TimeSpan.FromMinutes(3));

builder.Services.Configure<IdentityOptions>(options =>
{
    // khóa khi login fail 5 lần
    options.Lockout.MaxFailedAccessAttempts = 5;
    // tự động mở khóa sau 5p
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddTransient<IArticleUserRepository, ArticleUserRepository>();
builder.Services.AddTransient<IArticleUserService, ArticleUserService>();
builder.Services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
builder.Services.AddScoped<IArticleSubCategoryRepository, ArticleSubCategoryRepository>();
builder.Services.AddScoped<IArticleSubCategoryService, ArticleSubCategoryService>();
builder.Services.AddScoped<IArticleRoleRepository, ArticleRoleRepository>();
builder.Services.AddScoped<IArticleRoleService, ArticleRoleService>();
builder.Services.AddScoped<IArticleFunctionRepository, ArticleFunctionRepository>();
builder.Services.AddScoped<IArticleFunctionService, ArticleFunctionService>();
builder.Services.AddScoped<IArticlePermissionRepository, ArticlePermissionRepository>();
builder.Services.AddScoped<IArticlePermissionService, ArticlePermissionService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IAwsS3Service, AwsS3Service>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
          .AddJwtBearer(options =>
          {
              options.RequireHttpsMetadata = false;
              options.SaveToken = true;
              options.TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidateIssuer = true,
                  ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                  ValidateAudience = true,
                  ValidAudience = builder.Configuration["JWT:ValidIssuer"],
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  ClockSkew = System.TimeSpan.Zero,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
              };
          });

// awsS3
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

// Thêm dịch vụ CORS vào container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
