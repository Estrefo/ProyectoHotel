using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TEST.Components;
using TEST.Controllers.Habitaciones;
using TEST.Controllers.Reservas;
using TEST.Controllers.Servicios;
using TEST.Controllers.ServiciosUsuarios;
using TEST.Identity;
using TEST.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var conn= builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<MyIdentityDbContext>(e => e.UseSqlServer(conn));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentity<Usuario, Rol>(
    
    
    options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;

        options.SignIn.RequireConfirmedEmail = false;

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MyIdentityDbContext>()
    .AddApiEndpoints();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IReservasController, ReservasController>();
builder.Services.AddScoped<IHabitacionesController, HabitacionesController>();
builder.Services.AddScoped<IServiciosController, ServiciosController>();
builder.Services.AddScoped<IServiciosUsuariosController, ServiciosUsuariosController>();

builder.Services.AddHealthChecks();


var app = builder.Build();

app.MapIdentityApi<Usuario>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
