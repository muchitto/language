using Syntax.Nodes.Literal;

namespace Syntax.NodeHandlers;

public interface ILiteralNodeHandler
{
    public void Handle(StructLiteralNode structLiteralNode);

    public void Handle(StructLiteralFieldNode structLiteralFieldNode);

    public void Handle(TupleLiteralNode tupleLiteralNode);

    public void Handle(TupleLiteralFieldNode tupleLiteralFieldNode);

    public void Handle(StringLiteralNode stringLiteralNode);

    public void Handle(NilLiteralNode nullLiteralNode);

    public void Handle(BooleanLiteralNode booleanLiteralNode);

    public void Handle(NumberLiteralNode numberLiteralNode);

    public void Handle(CharLiteralNode charLiteralNode);
}