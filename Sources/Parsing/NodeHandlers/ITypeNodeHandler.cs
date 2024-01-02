using Parsing.Nodes;

namespace Parsing.NodeHandlers;

public interface ITypeNodeHandler
{ 
    public void Handle(TypeNode typeNode);

    public void Handle(StructTypeNode structTypeNode);

    public void Handle(StructTypeFieldNode structTypeFieldNode);

    public void Handle(FunctionTypeNode functionTypeNode);

    public void Handle(TupleTypeNode tupleTypeNode);

    public void Handle(TupleTypeFieldNode tupleTypeFieldNode);

    public void Handle(IdentifierTypeNode identifierTypeNode);

    public void Handle(FunctionTypeArgumentNode functionTypeArgumentNode);
}