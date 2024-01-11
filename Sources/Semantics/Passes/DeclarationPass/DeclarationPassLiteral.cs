using Syntax.Nodes.Literal;

namespace Semantics.Passes.DeclarationPass;

public partial class DeclarationPass
{
    public void Handle(TupleLiteralNode tupleLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TupleLiteralFieldNode tupleLiteralFieldNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StringLiteralNode stringLiteralNode)
    {
        AddNodeToScope(stringLiteralNode);

        stringLiteralNode.PropagateTypeRef(SemanticContext.StringType());
    }

    public void Handle(NilLiteralNode nullLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(BooleanLiteralNode booleanLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(NumberLiteralNode numberLiteralNode)
    {
        AddNodeToScope(numberLiteralNode);

        numberLiteralNode.PropagateTypeRef(
            numberLiteralNode.Value.Contains('.')
                ? SemanticContext.FloatType()
                : SemanticContext.IntType()
        );
    }

    public void Handle(CharLiteralNode charLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructLiteralNode structLiteralNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructLiteralFieldNode structLiteralFieldNode)
    {
        throw new NotImplementedException();
    }
}