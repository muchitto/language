using ErrorReporting;
using Lexing;

namespace Parsing;

public class ParseError(PositionData positionData, string message) : CompileError(positionData, message)
{
    public class UnexpectedToken(Token token, string message) : ParseError(token.PositionData, message)
    {
        public Token Token { get; set; } = token;
    }

    public class ExpectedToken(Token expected, Token got, string message) : ParseError(got.PositionData, message)
    {
        public Token Expected { get; set; } = expected;
        public Token Got { get; set; } = got;
    }
}