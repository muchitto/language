using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Literal;
using Syntax.Nodes.Statement;

namespace Parsing.Tests;

public class Basic : ParserTest
{
    [Fact]
    public void Test_A_Assignment_Without_Newline_And_Spaces()
    {
        const string source = "a=1";
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new AssignmentNode(
                        new IdentifierNode(PositionData.Test(), "a"),
                        new NumberLiteralNode(PositionData.Test(), "1")
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_A_Assignment_With_Newline_And_Spaces()
    {
        const string source = """
                                   a=1


                              """;
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new AssignmentNode(
                        new IdentifierNode(PositionData.Test(), "a"),
                        new NumberLiteralNode(PositionData.Test(), "1")
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_An_Empty_Program()
    {
        const string source = "";
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(), [])
        ));
    }

    [Fact]
    public void Test_An_Empty_Program_With_Newline_And_Spaces()
    {
        const string source = """



                              """;
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(), [])
        ));
    }
}