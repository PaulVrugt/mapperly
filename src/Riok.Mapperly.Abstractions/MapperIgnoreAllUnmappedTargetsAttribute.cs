namespace Riok.Mapperly.Abstractions;

/// <summary>
/// Ignores all target properties without a mapped source property from the mapping.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class MapperIgnoreAllUnmappedTargetsAttribute : Attribute
{
    /// <summary>
    /// Ignores all target properties without a mapped source property from the mapping.
    /// </summary>
    public MapperIgnoreAllUnmappedTargetsAttribute() { }
}
