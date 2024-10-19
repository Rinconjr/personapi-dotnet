using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using personapi_dotnet.Repository;
using personapi_dotnet.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios con vistas y JSON, ignorando ciclos de referencia
builder.Services.AddControllersWithViews()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        opts.JsonSerializerOptions.WriteIndented = false;  // Opcional: elimina la indentación para respuestas JSON más compactas
    });

// Registrar repositorios e interfaces
builder.Services.AddTransient<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IEstudiosRepository, EstudiosRepository>();
builder.Services.AddScoped<IProfesionRepository, ProfesionRepository>();
builder.Services.AddScoped<ITelefonoRepository, TelefonoRepository>();

// Configurar DbContext con la cadena de conexión
builder.Services.AddDbContext<ArqPerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Persona API", Version = "v1" });
});

var app = builder.Build();

// Configurar el middleware para manejo de errores y HSTS en producción
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Habilitar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Persona API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

// Comentado para evitar problemas de redirección en Docker (solo usar HTTP en Docker)
// app.UseHttpsRedirection();

app.UseStaticFiles();  // Sirve archivos estáticos como CSS, JS, imágenes, etc.

app.UseRouting();      // Habilita el enrutamiento

app.UseAuthorization();  // Habilita la autorización

// Redirigir automáticamente la raíz a /home
app.MapGet("/", context =>
{
    context.Response.Redirect("/home");
    return Task.CompletedTask;
});

// Mapea las rutas a los controladores
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecuta la aplicación
app.Run();
