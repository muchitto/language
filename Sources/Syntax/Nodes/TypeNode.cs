using ErrorReporting;
using TypeInformation;

namespace Syntax.Nodes;

public abstract class TypeNode(PositionData positionData) : BaseNode(positionData)
{
    public abstract TypeRef ResultingType();
}