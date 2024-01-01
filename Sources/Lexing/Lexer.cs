namespace Lexing;

public class LexerError(PosData posData, string message) : Exception(message)
{
    public PosData PosData { get; set; } = posData;
}

public class Lexer(PosData posData)
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

    public bool IsEnd => posData.IsEnd;

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
            if (char.IsWhiteSpace(posData.PeekChar()))
            {
                var chr = posData.GetChar();

                if (!_lastWasNewLine && chr == '\n')
                {
                    _lastWasNewLine = true;

                    return new Token(TokenType.Newline, posData);
                }
            }
            else if (posData.IsNext("//", true))
            {
                posData.GetUntil("\n");
            }
            else if (posData.IsNext("/*"))
            {
                var nestedCommentCount = 0;
                while (true)
                {
                    if (posData.IsNext("/*", true))
                    {
                        nestedCommentCount++;
                    }
                    else if (posData.IsNext("*/", true))
                    {
                        nestedCommentCount--;

                        if (nestedCommentCount == 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        posData.GetChar();
                    }
                }
            }
            else
            {
                break;
            }
        }

        if (posData.IsEnd)
        {
            return new Token(TokenType.EndOfFile, posData);
        }

        _lastWasNewLine = false;

        if (posData.PeekChar() == '"')
        {
            posData.GetChar();

            var startPosData = posData;

            var str = posData.GetUntil("\"", true);

            return new Token(TokenType.StringLiteral, startPosData, str);
        }

        if (char.IsNumber(posData.PeekChar()))
        {
            var startPosData = posData;

            var str = posData.GetWhile((chr, _, _) => char.IsNumber(chr) || chr == '.');

            return new Token(TokenType.NumberLiteral, startPosData, str);
        }

        if (char.IsLetter(posData.PeekChar()) || posData.PeekChar() == '_')
        {
            var startPosData = posData;

            var str = posData.GetWhile((chr, _, _) => char.IsLetterOrDigit(chr) || chr == '_');

            return new Token(TokenType.Identifier, startPosData, str);
        }

        {
            var startPosData = posData;

            var str = posData.GetWhile((chr, _, str) =>
            {
                return Symbols.Keys.Any(symbol => symbol.StartsWith(str + chr));
            });

            if (!Symbols.TryGetValue(str, out var value))
            {
                throw new LexerError(startPosData, $"Unknown symbol {str}");
            }

            return new Token(value, startPosData, str);
        }
    }
}