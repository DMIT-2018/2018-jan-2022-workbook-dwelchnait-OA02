
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

#region Additional Namespaces
using ChinookSystem;    //Chinook Librart
using AppSecurity.BLL;
using AppSecurity;       //AppSecurity library
#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//given
//supplied database connection due to the fact that we create this
//  web app to use Individual Accounts
//code retreives the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//given
//register the supplied connection string with the IServiceConllection (.Services)
//registers the connection string for Individual Accounts
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//added
//code the logic to add our class library services to IServiceCollection 
//one could do the registration code here in Program.cs
//HOWEVER, every time a service class is added, you would be changing this file
//the implementation of the DbContent and AddTransient(..) code in this example
//      will be done in an extension method to IServiceCollection
//the extension method will be coded inside the ChinookSystem class library
//the extension method will have a parameter: options.UseSqlServer()
builder.Services.ChinookSystemBackendDependencies(options =>
    options.UseSqlServer(connectionString));

//add the registration of the AppSecurity extension class
builder.Services.AppSecurityBackendDependencies(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/SamplePages")
        .AllowAnonymousToPage("/SamplePages/Basics");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapRazorPages();

await ApplicationUserSeeding(app);
app.Run();

static async Task ApplicationUserSeeding(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<Program>();
        var env = services.GetRequiredService<IWebHostEnvironment>();
        if (env is not null && env.IsDevelopment())
        {
            try
            {
                var configuration = services.GetRequiredService<IConfiguration>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                if (!userManager.Users.Any())
                {
                    var securityService = services.GetRequiredService<SecurityService>();
                    var users = securityService.ListEmployees();
                    string password = configuration.GetValue<string>("Setup:InitialPassword");
                    foreach (var person in users)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = person.UserName,
                            Email = person.Email,
                            EmployeeId = person.EmployeeId,
                            EmailConfirmed = true
                        };
                        var result = await userManager.CreateAsync(user, password);
                        if (!result.Succeeded)
                        {
                            logger.LogInformation("User was not created");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred seeing the website users");
            }
        }
    }
}
