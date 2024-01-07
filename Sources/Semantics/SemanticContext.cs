using Syntax.Nodes;
using TypeInformation;

namespace Semantics;

public class SemanticContext
{
    public readonly List<Scope> AllScopes = [];
    public Dictionary<BaseNode, Scope> NodeToScope = new();

    public Scope CurrentScope { get; private set; }

    public Scope TopScope => CurrentScope.TopScope;

    public bool IsCurrentScopeTopScope => CurrentScope == TopScope;

    public Scope StartScope(ScopeType scopeType)
    {
        var newScope = new Scope(CurrentScope, scopeType);

        AllScopes.Add(newScope);

        CurrentScope = newScope;

        return newScope;
    }

    public void EndScope()
    {
        if (CurrentScope.Parent != null)
        {
            CurrentScope = CurrentScope.Parent;
        }
    }

    public void SetCurrentScope(Scope scope)
    {
        CurrentScope = scope;
    }

    public TypeRef VoidType()
    {
        var result = CurrentScope.LookupSymbol("Void");

        if (result == null)
        {
            throw new Exception("Void type not found");
        }

        return result.TypeRef;
    }

    public TypeRef DynamicType()
    {
        var result = CurrentScope.LookupSymbol("Dynamic");

        if (result == null)
        {
            throw new Exception("Dynamic type not found");
        }

        return result.TypeRef;
    }

    public TypeRef BoolType()
    {
        var result = CurrentScope.LookupSymbol("Bool");

        if (result == null)
        {
            throw new Exception("Bool type not found");
        }

        return result.TypeRef;
    }

    public TypeRef IntType()
    {
        var result = CurrentScope.LookupSymbol("Int");

        if (result == null)
        {
            throw new Exception("Int type not found");
        }

        return result.TypeRef;
    }

    public TypeRef FloatType()
    {
        var result = CurrentScope.LookupSymbol("Float");

        if (result == null)
        {
            throw new Exception("Float type not found");
        }

        return result.TypeRef;
    }

    public TypeRef StringType()
    {
        var result = CurrentScope.LookupSymbol("String");

        if (result == null)
        {
            throw new Exception("String type not found");
        }

        return result.TypeRef;
    }
}