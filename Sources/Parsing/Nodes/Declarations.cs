using Lexing;
using Parsing.NodeHandlers;

namespace Parsing.Nodes;

public abstract class DeclarationNode(IdentifierNode name) : BaseNode(name.PosData)
{
    public IdentifierNode Name { get; } = name;
}

public class FunctionDeclarationNode(
    IdentifierNode name,
    FunctionArgumentListNode arguments,
    BodyContainerNode bodyContainerNode,
    bool canThrow,
    IdentifierNode? returnTypeName = null)
    : DeclarationNode(name)
{
    public FunctionArgumentListNode Arguments { get; set; } = arguments;
    public BodyContainerNode BodyContainerNode { get; set; } = bodyContainerNode;

    public bool CanThrow { get; set; } = canThrow;
    public IdentifierNode? ReturnTypeName { get; set; } = returnTypeName;

    public override void Accept(INodeHandler handler)
    {
        handler.HandleStart(this);

        Arguments.Accept(handler);

        BodyContainerNode.Accept(handler);

        ReturnTypeName?.Accept(handler);

        handler.HandleEnd(this);
    }
}

public class FunctionArgumentListNode(PosData posData, List<FunctionArgumentNode> arguments) : BaseNode(posData)
{
    public List<FunctionArgumentNode> Arguments { get; set; } = arguments;

    public override void Accept(INodeHandler handler)
    {
        handler.HandleStart(this);

        foreach (var argument in Arguments)
        {
            argument.Accept(handler);
        }

        handler.HandleEnd(this);
    }
}

public class FunctionArgumentNode(
    PosData posData,
    IdentifierNode name,
    TypeNode? typeName,
    BaseNode? defaultValue,
    bool isDynamic)
    : BaseNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public TypeNode? TypeName { get; set; } = typeName;

    public BaseNode? DefaultValue { get; set; } = defaultValue;

    public bool IsDynamic { get; set; } = isDynamic;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        TypeName?.Accept(handler);
        DefaultValue?.Accept(handler);
        Name.Accept(handler);
    }
}

public class VariableDeclarationNode(
    IdentifierNode name,
    BaseNode? value,
    bool isLet,
    IdentifierNode? typeName,
    bool isDynamic
)
    : DeclarationNode(name)
{
    public BaseNode? Value { get; set; } = value;
    public bool IsLet { get; set; } = isLet;
    public IdentifierNode? TypeName { get; set; } = typeName;
    public bool IsDynamic { get; set; } = isDynamic;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Value?.Accept(handler);
        TypeName?.Accept(handler);
    }
}

public class StructDeclarationNode(
    IdentifierNode name,
    List<StructFieldNode> fields,
    IdentifierNode? parent,
    List<IdentifierNode> interfaces,
    bool implOnly)
    : DeclarationNode(name)
{
    public List<StructFieldNode> Fields { get; set; } = fields;

    public IdentifierNode? Parent { get; set; } = parent;

    public List<IdentifierNode> Interfaces { get; set; } = interfaces;

    public bool ImplOnly { get; set; } = implOnly;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        foreach (var field in Fields)
        {
            field.Accept(handler);
        }
    }
}

public abstract class StructFieldNode(PosData posData, string name) : BaseNode(posData)
{
    public string Name { get; set; } = name;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }
}

public class StructVariableNode(PosData posData, string name, VariableDeclarationNode variable)
    : StructFieldNode(posData, name)
{
    public VariableDeclarationNode Variable { get; set; } = variable;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Variable.Accept(handler);
    }
}

public class StructFunctionNode(PosData posData, string name, FunctionDeclarationNode function)
    : StructFieldNode(posData, name)
{
    public FunctionDeclarationNode Function { get; set; } = function;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Function.Accept(handler);
    }
}

public class EnumNode(IdentifierNode name, List<EnumCaseNode> cases) : DeclarationNode(name)
{
    public List<EnumCaseNode> Cases { get; set; } = cases;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        foreach (var @case in Cases)
        {
            @case.Accept(handler);
        }
    }
}

public class EnumCaseNode(PosData posData, IdentifierNode name, List<EnumCaseAssociatedValueNode> associatedValues)
    : BaseNode(posData)
{
    public IdentifierNode Name { get; set; } = name;
    public List<EnumCaseAssociatedValueNode> AssociatedValues { get; set; } = associatedValues;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        foreach (var associatedValue in AssociatedValues)
        {
            associatedValue.Accept(handler);
        }
    }
}

public class EnumCaseAssociatedValueNode(PosData posData, IdentifierNode? name, IdentifierNode type) : BaseNode(posData)
{
    public IdentifierNode? Name { get; set; } = name;
    public IdentifierNode Type { get; set; } = type;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Name?.Accept(handler);
        Type.Accept(handler);
    }
}

public class EnumFunctionNode(PosData posData, FunctionDeclarationNode function) : BaseNode(posData)
{
    public FunctionDeclarationNode Function { get; set; } = function;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Function.Accept(handler);
    }
}

public class InterfaceDeclarationNode(
    IdentifierNode name,
    List<FunctionDeclarationNode> functions,
    List<VariableDeclarationNode> fields) : DeclarationNode(name)
{
    public List<FunctionDeclarationNode> Functions { get; set; } = functions;
    public List<VariableDeclarationNode> Fields { get; set; } = fields;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        foreach (var function in Functions)
        {
            function.Accept(handler);
        }

        foreach (var field in Fields)
        {
            field.Accept(handler);
        }
    }
}

public class TypeAliasDeclarationNode(IdentifierNode name, TypeNode type) : DeclarationNode(name)
{
    public TypeNode Type { get; set; } = type;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);

        Type.Accept(handler);
    }
}