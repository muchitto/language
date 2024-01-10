using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Declaration.Closure;
using Syntax.Nodes.Expression;
using Syntax.Nodes.Literal;

namespace Parsing.Tests;

public class FunctionCall : ParserTest
{
    [Fact]
    public void Test_Function_Call_With_Parenthesis_And_Without_Arguments()
    {
        var source = "foo()";
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new FunctionCallNode(
                        new IdentifierNode(PositionData.Test(), "foo"),
                        new List<FunctionCallArgumentNode>()
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_Function_Call_Without_Parenthesis_And_Without_Arguments()
    {
        var source = "foo";
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new FunctionCallNode(
                        new IdentifierNode(PositionData.Test(), "foo"),
                        new List<FunctionCallArgumentNode>()
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_Function_Call_Without_Parenthesis_And_With_One_Argument()
    {
        var source = """ foo "test" """;
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new FunctionCallNode(
                        new IdentifierNode(PositionData.Test(), "foo"),
                        [
                            new FunctionCallArgumentNode(
                                PositionData.Test(),
                                null,
                                new StringLiteralNode(PositionData.Test(), "test")
                            )
                        ]
                    )
                ]
            )
        ));
    }

    [Fact]
    public void Test_Function_Call_Without_Parenthesis_And_A_Closure()
    {
        var source = """
                     foo "test" do
                     end
                     """;
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new FunctionCallNode(
                        new IdentifierNode(PositionData.Test(), "foo"),
                        [
                            new FunctionCallArgumentNode(
                                PositionData.Test(),
                                null,
                                new StringLiteralNode(PositionData.Test(), "test")
                            ),
                            new FunctionCallArgumentNode(
                                PositionData.Test(),
                                null,
                                new ClosureNode(
                                    PositionData.Test(),
                                    [],
                                    new BodyContainerNode(PositionData.Test(),
                                        [
                                            new NumberLiteralNode(PositionData.Test(), "1")
                                        ],
                                        false
                                    )
                                )
                            )
                        ]
                    )
                ]
            )
        ));
    }
}