

using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Amphoreus.Domain;
public class Coffe : BaseEntity
{
    /// Display name of the coffee (required).
    public required string Name { get; set; }

    /// Optional description for the coffee.
    public string? description { get; set; }

    /// Price of the coffee in the configured currency.
    public decimal price { get; set; }

    /// Foreign key to the coffee category.
    /// Keep the naming consistent with the rest of the domain.
    public int cateogoryId { get; set; }

    /// Optional image path or URL for the coffee.
    public string? image { get; set; }

    /// Navigation property to the <see cref="Category"/> this coffee belongs to.
    /// Nullable when the relationship is optional or not yet loaded.
    public Category? Category { get; set; }


    /// Collection navigation for ingredients directly associated with this coffee.
    /// Initialized to an empty list to avoid null reference usage when enumerating.
    public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    /// Collection navigation for the join entities linking coffees and ingredients.
    /// Use this when you need access to additional payload on the relationship.
    public ICollection<coffeIngredient> coffeIngredients { get; set; } = new List<coffeIngredient>();

    /// Collection of related coffees (if the domain models related coffee groupings).
    /// Initialized to an empty list to avoid null checks.
    public ICollection<Coffe> coffes { get; set; } = new List<Coffe>();

}
