using Lexing;
using Parsing.Node.Declaration.Function;
using Parsing.NodeHandlers;

namespace Parsing.Node.Declaration.Enum;

public class EnumFunctionNode(PosData posData, FunctionDeclarationNode function) : BaseNode(posData)
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