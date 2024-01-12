using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Closure;
using Syntax.Nodes.Declaration.Enum;
using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Declaration.Interface;
using Syntax.Nodes.Declaration.Struct;

namespace Semantics.Passes.DeclarationPass;

public class DeclarationPass(SemanticContext semanticContext)
    : SemanticPass(semanticContext), IDeclarationNodeHandler, IStatementListNodeHandler
{
    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructFunctionNode structFunctionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructVariableNode structVariableNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(InterfaceDeclarationNode interfaceDeclarationNodeDeclaration)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumDeclarationNode enumDeclarationNodeDeclaration)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumFunctionNode enumFunctionNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumCaseAssociatedValueNode enumCaseAssociatedValueNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(EnumCaseNode enumCaseNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionDeclarationNode functionDeclarationNode)
    {
        CurrentScope.LookupSymbol(functionDeclarationNode.Name.Name);

        functionDeclarationNode.BodyContainerNode.Accept(this);
    }

    public void Handle(FunctionArgumentNode functionArgumentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ClosureNode closureNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ClosureArgumentNode closureArgumentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(VariableDeclarationNode variableDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TypeAliasDeclarationNode typeAliasDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(BodyContainerNode bodyContainerDeclarationNode)
    {
        BlockHandler(bodyContainerDeclarationNode);
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        BlockHandler(programContainerNode);
    }

    private void BlockHandler(CodeBlockNode codeBlockNode)
    {
        StartScope();

        foreach (var node in codeBlockNode.Statements)
        {
            switch (node)
            {
                case INodeAcceptor<IStructDeclarationNodeHandler> statementListNode:
                    statementListNode.Accept(this);
                    break;
                case INodeAcceptor<IFunctionDeclarationNodeHandler> statementListNode:
                    statementListNode.Accept(this);
                    break;
            }
        }

        EndScope();
    }

    public override void Run(ProgramContainerNode ast)
    {
        ast.Accept(this);
    }
}