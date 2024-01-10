using Lexing;
using Syntax.Nodes;

namespace Parsing.Parsers;

public record struct DoBlockParserData
{
    public bool IsExpr;
}

public class DoBlockParser(ParsingContext context) : ParserWithData<BodyContainerNode, DoBlockParserData>(context)
{
    public override BodyContainerNode Parse(DoBlockParserData data)
    {
        ExpectAndEat(TokenType.Identifier, "do", "expected do");

        var body = new BodyParser(Context).Parse(new BodyParserData
        {
            IsExpr = data.IsExpr
        });

        ExpectAndEat(TokenType.Identifier, "end", "expected an end");

        return body;
    }
}