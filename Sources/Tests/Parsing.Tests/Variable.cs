using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;
using Syntax.Nodes.Type;

namespace Parsing.Tests;

public class Variable
{
    [Fact]
    public void Test_Variable_Declaration_With_Type()
    {
        const string source = "var x string";

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(PositionData.Test(), "x"),
                        null,
                        false,
                        new IdentifierTypeNode(PositionData.Test(), "string"),
                        false
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_Variable_Declaration_Without_Value()
    {
        const string source = "var x";

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(PositionData.Test(), "x"),
                        null,
                        false,
                        null,
                        false
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_Variable_Declaration_With_Value()
    {
        const string source = "var x = 1";

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(PositionData.Test(), "x"),
                        new NumberLiteralNode(PositionData.Test(), "1"),
                        false,
                        null,
                        false
                    )
                ]
            )
        ));
    }
}