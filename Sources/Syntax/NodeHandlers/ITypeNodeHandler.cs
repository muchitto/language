using Syntax.Nodes.Type;
using Syntax.Nodes.Type.Function;
using Syntax.Nodes.Type.Struct;
using Syntax.Nodes.Type.Tuple;

namespace Syntax.NodeHandlers;

public interface ITypeNodeHandler
{
    public void Handle(StructTypeNode structTypeNode);

    public void Handle(StructTypeFieldNode structTypeFieldNode);

    public void Handle(FunctionTypeNode functionTypeNode);

    public void Handle(TupleTypeNode tupleTypeNode);

    public void Handle(TupleTypeFieldNode tupleTypeFieldNode);

    public void Handle(IdentifierTypeNode identifierTypeNode);

    public void Handle(FunctionTypeArgumentNode functionTypeArgumentNode);
}