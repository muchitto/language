using Lexing;
using Parsing.NodeHandlers;
using TypeInformation;

namespace Parsing.Node;

public abstract class BaseNode(PosData posData)
{
    public PosData PosData { get; set; } = posData;

    public TypeRef TypeRef { get; set; }

    public abstract void Accept(INodeHandler handler);

    public abstract void TypeRefAdded();
}