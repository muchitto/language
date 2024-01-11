using ErrorReporting;
using Lexing;

namespace Parsing.Tests;

public abstract class ParserTest
{
    public PositionData Pos => PositionData.Test();

    public ParsingContext GetContextWith(string filename, string source)
    {
        var positionData = new PositionData(filename, source);
        var tokens = new Lexer(positionData);
        return new ParsingContext(tokens);
    }
}