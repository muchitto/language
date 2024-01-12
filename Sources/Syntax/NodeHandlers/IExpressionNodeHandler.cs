using Syntax.Nodes.Expression;

namespace Syntax.NodeHandlers;

public interface IExpressionNodeHandler
{
    public void Handle(IfExpressionNode ifExpressionNode);

    public void Handle(BinaryOpNode binaryOpNode);

    public void Handle(BodyExpressionNode bodyExpressionNode);

    public void Handle(FunctionCallNode functionCallNode);

    public void Handle(FunctionCallArgumentNode functionCallArgumentNode);
}