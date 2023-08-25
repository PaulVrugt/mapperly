namespace Riok.Mapperly.Abstractions;

/// <summary>
/// Ignores all unmapped source property from the mapping.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class MapperIgnoreAllUnmappedSourcesAttribute : Attribute
{
    /// <summary>
    /// Ignores all unmapped source property from the mapping.
    /// </summary>
    public MapperIgnoreAllUnmappedSourcesAttribute() { }
}
