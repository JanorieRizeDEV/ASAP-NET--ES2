namespace Amphoreus.Domain;

public class Ingredient : BaseEntity
{
    /// Nombre para mostrar del ingrediente (requerido).
    public required string Name { get; set; }

    /// Descripción opcional en texto libre del ingrediente.
    /// Mantén la propiedad como nullable cuando no haya descripción disponible.
    public string? description { get; set; }

    /// Propiedad de navegación: los cafés que contienen este ingrediente.
    /// Es una navegación de colección usada por EF Core para la relación muchos-a-muchos / uno-a-muchos.
    /// Inicializada como lista vacía para evitar comprobaciones de null al iterar.
    public ICollection<Coffe> Coffes { get; set; } = new List<Coffe>();

    /// Propiedad de navegación: entidades de unión que enlazan cafés e ingredientes.
    /// Nombrada usando el tipo existente del proyecto <see cref="coffeIngredient"/>.
    /// Inicializada como lista vacía para evitar problemas de referencia nula.
    public ICollection<coffeIngredient> coffeIngredients { get; set; } = new List<coffeIngredient>();
}
