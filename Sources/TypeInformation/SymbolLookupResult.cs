namespace TypeInformation;

/// <summary>
///     This is used for symbol lookup. It contains the type reference and a boolean indicating whether the lookup crossed
///     a declaration boundary.
/// </summary>
/// <param name="TypeRef">The type reference that was returned, can be null</param>
/// <param name="CrossedDeclarationBoundary">This is true if the type fetching crossed a declaration boundary</param>
/// <param name="Scope">The scope in which the type reference was found</param>
public record SymbolLookupResult(TypeRef? TypeRef, bool CrossedDeclarationBoundary, Scope Scope)
{
    /// <summary>
    ///     This is true if the type reference is not null and the lookup did not cross a declaration boundary
    ///     You can use this to check if the type reference can be used as a type again.
    ///     For example:
    ///     <code>
    ///     func test()
    ///         // ...
    ///     end
    /// 
    ///     struct Test
    ///         func test()
    ///         end
    ///     end
    ///     </code>
    ///     Those two functions have the same name, but the first one is a function and the second one is a method.
    /// </summary>
    public bool CanBeUsedAsType => TypeRef is { IsUnknown: true } ||
                                   (CrossedDeclarationBoundary && TypeRef is { IsUnknown: false });

    /// <summary>
    ///     This is true if the type reference is not null and the lookup did not cross a declaration boundary
    ///     You can use this to check if the type reference can be used as a variable again.
    /// </summary>
    public bool CanBeUsedAsVariable => CanBeUsedAsType;
}