using System;

namespace Amphoreus.Domain;

public class coffeIngredient
{
   
    /// Identificador (clave foránea) del ingrediente.
    /// Corresponde a la columna ingredientId en la tabla de unión.
   
    public Guid ingredientId { get; set; }

   
    /// Identificador (clave foránea) del café.
    /// Corresponde a la columna coffeId en la tabla de unión.
   
    public Guid coffeId { get; set; }

   
    /// Navegación al ingrediente relacionado. Nullable cuando no se carga la relación.
   
    public Ingredient? Ingredient { get; set; }

   
    /// Navegación al café relacionado. Nullable cuando no se carga la relación.
   
    public Coffe? Coffe { get; set; }
}
