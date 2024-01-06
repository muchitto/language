using Parsing.NodeHandlers;
using Parsing.Nodes;

namespace Parsing.Node.Expression;

public class FunctionCallNode(BaseNode callee, List<FunctionCallArgumentNode> arguments)
    : ExpressionNode(callee.PosData)
{
    public BaseNode Callee { get; set; } = callee;
    public List<FunctionCallArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Callee.TypeRefAdded();

        foreach (var argument in Arguments)
        {
            argument.TypeRefAdded();
        }
    }
}