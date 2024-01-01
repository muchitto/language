using Lexing;
using Parsing.NodeHandlers;
using TypeInformation;

namespace Parsing.Nodes;

public abstract class BaseNode(PosData posData)
{
    public PosData PosData { get; set; } = posData;

    public TypeRef TypeRef { get; set; }

    public abstract void Accept(INodeHandler handler);
}

public abstract class StatementListContainerNode(PosData posData, List<BaseNode> Statements) : BaseNode(posData)
{
    public List<BaseNode> Statements { get; set; } = Statements;

    public override void Accept(INodeHandler handler)
    {
        handler.HandleStart(this);

        foreach (var statement in Statements)
        {
            statement.Accept(handler);
        }

        handler.HandleEnd(this);
    }
}

public class ProgramContainerNode(PosData posData, List<BaseNode> statements)
    : StatementListContainerNode(posData, statements)
{
}

public class BodyContainerNode(PosData posData, List<BaseNode> statements, bool canReturn)
    : StatementListContainerNode(posData, statements)
{
    public bool CanReturn { get; set; } = canReturn;
}

public class IdentifierNode(PosData posData, string name, BaseNode? subField = null)
    : BaseNode(posData)
{
    public string Name { get; set; } = name;
    public BaseNode? SubField { get; set; } = subField;

    public void PropagateTypes()
    {
    }

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        SubField?.Accept(handler);
    }

    public static explicit operator IdentifierNode(IdentifierTypeNode node)
    {
        return new IdentifierNode(node.PosData, node.Name);
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

        Name.Accept(handler);
        Arguments.Accept(handler);
        AttachedNode.Accept(handler);
    }
}

public class AnnotationArgumentListNode(PosData posData, List<AnnotationArgumentNode> arguments) : BaseNode(posData)
{
    public List<AnnotationArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        foreach (var argument in Arguments)
        {
            argument.Accept(handler);
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

        Name.Accept(handler);
        Value.Accept(handler);
    }
}