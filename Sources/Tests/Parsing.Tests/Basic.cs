using ErrorReporting;
using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Expression;
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

    [Fact]
    public void Test_Expression_Precedence()
    {
        const string source = "a = 1 + 2 * 3";
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new AssignmentNode(
                        new IdentifierNode(PositionData.Test(), "a"),
                        new BinaryOpNode(
                            Pos,
                            new NumberLiteralNode(PositionData.Test(), "1"),
                            Operator.Add,
                            new BinaryOpNode(
                                Pos,
                                new NumberLiteralNode(PositionData.Test(), "2"),
                                Operator.Multiply,
                                new NumberLiteralNode(PositionData.Test(), "3")
                            )
                        )
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_Expression_Precedence_With_Different_Order()
    {
        const string source = "a = 1 * 2 + 3";
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new AssignmentNode(
                        new IdentifierNode(PositionData.Test(), "a"),
                        new BinaryOpNode(
                            Pos,
                            new BinaryOpNode(
                                Pos,
                                new NumberLiteralNode(PositionData.Test(), "1"),
                                Operator.Multiply,
                                new NumberLiteralNode(PositionData.Test(), "2")
                            ),
                            Operator.Add,
                            new NumberLiteralNode(PositionData.Test(), "3")
                        )
                    )
                ]
            )
        ));
    }
}