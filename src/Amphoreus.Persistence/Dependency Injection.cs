

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Amphoreus.Persistence;

// Métodos de extensión para registrar los servicios relacionados con la
// persistencia en el contenedor DI de la aplicación. Mantén este archivo
// pequeño: el método que sigue configura el DbContext y futuros ayudantes
// de persistencia (repositorios, unit-of-work, seeders, etc.).
public static class Dependency_Injection
{

        /// Registra los servicios de la capa de persistencia en el
        /// <see cref="IServiceCollection"/> de la aplicación.
        ///
        /// - Añade <see cref="AmphoreusDBContext"/> al contenedor DI.
        /// - Usa por defecto la cadena de conexión llamada "sqliteDatabase"
        ///   obtenida desde <see cref="IConfiguration"/>.
        ///
        /// Uso: en Program.cs o Startup.cs llamar a
        /// services.AddPersistence(Configuration);
        ///
        /// Notas / consejos:
        /// - AddDbContext registra el DbContext con tiempo de vida Scoped
        ///   por defecto, que es el recomendado en aplicaciones web.
        /// - Si cambias de proveedor (por ejemplo a PostgreSQL), reemplaza
        ///   UseSqlite por UseNpgsql y añade el paquete proveedor adecuado
        ///   (por ejemplo Npgsql.EntityFrameworkCore.PostgreSQL) al proyecto.
        /// - Para las herramientas de EF (migrations) el proyecto de
        ///   persistencia debería incluir Microsoft.EntityFrameworkCore.Design
        ///   como PackageReference (o que el paquete de diseño esté disponible
        ///   en el proyecto que contiene el DbContext).
        /// - La clave de la cadena de conexión usada aquí es "sqliteDatabase";
        ///   actualiza appsettings.json o variables de entorno según convenga.
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration Configuration)
        {
                // Registrar el DbContext de la aplicación. Este ejemplo usa SQLite
                // y obtiene la cadena de conexión llamada "sqliteDatabase" desde
                // IConfiguration (appsettings.json, variables de entorno, etc.).
                services.AddDbContext<AmphoreusDBContext>(opt =>
                {
                                // UseSqlite es la llamada al proveedor para SQLite.
                                // Comprobamos varias ubicaciones de configuración para mantener compatibilidad:
                                // 1) ConnectionStrings:sqliteDatabase (GetConnectionString)
                                // 2) ColletionDatabaseSettings:SqliteDatabase (clave existente en algunos appsettings)
                                var connectionString = Configuration.GetConnectionString("sqliteDatabase")
                                                       ?? Configuration["ColletionDatabaseSettings:SqliteDatabase"]
                                                       ?? Configuration["ConnectionStrings:sqliteDatabase"];

                                if (string.IsNullOrWhiteSpace(connectionString))
                                {
                                    throw new InvalidOperationException("No se encontró la cadena de conexión 'sqliteDatabase'. Añade ConnectionStrings:sqliteDatabase o ColletionDatabaseSettings:SqliteDatabase en appsettings.");
                                }

                                opt.UseSqlite(connectionString);
                });

                // Si más adelante añades repositorios u otros ayudantes de
                // persistencia, regístralos aquí. Ejemplo:
                // services.AddScoped<IRepository, Repository>();

                return services;
        }
}
