using Lexing;
using Parsing.Parsers.Base;
using Syntax.Nodes;

namespace Parsing.Parsers;

public struct BodyParserData
{
    public bool IsExpr { get; set; }
}

public class BodyParser(ParsingContext context) : ParserWithData<BodyContainerNode, BodyParserData>(context)
{
    public override BodyContainerNode Parse(BodyParserData data)
    {
        var token = PeekToken();
        var statements = new List<BaseNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var statement = new StatementParser(Context).Parse();

            ExpectNot(TokenType.Symbol, ";", "expected a newline instead of a semicolon as semicolons are not needed");

            ExpectAndEatNewline();

            statements.Add(statement);
        }

        return new BodyContainerNode(token.PositionData, statements, data.IsExpr);
    }
}