using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public abstract class LiteralNode : BaseNode
{
    protected LiteralNode(PosData posData) : base(posData)
    {
    }
}

public class StringLiteralNode(PosData posData, string value) : LiteralNode(posData)
{
    public string Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}

public class NumberLiteralNode(PosData posData, string value) : LiteralNode(posData)
{
    public string Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}

public class CharLiteralNode(PosData posData, char value) : LiteralNode(posData)
{
    public char Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}

public class NilLiteralNode(PosData posData) : LiteralNode(posData)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}

public class BooleanLiteralNode(PosData posData, bool value) : LiteralNode(posData)
{
    public bool Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}

public class StructLiteralNode(PosData posData, List<StructLiteralFieldNode> fields) : LiteralNode(posData)
{
    public List<StructLiteralFieldNode> Fields { get; set; } = fields;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        foreach (var field in Fields)
        {
            field.TypeRefAdded();
        }
    }
}

public class StructLiteralFieldNode(PosData posData, IdentifierNode name, BaseNode value) : LiteralNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Name.TypeRefAdded();
        Value.TypeRefAdded();
    }
}

public class TupleLiteralNode(PosData posData, List<BaseNode> values) : LiteralNode(posData)
{
    public List<BaseNode> Values { get; set; } = values;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        foreach (var value in Values)
        {
            value.TypeRefAdded();
        }
    }
}

public class TupleLiteralFieldNode(PosData posData, string? name, BaseNode value) : LiteralNode(posData)
{
    public BaseNode Value { get; set; } = value;
    public string? Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }

        Value.TypeRefAdded();
    }
}