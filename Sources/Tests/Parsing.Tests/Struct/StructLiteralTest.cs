using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;

namespace Parsing.Tests.Struct;

public class StructLiteralTest
{
    [Fact]
    public void ParseStructLiteralWithoutCommasButOnOneLine()
    {
        const string source = "var t = { x = 1 y = 2 }";

        // This should fail because there are no commas between the fields.
        Assert.Throws<ParseError>(() => Parser.Parse("test", source));
    }

    [Fact]
    public void ParseStructLiteralWithCommas()
    {
        const string source = "var t = { x = 1, y = 2 }";

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(PositionData.Test(), "t"),
                        new StructLiteralNode(
                            PositionData.Test(),
                            [
                                new StructLiteralFieldNode(
                                    PositionData.Test(),
                                    new IdentifierNode(PositionData.Test(), "x"),
                                    new NumberLiteralNode(PositionData.Test(), "1")
                                ),
                                new StructLiteralFieldNode(
                                    PositionData.Test(),
                                    new IdentifierNode(PositionData.Test(), "y"),
                                    new NumberLiteralNode(PositionData.Test(), "2")
                                )
                            ]
                        ),
                        false,
                        null,
                        false
                    )
                ]
            )
        ));
    }

    [Fact]
    public void ParseStructLiteralMultilineWithCommas()
    {
        const string source = """
                              var t = {
                                x = 1,
                                y = 2,
                              }
                              """;

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(PositionData.Test(), "t"),
                        new StructLiteralNode(
                            PositionData.Test(),
                            [
                                new StructLiteralFieldNode(
                                    PositionData.Test(),
                                    new IdentifierNode(PositionData.Test(), "x"),
                                    new NumberLiteralNode(PositionData.Test(), "1")
                                ),
                                new StructLiteralFieldNode(
                                    PositionData.Test(),
                                    new IdentifierNode(PositionData.Test(), "y"),
                                    new NumberLiteralNode(PositionData.Test(), "2")
                                )
                            ]
                        ),
                        false,
                        null,
                        false
                    )
                ]
            )
        ));
    }

    [Fact]
    public void ParseStructLiteralMultiline()
    {
        const string source = """
                              var t = {
                                x = 1
                                y = 2
                              }
                              """;

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(PositionData.Test(), "t"),
                        new StructLiteralNode(
                            PositionData.Test(),
                            [
                                new StructLiteralFieldNode(
                                    PositionData.Test(),
                                    new IdentifierNode(PositionData.Test(), "x"),
                                    new NumberLiteralNode(PositionData.Test(), "1")
                                ),
                                new StructLiteralFieldNode(
                                    PositionData.Test(),
                                    new IdentifierNode(PositionData.Test(), "y"),
                                    new NumberLiteralNode(PositionData.Test(), "2")
                                )
                            ]
                        ),
                        false,
                        null,
                        false
                    )
                ]
            )
        ));
    }

    [Fact]
    public void ParseStructLiteralOnOneLine()
    {
        const string source = "var t = { x = 1 }";

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(PositionData.Test(),
                [
                    new VariableDeclarationNode(
                        new IdentifierNode(PositionData.Test(), "t"),
                        new StructLiteralNode(
                            PositionData.Test(),
                            [
                                new StructLiteralFieldNode(
                                    PositionData.Test(),
                                    new IdentifierNode(PositionData.Test(), "x"),
                                    new NumberLiteralNode(PositionData.Test(), "1")
                                )
                            ]
                        ),
                        false,
                        null,
                        false
                    )
                ]
            )
        ));
    }
}