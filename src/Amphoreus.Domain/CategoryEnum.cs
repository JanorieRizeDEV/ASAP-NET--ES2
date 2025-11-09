namespace Amphoreus.Domain;

// Enum que representa las categorías de café en la aplicación.
//
// Comentarios en español:
// - Cada valor corresponde a una categoría usada para clasificar los
//   productos (por ejemplo, para filtrado o datos semilla).
// - Los valores numéricos se fijan explícitamente (1, 2, 3) para
//   facilitar su uso al persistir datos o generar seed data.
public enum CategoryEnum
{
    // Café frío/iced coffee
    iceCoffe = 1,

    // Café caliente
    hotCoffe = 2,

    // Café batido / blended (ej. frappé)
    blendedCoffe = 3
}
