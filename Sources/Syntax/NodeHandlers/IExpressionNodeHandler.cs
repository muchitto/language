using Syntax.Nodes.Expression;

namespace Syntax.NodeHandlers;

public interface IExpressionNodeHandler
{
    public void Handle(IfExpressionNode ifExpressionNode);

    public void Handle(BinaryOpNode binaryOpNode);

    public void Handle(BodyExpressionNode bodyExpressionNode);
}