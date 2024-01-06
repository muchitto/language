using Syntax.Nodes;
using TypeInformation;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(BodyContainerNode bodyContainerDeclarationNode)
    {
        SemanticContext.StartScope(ScopeType.Regular);

        bodyContainerDeclarationNode.Statements.ForEach(statement => statement.Accept(this));

        SemanticContext.EndScope();
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        SemanticContext.StartScope(ScopeType.Regular);

        CreateBaseTypes();

        programContainerNode.Statements.ForEach(statement => statement.Accept(this));

        SemanticContext.EndScope();
    }
}