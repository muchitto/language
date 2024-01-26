using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;
using IdentifierNode = Syntax.Nodes.IdentifierNode;

namespace Parsing.Tests.Struct;

public class StructLiteralTest : ParserTest
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
            new ProgramContainerNode(Pos,
                [
                    new VariableDeclarationNode(
                        new DeclarationNameNode(Pos, "t"),
                        new StructLiteralNode(
                            Pos,
                            [
                                new StructLiteralFieldNode(
                                    Pos,
                                    new IdentifierNode(Pos, "x"),
                                    new NumberLiteralNode(Pos, "1")
                                ),
                                new StructLiteralFieldNode(
                                    Pos,
                                    new IdentifierNode(Pos, "y"),
                                    new NumberLiteralNode(Pos, "2")
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
            new ProgramContainerNode(Pos,
                [
                    new VariableDeclarationNode(
                        new DeclarationNameNode(Pos, "t"),
                        new StructLiteralNode(
                            Pos,
                            [
                                new StructLiteralFieldNode(
                                    Pos,
                                    new IdentifierNode(Pos, "x"),
                                    new NumberLiteralNode(Pos, "1")
                                ),
                                new StructLiteralFieldNode(
                                    Pos,
                                    new IdentifierNode(Pos, "y"),
                                    new NumberLiteralNode(Pos, "2")
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
            new ProgramContainerNode(Pos,
                [
                    new VariableDeclarationNode(
                        new DeclarationNameNode(Pos, "t"),
                        new StructLiteralNode(
                            Pos,
                            [
                                new StructLiteralFieldNode(
                                    Pos,
                                    new IdentifierNode(Pos, "x"),
                                    new NumberLiteralNode(Pos, "1")
                                ),
                                new StructLiteralFieldNode(
                                    Pos,
                                    new IdentifierNode(Pos, "y"),
                                    new NumberLiteralNode(Pos, "2")
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
            new ProgramContainerNode(Pos,
                [
                    new VariableDeclarationNode(
                        new DeclarationNameNode(Pos, "t"),
                        new StructLiteralNode(
                            Pos,
                            [
                                new StructLiteralFieldNode(
                                    Pos,
                                    new IdentifierNode(Pos, "x"),
                                    new NumberLiteralNode(Pos, "1")
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