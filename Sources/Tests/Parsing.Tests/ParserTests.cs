namespace Parsing.Tests;

public class ParserTests
{
    [Fact]
    public void VariableDeclaration()
    {
        var program = Parser.Parse("test", "let a = 1");
    }
}