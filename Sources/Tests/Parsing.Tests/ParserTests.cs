namespace Parsing.Tests;

public class ParserTests
{
    [Fact]
    public void VariableDeclaration()
    {
        var program = Parser.Parser.Parse("test", "let a = 1");
    }
}