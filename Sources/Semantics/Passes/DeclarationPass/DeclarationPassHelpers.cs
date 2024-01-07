using ErrorReporting;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public TypeRef DeclareTypeUnknown(PositionData positionData, string name)
    {
        return DeclareType(positionData, name, new UnknownTypeInfo());
    }

    public TypeRef DeclareType(PositionData positionData, string name, TypeInfo typeInfo)
    {
        var result = CurrentScope.LookupSymbol(name);
        var typeRef = result.TypeRef;

        if (typeRef != null)
        {
            if (!result.CanBeUsedAsType)
            {
                throw new SemanticError(positionData, $"Type {name} already declared");
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

    public TypeRef DeclareVariableUnknown(PositionData positionData, string name)
    {
        return DeclareVariable(positionData, name, new UnknownTypeInfo());
    }

    public TypeRef DeclareVariable(PositionData positionData, string name, TypeInfo typeInfo)
    {
        return DeclareVariable(positionData, name, new TypeRef(CurrentScope, typeInfo));
    }

    public TypeRef DeclareVariable(PositionData positionData, string name, TypeRef typeRef)
    {
        var result = CurrentScope.LookupSymbol(name);
        var resultTypeRef = result.TypeRef;

        if (resultTypeRef != null)
        {
            if (!result.CanBeUsedAsVariable)
            {
                throw new SemanticError(positionData, $"Variable {name} already declared");
            }

            if (!result.CrossedDeclarationBoundary)
            {
                if (resultTypeRef.IsUnknown)
                {
                    resultTypeRef.TypeInfo = typeRef.TypeInfo;
                }

                return resultTypeRef;
            }
        }

        CurrentScope.Symbols.Add(name, typeRef);

        return typeRef;
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