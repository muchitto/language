using Syntax.Nodes;
using Syntax.Nodes.Declaration.Function;

namespace Parsing.Tests;

public class Annotations : ParserTest
{
    [Fact]
    public void Test_Function_With_A_Single_Annotation_With_No_Arguments()
    {
        const string source = """
                                  @Annotation
                                  func main()
                                  end
                              """;

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(Pos,
                [
                    new FunctionDeclarationNode(
                        new IdentifierNode(Pos, "main"),
                        [],
                        new BodyContainerNode(Pos, [], false),
                        false,
                        false,
                        null
                    )
                    {
                        Annotations = new AnnotationsNode(
                            Pos,
                            [
                                new AnnotationNode(
                                    new IdentifierNode(Pos, "Annotation"),
                                    []
                                )
                            ]
                        )
                    }
                ]
            )
        ));
    }

    [Fact]
    public void Test_Function_With_A_Single_Annotation_With_No_Arguments_On_Same_Line_As_Declaration()
    {
        const string source = """
                                  @Annotation func main()
                                  end
                              """;

        var ast = Parser.Parse("test", source);

        Assert.True(ast.TestEquals(
            new ProgramContainerNode(Pos,
                [
                    new FunctionDeclarationNode(
                        new IdentifierNode(Pos, "main"),
                        [],
                        new BodyContainerNode(Pos, [], false),
                        false,
                        false,
                        null
                    )
                    {
                        Annotations = new AnnotationsNode(
                            Pos,
                            [
                                new AnnotationNode(
                                    new IdentifierNode(Pos, "Annotation"),
                                    []
                                )
                            ]
                        )
                    }
                ]
            )
        ));
    }
}