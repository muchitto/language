using ErrorReporting;
using Syntax.NodeHandlers;
using Syntax.Nodes.Declaration.Function;

namespace Syntax.Nodes.Declaration.Struct;

public abstract class StructFieldNode(PositionData positionData, string name) : BaseNode(positionData)
{
    public string Name { get; set; } = name;
}

public class StructVariableNode(PositionData positionData, string name, VariableDeclarationNode variable)
    : StructFieldNode(positionData, name)
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

public class StructFunctionNode(PositionData positionData, string name, FunctionDeclarationNode function)
    : StructFieldNode(positionData, name)
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