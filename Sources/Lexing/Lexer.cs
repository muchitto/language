using ErrorReporting;

namespace Lexing;

public class Lexer(PositionData positionData)
{
    private static readonly Dictionary<string, TokenType> Symbols = new()
    {
        ["@"] = TokenType.Symbol,
        ["("] = TokenType.Symbol,
        [")"] = TokenType.Symbol,
        ["{"] = TokenType.Symbol,
        ["}"] = TokenType.Symbol,
        ["["] = TokenType.Symbol,
        ["]"] = TokenType.Symbol,
        [";"] = TokenType.Symbol,
        [","] = TokenType.Symbol,
        ["."] = TokenType.Symbol,
        [":"] = TokenType.Symbol,
        ["?"] = TokenType.Symbol,
        ["="] = TokenType.Symbol,
        ["+"] = TokenType.Operator,
        ["-"] = TokenType.Operator,
        ["*"] = TokenType.Operator,
        ["/"] = TokenType.Operator,
        ["%"] = TokenType.Operator,
        ["=="] = TokenType.Operator,
        ["!="] = TokenType.Operator,
        ["<"] = TokenType.Operator,
        [">"] = TokenType.Operator,
        ["<="] = TokenType.Operator,
        [">="] = TokenType.Operator,
        ["&&"] = TokenType.Operator,
        ["||"] = TokenType.Operator,
        ["!"] = TokenType.Operator,
        ["&"] = TokenType.Operator,
        ["|"] = TokenType.Operator,
        ["^"] = TokenType.Operator,
        ["~"] = TokenType.Operator,
        ["<<"] = TokenType.Operator,
        [">>"] = TokenType.Operator
    };

    private Token? _lastToken;
    private bool _lastWasNewLine = true;

    public bool IsEnd => positionData.IsEnd();

    public Token PeekToken()
    {
        if (_lastToken != null)
        {
            return _lastToken.Value;
        }

        var token = GetNextToken();

        _lastToken = token;

        return token;
    }

    public Token GetNextToken()
    {
        if (_lastToken != null)
        {
            var token = _lastToken.Value;

            _lastToken = null;

            return token;
        }

        while (true)
        {
            if (char.IsWhiteSpace(positionData.PeekChar()))
            {
                var chr = positionData.GetChar();

                if (!_lastWasNewLine && chr == '\n')
                {
                    _lastWasNewLine = true;

                    return new Token(TokenType.Newline, positionData with { To = positionData.From });
                }
            }
            else if (positionData.IsNext("//", true))
            {
                positionData.GetUntil("\n");
            }
            else if (positionData.IsNext("/*"))
            {
                var nestedCommentCount = 0;
                while (true)
                {
                    if (positionData.IsNext("/*", true))
                    {
                        nestedCommentCount++;
                    }
                    else if (positionData.IsNext("*/", true))
                    {
                        nestedCommentCount--;

                        if (nestedCommentCount == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        positionData.GetChar();
                    }
                }
            }
            else
            {
                break;
            }
        }

        if (positionData.IsEnd())
        {
            return new Token(TokenType.EndOfFile, positionData with { To = positionData.From });
        }

        _lastWasNewLine = false;

        if (positionData.PeekChar() == '"')
        {
            positionData.GetChar();

            var startPosData = positionData;

            var str = positionData.GetUntil("\"", true);

            return new Token(
                TokenType.StringLiteral,
                startPosData with { To = startPosData.From + str.Length - 1 },
                str
            );
        }

        if (char.IsNumber(positionData.PeekChar()))
        {
            var startPosData = positionData;

            var str = positionData.GetWhile((chr, _, _) => char.IsNumber(chr) || chr == '.');

            return new Token(
                TokenType.NumberLiteral,
                startPosData with { To = startPosData.From + str.Length - 1 },
                str
            );
        }

        if (char.IsLetter(positionData.PeekChar()) || positionData.PeekChar() == '_')
        {
            var startPosData = positionData;

            var str = positionData.GetWhile((chr, _, _) => char.IsLetterOrDigit(chr) || chr == '_');

            return new Token(TokenType.Identifier, startPosData with { To = startPosData.From + str.Length - 1 }, str);
        }

        {
            var startPosData = positionData;

            var str = positionData.GetWhile((chr, _, str) =>
            {
                return Symbols.Keys.Any(symbol => symbol.StartsWith(str + chr));
            });

            if (!Symbols.TryGetValue(str, out var value))
            {
                throw new LexingError(startPosData, $"Unknown symbol {str}");
            }

            return new Token(value, startPosData with { To = startPosData.From + str.Length - 1 }, str);
        }
    }
}