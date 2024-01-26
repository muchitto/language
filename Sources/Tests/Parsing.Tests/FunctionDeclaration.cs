using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Function;
using Syntax.Nodes.Type;
using DeclarationName = Syntax.Nodes.IdentifierNode;

namespace Parsing.Tests;

public class FunctionDeclaration : ParserTest
{
    /*
     func test(t int)
           var i = t
       end


     */
    [Fact]
    public void Test_Function_Declaration_With_One_Argument()
    {
        const string source = """
                              func test(t int)
                                  var i = t
                              end
                              """;
        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(Pos,
                [
                    new FunctionDeclarationNode(
                        new DeclarationNameNode(Pos, "test"),
                        [
                            new FunctionArgumentNode(
                                new DeclarationNameNode(Pos, "t"),
                                new IdentifierTypeNode(Pos, "int"),
                                null,
                                false
                            )
                        ],
                        new BodyContainerNode(Pos, [
                                new VariableDeclarationNode(
                                    new DeclarationNameNode(Pos, "i"),
                                    new DeclarationName(Pos, "t"),
                                    false,
                                    null,
                                    false
                                )
                            ],
                            false
                        ),
                        false,
                        false,
                        null
                    )
                ]
            )
        ));
    }
}