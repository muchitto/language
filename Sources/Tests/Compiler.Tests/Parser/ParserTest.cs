using ErrorReporting;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Literal;

namespace Compiler.Tests.Parser;

public class ParserTest
{
    [Fact]
    public void ParseVariableDeclaration()
    {
        var source = @"
            var x = 1
        ";

        var ast = Parsing.Parser.Parse("test", source);

        // Test that the AST is correct
        // it should be:
        // ProgramContainerNode:
        //   - VariableDeclarationNode:
        //       - IdentifierNode: x
        //       - IntegerLiteralNode: 1

        // Write the assert
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