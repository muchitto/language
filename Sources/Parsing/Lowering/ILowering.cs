using Syntax.Nodes;

namespace Parsing.Lowering;

public interface ILowering
{
    public void Lower(ProgramContainerNode programContainerNode);
}