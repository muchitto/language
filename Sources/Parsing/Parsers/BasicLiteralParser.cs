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
            TokenType.Identifier when token.Value == "nil" => ParseNilLiteral(),
            TokenType.Identifier when token.Value is "true" or "false" => ParseBooleanLiteral(),
            _ => throw new ParseError(
                token.PositionData,
                "was trying to parse a basic literal, but got something else"
            )
        };
    }

    private BooleanLiteralNode ParseBooleanLiteral()
    {
        Expect(TokenType.Identifier, "expected boolean literal");

        var token = GetNextToken();

        return new BooleanLiteralNode(token.PositionData, token.Value == "true");
    }

    private BackTickStringLiteralNode ParseBackTickStringLiteral()
    {
        Expect(TokenType.Symbol, "`", "expected back tick string literal");

        var token = GetNextToken();

        return new BackTickStringLiteralNode(token.PositionData, token.Value);
    }

    private NilLiteralNode ParseNilLiteral()
    {
        Expect(TokenType.Symbol, "nil", "expected nil literal");

        var token = GetNextToken();

        return new NilLiteralNode(token.PositionData);
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