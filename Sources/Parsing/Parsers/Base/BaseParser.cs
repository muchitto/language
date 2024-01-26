using Lexing;
using Syntax.Nodes;

namespace Parsing.Parsers.Base;

public abstract class BaseParser(ParsingContext context)
{
    protected ParsingContext Context { get; } = context;

    protected bool IsEnd => Context.Lexer.IsEnd || PeekToken().Is(TokenType.EndOfFile);

    protected void Expect(TokenType token, string? message)
    {
        var peekToken = PeekToken();

        if (peekToken.Is(token))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token}, got {peekToken}";

        throw new ParseError.ExpectedToken(
            new Token(token, peekToken.PositionData),
            peekToken,
            innerMessage
        );
    }

    protected void ExpectEndOfStatement(string message)
    {
        var peekToken = PeekToken();

        if (peekToken.Is(TokenType.Newline) || peekToken.Is(TokenType.EndOfFile))
        {
            return;
        }

        throw new ParseError.UnexpectedToken(
            peekToken,
            message
        );
    }

    protected void Expect(TokenType token, string value, string? message)
    {
        var peekToken = PeekToken();

        if (peekToken.Is(token, value))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token} with value {value}, got {peekToken}";

        throw new ParseError.ExpectedToken(
            new Token(token, peekToken.PositionData, value),
            peekToken,
            innerMessage
        );
    }

    protected void ExpectIdentifier(string? message)
    {
        var peekToken = PeekToken();

        if (peekToken.Is(TokenType.Identifier))
        {
            return;
        }

        var innerMessage = message ?? $"expected identifier, got {peekToken}";

        throw new ParseError.UnexpectedToken(peekToken, innerMessage);
    }

    protected void ExpectAndEat(TokenType token, string? message)
    {
        Expect(token, message);

        GetNextToken();
    }

    protected void ExpectAndEat(TokenType token, string value, string? message)
    {
        Expect(token, value, message);

        GetNextToken();
    }

    protected void ExpectAndEatNewline()
    {
        var peekToken = PeekToken();

        if (!peekToken.Is(TokenType.Newline))
        {
            throw new ParseError.ExpectedToken(
                new Token(TokenType.Newline, peekToken.PositionData),
                peekToken,
                "expected a newline");
        }

        GetNextToken();
    }

    protected void ExpectNot(TokenType token, string? message)
    {
        var peekToken = PeekToken();

        if (!peekToken.Is(token))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token}, got {peekToken}";

        throw new ParseError.UnexpectedToken(
            peekToken,
            innerMessage
        );
    }

    protected void ExpectNot(TokenType token, string value, string? message)
    {
        var peekToken = PeekToken();

        if (!peekToken.Is(token, value))
        {
            return;
        }

        var innerMessage = message ?? $"expected token {token} with value {value}, got {peekToken}";

        throw new ParseError.UnexpectedToken(
            peekToken,
            innerMessage
        );
    }

    protected bool IsNext(TokenType token)
    {
        return PeekToken().Is(token);
    }

    protected bool IsNext(TokenType token, string value)
    {
        return PeekToken().Is(token, value);
    }

    protected bool IsNextAndEat(TokenType token)
    {
        var peekToken = PeekToken();

        if (!peekToken.Is(token))
        {
            return false;
        }

        GetNextToken();

        return true;
    }

    protected bool IsNextAndEat(TokenType token, string value)
    {
        var peekToken = PeekToken();

        if (!peekToken.Is(token, value))
        {
            return false;
        }

        GetNextToken();

        return true;
    }

    protected void Optional(TokenType token)
    {
        if (PeekToken().Is(token))
        {
            GetNextToken();
        }
    }

    protected IdentifierNode? GetIdentifierIfNext()
    {
        return IsNext(TokenType.Identifier) ? ParseSingleIdentifier() : null;
    }

    protected IdentifierNode ParseSingleIdentifier()
    {
        Expect(TokenType.Identifier, "expected identifier");

        var name = GetNextToken();
        var namePos = name.PositionData;

        return new IdentifierNode(namePos, name.Value);
    }

    protected Token PeekToken()
    {
        return Context.Lexer.PeekToken();
    }

    protected Token GetNextToken()
    {
        return Context.Lexer.GetNextToken();
    }
}