using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;
using Syntax.Nodes.Declaration.Function;

namespace Syntax.Nodes.Declaration.Enum;

public class EnumFunctionNode(PositionData positionData, FunctionDeclarationNode function)
    : BaseNode(positionData), INodeAcceptor<IEnumDeclarationNodeHandler>
{
    public FunctionDeclarationNode Function { get; set; } = function;

    public void Accept(IEnumDeclarationNodeHandler handler)
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