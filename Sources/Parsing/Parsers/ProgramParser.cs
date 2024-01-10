using Lexing;
using Syntax.Nodes;

namespace Parsing.Parsers;

public class ProgramParser(ParsingContext context) : Parser<ProgramContainerNode>(context)
{
    public override ProgramContainerNode Parse()
    {
        var body = new List<BaseNode>();

        var token = PeekToken();

        while (!IsEnd)
        {
            var statement = new StatementParser(Context).Parse();

            body.Add(statement);

            if (IsNext(TokenType.EndOfFile))
            {
                break;
            }

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();
        }

        return new ProgramContainerNode(token.PositionData, body);
    }
}