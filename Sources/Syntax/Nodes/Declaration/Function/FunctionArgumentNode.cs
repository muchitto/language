using Syntax.NodeHandlers;
using TypeInformation;

namespace Syntax.Nodes.Declaration.Function;

public class FunctionArgumentNode(
    IdentifierNode name,
    TypeNode? typeName,
    BaseNode? defaultValue,
    bool isDynamic)
    : BaseNode(name.PositionData)
{
    public IdentifierNode Name { get; set; } = name;
    public TypeNode? TypeName { get; set; } = typeName;

    public BaseNode? DefaultValue { get; set; } = defaultValue;

    public bool IsDynamic { get; set; } = isDynamic;

    public override void Accept(INodeHandler handler)
    {
        handler.Handle(this);
    }


    public override void SetTypeRef(TypeRef typeRef)
    {
        TypeRef = typeRef;
        Name.SetTypeRef(typeRef);
        TypeName?.SetTypeRef(typeRef);
    }

    public override bool TestEquals(BaseNode other)
    {
        if (other is not FunctionArgumentNode node)
        {
            return false;
        }

        return node.IsDynamic == IsDynamic
               && DefaultValue.TestEqualsOrBothNull(node.DefaultValue)
               && TypeName.TestEqualsOrBothNull(node.TypeName)
               && node.Name.TestEquals(Name);
    }

    public void Test()
    {
        var t = new
        {
            x = 1,
            y = 2,
            Statements = new[]
            {
                new
                {
                    x = 1,
                    y = 2
                },
                new
                {
                    x = 1,
                    y = 2
                }
            }
        };
    }
}