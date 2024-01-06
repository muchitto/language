using Lexing;
using Parsing.NodeHandlers;
using Parsing.Nodes.Type;
using TypeInformation;

namespace Parsing.Nodes;

public abstract class BaseNode(PosData posData)
{
    public PosData PosData { get; set; } = posData;

    public TypeRef TypeRef { get; set; }

    public abstract void Accept(INodeHandler handler);

    public abstract void TypeRefAdded();
}

public abstract class StatementListContainerNode(PosData posData, List<BaseNode> Statements) : BaseNode(posData)
{
    public List<BaseNode> Statements { get; set; } = Statements;
}

public class FieldAccessNode(BaseNode left, BaseNode right) : BaseNode(left.PosData)
{
    public BaseNode Left { get; set; } = left;
    public BaseNode Right { get; set; } = right;

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

        Left.TypeRefAdded();
        Right.TypeRefAdded();
    }
}

public class ArrayAccessNode(BaseNode array, BaseNode access) : BaseNode(array.PosData)
{
    public BaseNode Array { get; set; } = array;
    public BaseNode AccessExpression { get; set; } = access;

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

        Array.TypeRefAdded();
        AccessExpression.TypeRefAdded();
    }
}

public class ProgramContainerNode(PosData posData, List<BaseNode> statements)
    : StatementListContainerNode(posData, statements)
{
    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public override void TypeRefAdded()
    {
        foreach (var statement in Statements)
        {
            statement.TypeRefAdded();
        }
    }
}

public class BodyContainerNode(PosData posData, List<BaseNode> statements, bool canReturn)
    : StatementListContainerNode(posData, statements)
{
    public bool CanReturn { get; set; } = canReturn;


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

        foreach (var statement in Statements)
        {
            statement.TypeRefAdded();
        }
    }
}

public class IdentifierNode(PosData posData, string name)
    : BaseNode(posData)
{
    public string Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }

    public static explicit operator IdentifierNode(IdentifierTypeNode node)
    {
        return new IdentifierNode(node.PosData, node.Name);
    }

    public override void TypeRefAdded()
    {
        if (TypeRef == null)
        {
            throw new Exception("TypeRef is null");
        }
    }
}

public class AnnotationNode(
    IdentifierNode name,
    AnnotationArgumentListNode arguments,
    BaseNode attachedNode)
    : BaseNode(name.PosData)
{
    public IdentifierNode Name { get; set; } = name;
    public AnnotationArgumentListNode Arguments { get; set; } = arguments;
    public BaseNode AttachedNode { get; set; } = attachedNode;

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
        Arguments.TypeRefAdded();
        AttachedNode.TypeRefAdded();
    }
}

public class AnnotationArgumentListNode(PosData posData, List<AnnotationArgumentNode> arguments) : BaseNode(posData)
{
    public List<AnnotationArgumentNode> Arguments { get; set; } = arguments;

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

        foreach (var argument in Arguments)
        {
            argument.TypeRefAdded();
        }
    }
}

public class AnnotationArgumentNode(PosData posData, IdentifierNode name, ExpressionNode value)
    : BaseNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public ExpressionNode Value { get; set; } = value;

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