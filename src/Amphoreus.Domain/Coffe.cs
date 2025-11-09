

using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Amphoreus.Domain;
public class Coffe : BaseEntity
{
    /// Nombre para mostrar del café (requerido).
    public required string Name { get; set; }

    /// Descripción opcional del café.
    public string? description { get; set; }

    /// Precio del café en la moneda configurada.
    public decimal price { get; set; }

    /// Clave foránea a la categoría del café.
    /// Mantén la nomenclatura consistente con el resto del dominio.
    public int cateogoryId { get; set; }

    /// Ruta o URL opcional de la imagen del café.
    public string? image { get; set; }

    /// Propiedad de navegación a la <see cref="Category"/> a la que pertenece este café.
    /// Nullable cuando la relación es opcional o no está cargada.
    public Category? Category { get; set; }


    /// Navegación de colección para los ingredientes asociados a este café.
    /// Inicializada como lista vacía para evitar referencias nulas al iterar.
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    /// Navegación de colección para las entidades de unión que enlazan cafés e ingredientes.
    /// Úsala cuando necesites acceder a datos adicionales en la relación.
    public ICollection<coffeIngredient> coffeIngredients { get; set; } = new List<coffeIngredient>();

    /// Colección de cafés relacionados (si el dominio modela agrupaciones de cafés).
    /// Inicializada como lista vacía para evitar comprobaciones de null.
    public ICollection<Coffe> coffes { get; set; } = new List<Coffe>();

}
