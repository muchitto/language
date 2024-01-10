using ErrorReporting;
using Lexing;
using Parsing.Parsers;
using Syntax.Nodes;

namespace Parsing;

public static class Parser
{
    public static ProgramContainerNode Parse(string filename, string source)
    {
        var posData = new PositionData(filename, source);
        var lexer = new Lexer(posData);
        var context = new ParsingContext(lexer);
        var parser = new ProgramParser(context);

        return parser.Parse();
    }
}