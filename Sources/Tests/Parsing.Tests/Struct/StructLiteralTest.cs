using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;

namespace Parsing.Tests.Struct;

public class StructLiteralTest
{
    [Fact]
    public void Test_Struct_Literal_Without_Commas_On_One_Line()
    {
        const string source = "var t = { x = 1 y = 2 }";

        // This should fail because there are no commas between the fields.
        Assert.Throws<ParseError>(() => Parser.Parse("test", source));
    }

    [Fact]
    public void Test_Struct_Literal_With_Commas_On_One_Line()
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
    public void Test_Struct_Literal_With_Commas_On_Multiple_Lines()
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
    public void Test_Struct_Literal_Without_Commas_On_Multiple_Lines()
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
    public void Test_Struct_Literal_On_One_Line()
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