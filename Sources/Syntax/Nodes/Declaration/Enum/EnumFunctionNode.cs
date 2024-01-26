using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations.Enum;
using Syntax.Nodes.Declaration.Function;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumFunctionNode(PositionData positionData, FunctionDeclarationNode function)
    : BaseNode(positionData), INodeAcceptor<IEnumChildNodeHandler>
{
    public FunctionDeclarationNode Function { get; set; } = function;

    public void Accept(IEnumChildNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not EnumFunctionNode node)
        {
            return false;
        }

        return node.Function.TestEquals(Function);
    }
}