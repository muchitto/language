namespace TypeInformation;

/// <summary>
///     This is used for symbol lookup. It contains the type reference and a boolean indicating whether the lookup crossed
///     a declaration boundary.
/// </summary>
/// <param name="TypeRef">The type reference that was returned, can be null</param>
/// <param name="CrossedDeclarationBoundary">This is true if the type fetching crossed a declaration boundary</param>
public record SymbolLookupResult(TypeRef? TypeRef, bool CrossedDeclarationBoundary)
{
    /// <summary>
    ///     This is true if the type reference is not null and the lookup did not cross a declaration boundary
    ///     You can use this to check if the type reference can be used as a type.
    /// </summary>
    public bool CanBeUsedAsType => TypeRef is { IsUnknown: true } ||
                                   (CrossedDeclarationBoundary && TypeRef is { IsUnknown: false });
}