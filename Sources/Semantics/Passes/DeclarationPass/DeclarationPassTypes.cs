using Syntax.Nodes;
using Syntax.Nodes.Type;
using Syntax.Nodes.Type.Function;
using Syntax.Nodes.Type.Struct;
using Syntax.Nodes.Type.Tuple;

namespace Semantics.Passes.DeclarationPass;

partial class DeclarationPass
{
    public void Handle(StructTypeNode structTypeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(StructTypeFieldNode structTypeFieldNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(FunctionTypeNode functionTypeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TupleTypeNode tupleTypeNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TupleTypeFieldNode tupleTypeFieldNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(IdentifierTypeNode identifierTypeNode)
    {
        AddNodeToScope(identifierTypeNode);

        identifierTypeNode.PropagateTypeRef(ReferenceType(identifierTypeNode.Name));
    }

    public void Handle(FunctionTypeArgumentNode functionTypeArgumentNode)
    {
        throw new NotImplementedException();
    }

    public void Handle(TypeNode typeNode)
    {
        throw new NotImplementedException();
    }
}