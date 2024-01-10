using Lexing;
using Syntax.Nodes.Literal;

namespace Parsing.Parsers;

public class BasicLiteralParser(ParsingContext context)
    : Parser<LiteralNode>(context)
{
    public override LiteralNode Parse()
    {
        var token = PeekToken();

        return token.Type switch
        {
            TokenType.StringLiteral => ParseStringLiteral(),
            TokenType.NumberLiteral => ParseNumberLiteral(),
            TokenType.BackTickStringLiteral => ParseBackTickStringLiteral(),
            _ => throw new ParseError(token.PositionData, "expected a string or number literal")
        };
    }

    private BackTickStringLiteralNode ParseBackTickStringLiteral()
    {
        Expect(TokenType.Symbol, "`", "expected back tick string literal");

        var token = GetNextToken();

        return new BackTickStringLiteralNode(token.PositionData, token.Value);
    }

    private StringLiteralNode ParseStringLiteral()
    {
        Expect(TokenType.StringLiteral, "expected string literal");

        var token = GetNextToken();

        return new StringLiteralNode(token.PositionData, token.Value);
    }

    private NumberLiteralNode ParseNumberLiteral()
    {
        Expect(TokenType.NumberLiteral, "expected number literal");

        var token = GetNextToken();

        return new NumberLiteralNode(token.PositionData, token.Value);
    }
}