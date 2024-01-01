using Lexing;
using Parsing;
using Parsing.Parser;
using Semantics;

namespace Language;

public class Compiler
{
    public Compiler()
    {
        var filename = "TestData/test.txt";
        var testFile = File.ReadAllText(filename);

        var posData = new PosData(filename, testFile);
        var lexer = new Lexer(posData);
        var parser = new Parser(lexer);
        var ast = parser.Parse();

        var semanticChecks = new SemanticChecks();
        semanticChecks.RunPass(ast);
    }
}