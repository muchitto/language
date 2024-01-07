using ErrorReporting;
using Lexing;
using Parsing.Parser;
using Semantics;

namespace Language;

public class Compiler
{
    public Compiler()
    {
        try
        {
            var filename = "TestData/test.txt";
            var testFile = File.ReadAllText(filename);

            var posData = new PositionData(filename, testFile);
            var lexer = new Lexer(posData);
            var parser = new Parser(lexer);
            var ast = parser.Parse();

            var semanticChecks = new SemanticChecks();
            semanticChecks.RunPass(ast);
        }
        catch (CompileError e)
        {
            Console.WriteLine(e.PositionData.GetFullErrorMessage(e.Message));
        }
    }
}