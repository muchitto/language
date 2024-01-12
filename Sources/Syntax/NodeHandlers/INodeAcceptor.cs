namespace Syntax.NodeHandlers;

public interface INodeAcceptor<in T>
{
    public void Accept(T nodeHandler);
}