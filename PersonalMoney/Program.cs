using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalMoney.Models;
using PersonalMoney.Services;
using ExceptionHandling.CustomMiddlewares;
using NLog;
using NLog.Web;
using PersonalMoney.Hubs;
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddDbContext<PersonalMoneyContext>(
        optionsAction =>
        {
            optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect"));
        }
        );

    /*builder.Services.AddDefaultIdentity<User>()
        .AddEntityFrameworkStores<PersonalMoneyContext>();*/
    builder.Services.AddOptions();
    builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
    builder.Services.AddSingleton<IEmailSender, SendMailService>();
    builder.Services.AddSignalR();
    builder.Services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<PersonalMoneyContext>()
        .AddDefaultTokenProviders();
    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.AllowedForNewUsers = true;

        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
        options.SignIn.RequireConfirmedPhoneNumber = false;
    });
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/auth/signin";
        options.LogoutPath = "/auth/signout";
        options.AccessDeniedPath = "/403";
    });

    builder.Services.AddAuthentication()
    .AddGoogle(op =>
    {
        var gConfig = builder.Configuration.GetSection("Authentication:Google");
        op.ClientId = gConfig["ClientId"];
        op.ClientSecret = gConfig["ClientSecret"];
        op.CallbackPath = "/signin-google";
    })
    .AddFacebook(op =>
    {
        var fConfig = builder.Configuration.GetSection("Authentication:Facebook");
        op.ClientId = fConfig["ClientId"];
        op.ClientSecret = fConfig["ClientSecret"];
        op.CallbackPath = "/signin-facebook";
    });


    var app = builder.Build();
    using (var scope = app.Services.CreateScope())
    {
        await RolesData.SeedRoles(scope.ServiceProvider);
    }
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
    app.UseAuthentication();
    app.UseAuthorization();
    // app.UseStatusCodePagesWithRedirects("/error/{0}");
/*    app.UseMiddleware<ExceptionHandlingMiddleware>();
*/
    app.MapRazorPages();
    app.MapHub<SignalrServer>("/ws-server");
    app.Run();

}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{

    NLog.LogManager.Shutdown();
}

