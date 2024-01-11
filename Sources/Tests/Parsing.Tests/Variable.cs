using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;
using Syntax.Nodes.Type;

namespace Parsing.Tests;

public class Variable : ParserTest
{
    [Fact]
    public void Test_Variable_Declaration_With_Type()
    {
        const string source = "var x string";

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(Pos,
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(Pos, "x"),
                        null,
                        false,
                        new IdentifierTypeNode(Pos, "string"),
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
            new ProgramContainerNode(Pos,
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(Pos, "x"),
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
            new ProgramContainerNode(Pos,
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(Pos, "x"),
                        new NumberLiteralNode(Pos, "1"),
                        false,
                        null,
                        false
                    )
                ]
            )
        ));
    }
}