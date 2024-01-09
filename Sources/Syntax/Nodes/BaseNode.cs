using ErrorReporting;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public abstract class BaseNode(PositionData positionData)
{
    public PositionData PositionData { get; set; } = positionData;

    public TypeRef TypeRef { get; protected set; }

    public abstract void Accept(INodeHandler handler);

    public abstract void SetTypeRef(TypeRef typeRef);

    public abstract bool TestEquals(BaseNode other);

    protected bool TestEqualsOrBothNull<T, TK>(T? a, TK? b) where T : BaseNode where TK : BaseNode
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.TestEquals(b);
    }
}