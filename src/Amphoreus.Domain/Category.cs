using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Amphoreus.Domain;

public class Category
{   
    [SetsRequiredMembers]
    public Category (int id, string name) => (Id, Name) = (id, name);


    public required int Id { get; set; }

    public required string Name { get; set; }

    public string? description { get; set; }

    public ICollection<Coffe> Coffes { get; set; } = [];

    public static Category create(int id)
    {
        var Categoryname = (CategoryEnum)id;
        string CategoryNameString = Categoryname.ToString();

        return new Category(id, CategoryNameString);
    }
}