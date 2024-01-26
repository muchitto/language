using Semantics.SymbolResolving;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Struct;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Closure;
using Syntax.Nodes.Declaration.Enum;
using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Declaration.Struct;
using TypeInformation;

namespace Semantics.Passes.TypeResolutionPass;

public class TypeResolutionPass(SemanticContext semanticContext)
    : SemanticPass(semanticContext), ICommonPass
{
    public void Handle(BodyContainerNode bodyContainerDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ProgramContainerNode programContainerNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(CodeBlockNode codeBlockNode)
    {
        StartCodeBlockScope(codeBlockNode);

        HandleBlock(codeBlockNode);

        EndScope();
    }

    public void Handle(FunctionDeclarationNode functionDeclarationNode)
    {
        StartDeclarationScope(functionDeclarationNode.Name);

        HandleBlock(functionDeclarationNode.Body);

        EndScope();
    }

    public void Handle(StructDeclarationNode structDeclarationNode)
    {
        StartDeclarationScope(structDeclarationNode.Name);

        var fields = new Dictionary<string, TypeInfo>();

        foreach (var field in structDeclarationNode.Fields)
        {
            if (field is not INodeAcceptor<IStructChildNodeHandler> structChildNode)
            {
                throw new Exception("Struct child node is not a struct child node handler");
            }

            structChildNode.Accept(this);

            var symbol = SemanticContext.GetSymbol(field.Name);

            if (symbol is null)
            {
                throw new Exception("Symbol is null");
            }

            fields.Add(symbol.Name, symbol.TypeInfo);
        }

        var structType = new StructTypeInfo(fields);

        SemanticContext.SetSymbol(structDeclarationNode.Name, new Symbol(structDeclarationNode.Name.Name, structType));

        foreach (var node in structDeclarationNode.Fields)
        {
            if (node is not INodeAcceptor<IStructChildNodeHandler> structChildNode)
            {
                throw new Exception("Struct child node is not a struct child node handler");
            }

            structChildNode.Accept(this);
        }

        EndScope();
    }

    public void Handle(EnumDeclarationNode enumDeclarationNodeDeclaration)
    {
        throw new NotImplementedException();
    }

    public void Handle(ClosureDeclarationNode closureDeclarationNode)
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

    public void Handle(VariableDeclarationNode variableDeclarationNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(IdentifierNode identifierNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FieldAccessNode fieldAccessNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(ArrayAccessNode arrayAccessNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(DeclarationNameNode declarationNameNode)
    {
        throw new NotImplementedException();
    }

    private void HandleBlock(CodeBlockNode codeBlockNode)
    {
        foreach (var node in codeBlockNode.Statements)
        {
            switch (node)
            {
                case StructDeclarationNode statementListNode:
                    statementListNode.Accept(this);
                    break;
                case FunctionDeclarationNode statementListNode:
                    statementListNode.Accept(this);
                    break;
                case INodeAcceptor<ICodeBlockNodeHandler> statementListNode:
                    statementListNode.Accept(this);
                    break;
            }
        }
    }

    public override void Run(ProgramContainerNode ast)
    {
        (ast as CodeBlockNode).Accept(this);
    }
}