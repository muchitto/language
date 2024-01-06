using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public TypeRef DeclareTypeUnknown(string name)
    {
        return DeclareType(name, new UnknownTypeInfo());
    }

    public TypeRef DeclareType(string name, TypeInfo typeInfo)
    {
        var result = CurrentScope.LookupSymbol(name);
        var typeRef = result.TypeRef;

        if (typeRef != null)
        {
            if (!result.CanBeUsedAsType)
            {
                throw new SemanticError($"Type {name} already declared");
            }

            if (!result.CrossedDeclarationBoundary)
            {
                typeRef.TypeInfo = typeInfo;

                return typeRef;
            }
        }

        var newTypeRef = new TypeRef(CurrentScope, typeInfo);

        CurrentScope.Symbols.Add(name, newTypeRef);

        return newTypeRef;
    }

    public TypeRef DeclareVariableUnknown(string name)
    {
        return DeclareVariable(name, new UnknownTypeInfo());
    }

    public TypeRef DeclareVariable(string name, TypeInfo typeInfo)
    {
        var typeRef = CurrentScope.LookupSymbolFromCurrentScope(name);

        if (typeRef != null)
        {
            if (!typeRef.IsUnknown)
            {
                throw new SemanticError($"Variable {name} already declared");
            }

            typeRef.TypeInfo = typeInfo;
            return typeRef;
        }

        var newTypeRef = new TypeRef(CurrentScope, typeInfo);

        CurrentScope.Symbols.Add(name, newTypeRef);

        return newTypeRef;
    }

    public TypeRef ReferenceVariable(string name)
    {
        var result = CurrentScope.LookupSymbol(name);
        var typeRef = result.TypeRef;

        if (typeRef != null)
        {
            return typeRef;
        }

        var newTypeRef = new TypeRef(CurrentScope, new UnknownTypeInfo());
        CurrentScope.Symbols.Add(name, newTypeRef);
        return newTypeRef;
    }

    public TypeRef ReferenceType(string name)
    {
        var result = CurrentScope.LookupSymbol(name);
        var typeRef = result.TypeRef;

        if (typeRef != null)
        {
            return typeRef;
        }

        var scope = CurrentScope.Parent ?? CurrentScope;
        var newTypeRef = new TypeRef(scope, new UnknownTypeInfo());
        scope.Symbols.Add(name, newTypeRef);
        return newTypeRef;
    }
}