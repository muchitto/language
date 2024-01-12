using Syntax.NodeHandlers;
using Syntax.NodeHandlers.Declarations;

namespace Syntax.Nodes.Declaration.Function;

public class FunctionDeclarationNode(
    IdentifierNode name,
    List<FunctionArgumentNode> arguments,
    BodyContainerNode bodyContainerNode,
    bool canThrow,
    bool isMethod,
    IdentifierNode? returnTypeName)
    : DeclarationNode(name), INodeAcceptor<IFunctionDeclarationNodeHandler>
{
    public List<FunctionArgumentNode> Arguments { get; set; } = arguments;
    public BodyContainerNode BodyContainerNode { get; set; } = bodyContainerNode;

    public bool CanThrow { get; set; } = canThrow;
    public bool IsMethod { get; set; } = isMethod;
    public IdentifierNode? ReturnTypeName { get; set; } = returnTypeName;

    public void Accept(IFunctionDeclarationNodeHandler handler)
    {
        handler.Handle(this);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionDeclarationNode node)
        {
            return false;
        }

        if (node.CanThrow != CanThrow
            || node.IsMethod != IsMethod
            || !Arguments.TestEquals(node.Arguments)
            || !node.BodyContainerNode.TestEquals(BodyContainerNode)
            || !node.Name.TestEquals(Name))
        {
            return false;
        }

        return ReturnTypeName.TestEqualsOrBothNull(node.ReturnTypeName);
    }
}