using Lexing;

namespace Compiler.Tests.Parser;

public class ParserTest
{
    [Fact]
    public void Parser()
    {
        var posData = new PosData("test", "test");
        var parser = new Parsing.Parser.Parser(new Lexer(posData));
        var ast = parser.Parse();

        Assert.NotNull(ast);
    }
}