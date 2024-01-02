using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public interface IBodyAccept
{
    public void BodyAccept(INodeHandler nodeHandler);
}