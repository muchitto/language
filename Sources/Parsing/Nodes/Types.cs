using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public class TypeNode(PosData posData) : BaseNode(posData)
{ 
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class IdentifierTypeNode(PosData posData, string name) : TypeNode(posData)
{
    public string Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public static explicit operator IdentifierTypeNode(IdentifierNode node)
    {
        return new IdentifierTypeNode(node.PosData, node.Name);
    }
}

public class StructTypeNode(PosData posData, List<StructTypeFieldNode> fields) : TypeNode(posData)
{
    public List<StructTypeFieldNode> Fields { get; set; } = fields;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class StructTypeFieldNode(PosData posData, string name, TypeNode type) : BaseNode(posData)
{
    public string Name { get; set; } = name;
    public TypeNode Type { get; set; } = type;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class FunctionTypeNode(PosData posData, List<FunctionTypeArgumentNode> parameters, TypeNode returnType)
    : TypeNode(posData)
{
    public List<FunctionTypeArgumentNode> Parameters { get; set; } = parameters;
    public TypeNode ReturnType { get; set; } = returnType;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class FunctionTypeArgumentNode(PosData posData, string? name, TypeNode type) : TypeNode(posData)
{
    public string? Name { get; set; } = name;
    public TypeNode Type { get; set; } = type;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class TupleTypeNode(PosData posData, List<TupleTypeFieldNode> types) : TypeNode(posData)
{
    public List<TupleTypeFieldNode> Types { get; set; } = types;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class TupleTypeFieldNode(PosData posData, string? name, TypeNode? type) : TypeNode(posData)
{
    public string? Name { get; set; } = name;
    public TypeNode Type { get; set; } = type;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}