namespace Parsing.NodeHandlers;

public interface INodeHandler : IBasicNodeHandler, IEnumNodeHandler, IFunctionNodeHandler, IInterfaceNodeHandler,
    ILiteralNodeHandler, IStructNodeHandler, ITypeNodeHandler
{
}