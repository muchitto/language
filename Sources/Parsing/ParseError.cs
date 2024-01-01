using Lexing;

namespace Parsing;

public class ParseError(PosData posData, string message) : Exception(message)
{
    public PosData PosData { get; set; } = posData;

    public class UnexpectedToken(Token token, string message) : ParseError(token.PosData, message)
    {
        public Token Token { get; set; } = token;
    }

    public class ExpectedToken(Token expected, Token got, string message) : ParseError(got.PosData, message)
    {
        public Token Expected { get; set; } = expected;
        public Token Got { get; set; } = got;
    }
}
