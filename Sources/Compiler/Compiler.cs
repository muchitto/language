using ErrorReporting;
using Parsing;
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

            var ast = Parser.Parse(filename, testFile);

            var semanticChecks = new SemanticChecks();
            semanticChecks.RunPass(ast);
        }
        catch (CompileError e)
        {
            Console.WriteLine(e.PositionData.GetFullErrorMessage(e.Message));
        }
    }
}