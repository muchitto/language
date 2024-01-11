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
            new ProgramContainerNode(Pos,
                [
                    new FunctionCallNode(
                        new IdentifierNode(Pos, "foo"),
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
            new ProgramContainerNode(Pos,
                [
                    new FunctionCallNode(
                        new IdentifierNode(Pos, "foo"),
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
            new ProgramContainerNode(Pos,
                [
                    new FunctionCallNode(
                        new IdentifierNode(Pos, "foo"),
                        [
                            new FunctionCallArgumentNode(
                                Pos,
                                null,
                                new StringLiteralNode(Pos, "test")
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
            new ProgramContainerNode(Pos,
                [
                    new FunctionCallNode(
                        new IdentifierNode(Pos, "foo"),
                        [
                            new FunctionCallArgumentNode(
                                Pos,
                                null,
                                new StringLiteralNode(Pos, "test")
                            ),
                            new FunctionCallArgumentNode(
                                Pos,
                                null,
                                new ClosureNode(
                                    Pos,
                                    [],
                                    new BodyContainerNode(Pos,
                                        [
                                            new NumberLiteralNode(Pos, "1")
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

    [Fact]
    public void Test_Function_Call_Without_Parenthesis_And_A_Closure_With_Arguments()
    {
        var source = """
                        foo "test" do (bar, thing)
                            print "hah"
                        end
                     """;

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(Pos,
                [
                    new FunctionCallNode(
                        new IdentifierNode(Pos, "foo"),
                        [
                            new FunctionCallArgumentNode(
                                Pos,
                                null,
                                new StringLiteralNode(Pos, "test")
                            ),
                            new FunctionCallArgumentNode(
                                Pos,
                                null,
                                new ClosureNode(
                                    Pos,
                                    [
                                        new ClosureArgumentNode(
                                            new IdentifierNode(Pos, "bar"),
                                            null
                                        ),
                                        new ClosureArgumentNode(
                                            new IdentifierNode(Pos, "thing"),
                                            null
                                        )
                                    ],
                                    new BodyContainerNode(Pos,
                                        [
                                            new FunctionCallNode(
                                                new IdentifierNode(Pos, "print"),
                                                [
                                                    new FunctionCallArgumentNode(
                                                        Pos,
                                                        null,
                                                        new StringLiteralNode(Pos, "hah")
                                                    )
                                                ]
                                            )
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

    [Fact]
    public void Test_Function_Call_With_Parenthesis_And_A_Closure_With_Arguments_On_The_Same_Line()
    {
        const string source = """
                                 foo "test" do (bar, thing) print "hah" end
                              """;
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(Pos,
                [
                    new FunctionCallNode(
                        new IdentifierNode(Pos, "foo"),
                        [
                            new FunctionCallArgumentNode(
                                Pos,
                                null,
                                new StringLiteralNode(Pos, "test")
                            ),
                            new FunctionCallArgumentNode(
                                Pos,
                                null,
                                new ClosureNode(
                                    Pos,
                                    [
                                        new ClosureArgumentNode(
                                            new IdentifierNode(Pos, "bar"),
                                            null
                                        ),
                                        new ClosureArgumentNode(
                                            new IdentifierNode(Pos, "thing"),
                                            null
                                        )
                                    ],
                                    new BodyContainerNode(Pos,
                                        [
                                            new FunctionCallNode(
                                                new IdentifierNode(Pos, "print"),
                                                [
                                                    new FunctionCallArgumentNode(
                                                        Pos,
                                                        null,
                                                        new StringLiteralNode(Pos, "hah")
                                                    )
                                                ]
                                            )
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