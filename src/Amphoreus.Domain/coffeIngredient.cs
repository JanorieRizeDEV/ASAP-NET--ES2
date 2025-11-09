using System;

namespace Amphoreus.Domain;

public class coffeIngredient
{
    public Guid ingredientId { get; set; }

    public Guid coffeId { get; set; }

    public Ingredient? Ingredient { get; set; }

    public Coffe? Coffe { get; set; }
}
