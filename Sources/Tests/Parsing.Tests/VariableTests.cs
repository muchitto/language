using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;

namespace Parsing.Tests;

public class VariableTests
{
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