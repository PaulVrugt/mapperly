using Riok.Mapperly.Descriptors.Mappings;
using Riok.Mapperly.Descriptors.Mappings.ExistingTarget;
using Riok.Mapperly.Helpers;

namespace Riok.Mapperly.Descriptors.MappingBuilders;

public static class NullableMappingBuilder
{
    public static NewInstanceMapping? TryBuildMapping(MappingBuilderContext ctx)
    {
        if (!TryBuildNonNullableMappingKey(ctx, out var mappingKey))
            return null;

        var delegateMapping = ctx.BuildMapping(mappingKey, MappingBuildingOptions.KeepUserSymbol);
        return delegateMapping == null ? null : BuildNullDelegateMapping(ctx, delegateMapping);
    }

    public static IExistingTargetMapping? TryBuildExistingTargetMapping(MappingBuilderContext ctx)
    {
        if (!TryBuildNonNullableMappingKey(ctx, out var mappingKey))
            return null;

        var delegateMapping = ctx.FindOrBuildExistingTargetMapping(mappingKey);
        return delegateMapping == null ? null : new NullDelegateExistingTargetMapping(ctx.Source, ctx.Target, delegateMapping);
    }

    private static bool TryBuildNonNullableMappingKey(MappingBuilderContext ctx, out TypeMappingKey mappingKey)
    {
        var sourceIsNullable = ctx.Source.TryGetNonNullable(out var sourceNonNullable);
        var targetIsNullable = ctx.Target.TryGetNonNullable(out var targetNonNullable);
        if (!sourceIsNullable && !targetIsNullable)
        {
            mappingKey = default;
            return false;
        }

        mappingKey = new TypeMappingKey(sourceNonNullable ?? ctx.Source, targetNonNullable ?? ctx.Target);
        return true;
    }

    private static NewInstanceMapping BuildNullDelegateMapping(MappingBuilderContext ctx, INewInstanceMapping mapping)
    {
        var nullFallback = ctx.GetNullFallbackValue();
        return mapping switch
        {
            MethodMapping methodMapping => new NullDelegateMethodMapping(ctx.Source, ctx.Target, methodMapping, nullFallback),
            _ => new NullDelegateMapping(ctx.Source, ctx.Target, mapping, nullFallback),
        };
    }
}
