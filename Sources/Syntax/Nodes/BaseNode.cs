using Lexing;
using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes;

public abstract class BaseNode(PosData posData)
{
    public PosData PosData { get; set; } = posData;

    public TypeRef TypeRef { get; set; }

    public abstract void Accept(INodeHandler handler);

    public abstract void TypeRefAdded();
}