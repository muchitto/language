using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public abstract class LiteralNode : BaseNode
{
    protected LiteralNode(PosData posData) : base(posData)
    {
    }

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class StringLiteralNode(PosData posData, string value) : BaseNode(posData)
{
    public string Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class NumberLiteralNode(PosData posData, string value) : BaseNode(posData)
{
    public string Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class CharLiteralNode(PosData posData, char value) : BaseNode(posData)
{
    public char Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class NilLiteralNode(PosData posData) : BaseNode(posData)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class BooleanLiteralNode(PosData posData, bool value) : BaseNode(posData)
{
    public bool Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class StructLiteralNode(PosData posData, List<StructLiteralFieldNode> fields) : BaseNode(posData)
{
    public List<StructLiteralFieldNode> Fields { get; set; } = fields;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class StructLiteralFieldNode(PosData posData, IdentifierNode name, BaseNode value) : BaseNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public BaseNode Value { get; set; } = value;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class TupleLiteralNode(PosData posData, List<BaseNode> values) : LiteralNode(posData)
{
    public List<BaseNode> Values { get; set; } = values;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        foreach (var value in Values)
        {
            value.Accept(handler);
        }
    }
}

public class TupleLiteralFieldNode(PosData posData, string? name, BaseNode value) : ExpressionNode(posData)
{
    public BaseNode Value { get; set; } = value;
    public string? Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Value.Accept(handler);
    }
}