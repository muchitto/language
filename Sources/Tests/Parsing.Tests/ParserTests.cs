using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;

namespace Parsing.Tests;

public class ParserTests
{
    [Fact]
    public void ParseStructLiteral()
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

    [Fact]
    public void ParseVariableDeclaration()
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