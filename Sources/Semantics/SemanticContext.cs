using TypeInformation;

namespace Semantics;

public class SemanticContext
{
    public readonly List<Scope> AllScopes = [];

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
}