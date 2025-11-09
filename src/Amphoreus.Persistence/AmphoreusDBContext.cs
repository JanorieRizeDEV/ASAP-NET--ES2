
using Amphoreus.Domain;
using Microsoft.EntityFrameworkCore;

namespace Amphoreus.Persistence;


/// - Define los DbSet que representan las tablas de la base de datos.
/// - Configura relaciones, precisión de columnas y datos semilla en
///   <see cref="OnModelCreating"/>.
/// - Mantén las convenciones de nombres consistentes con las entidades
///   en el espacio de dominio (Amphoreus.Domain).
public class AmphoreusDBContext(DbContextOptions<AmphoreusDBContext> options) : DbContext(options)
{
    // DbSet para categorías. Cada entrada representa una fila en la tabla
    // Categories. Usar plural o singular es una convención del proyecto;
    // aquí se mantiene el nombre original `categories`.
    public required DbSet<Category> categories { get; set; }

    // DbSet para cafés (entidad `Coffe` en el dominio). Representa la tabla
    // Coffes. Se marca como required para indicar que no puede ser nulo
    // cuando se inyecte desde el contenedor DI.
    public required DbSet<Coffe> coffes { get; set; }

    // DbSet para ingredientes utilizados en las recetas de café.
    public required DbSet<Ingredient> ingredients { get; set; }

    /// Configuración del modelo y relaciones entre entidades.
    ///
    /// Comentarios clave:
    /// - Se configura una relación uno-a-muchos entre Category y Coffe.
    /// - La clave foránea en Coffe se indica como <c>cateogoryId</c> (tal
    ///   como está definida en la entidad de dominio).
    /// - La eliminación en cascada está habilitada: al borrar una
    ///   categoría se borran sus cafés asociados.
    /// - Se establece precisión para el campo `price` de Coffe
    ///   (10 dígitos, 2 decimales).
    /// - La relación muchos-a-muchos entre Coffe e Ingredient se modela
    ///   usando la entidad de unión `coffeIngredient` y se define la clave
    ///   compuesta (coffeId, ingredientId).
    /// - Finalmente se insertan datos semilla para Category mediante
    ///   <see cref="GetCategories"/>.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Category 1 - * Coffe
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Coffes)
            .WithOne(co => co.Category)
            .HasForeignKey(co => co.cateogoryId) // nombre de FK definido en la entidad
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade); // borrado en cascada

        // Configuración de precisión para el precio del café
        modelBuilder.Entity<Coffe>()
            .Property(co => co.price)
            .HasPrecision(10, 2); // 10 dígitos totales, 2 decimales

        // Relación muchos-a-muchos entre Coffe e Ingredient mediante la
        // entidad de unión coffeIngredient (tabla intermedia explícita).
        modelBuilder.Entity<Coffe>()
            .HasMany(ing => ing.Ingredients)
            .WithMany(ing => ing.Coffes)
            .UsingEntity<coffeIngredient>(
                // Configuración de la parte que apunta a Ingredient
                j => j
                    .HasOne(p => p.Ingredient)
                    .WithMany(p => p.coffeIngredients)
                    .HasForeignKey(p => p.ingredientId),
                // Configuración de la parte que apunta a Coffe
                j => j
                    .HasOne(p => p.Coffe)
                    .WithMany(p => p.coffeIngredients)
                    .HasForeignKey(p => p.coffeId),
                // Configuración adicional de la entidad de unión
                j =>
                {
                    // Clave compuesta para la tabla de unión
                    j.HasKey(t => new { t.coffeId, t.ingredientId });
                }
            );

        // Insertar datos semilla para Category usando el método auxiliar
        modelBuilder.Entity<Category>()
            .HasData(GetCategories());
    }
    /// Devuelve una colección de categorías (semilla) basada en el enum
    /// <see cref="CategoryEnum"/>. Se usa para poblar la tabla Category
    /// cuando se aplican las migraciones iniciales.
    private IEnumerable<Category> GetCategories()
    {
        // Convierte los valores del enum en instancias de Category mediante
        // el método estático create definido en la entidad.
        return Enum.GetValues<CategoryEnum>()
            .Select(p => Category.create((int)p));
    }
}



