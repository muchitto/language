using Lexing;
using Parsing.NodeHandlers;
using Parsing.Nodes.Declaration.Function;

namespace Parsing.Nodes.Declaration.Struct;

public abstract class StructFieldNode(PosData posData, string name) : BaseNode(posData)
{
    public string Name { get; set; } = name;
}

public class StructVariableNode(PosData posData, string name, VariableDeclarationNode variable)
    : StructFieldNode(posData, name)
{
    public VariableDeclarationNode Variable { get; set; } = variable;

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

        Variable.TypeRefAdded();
    }
}

public class StructFunctionNode(PosData posData, string name, FunctionDeclarationNode function)
    : StructFieldNode(posData, name)
{
    public FunctionDeclarationNode Function { get; set; } = function;

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

        Function.TypeRefAdded();
    }
}