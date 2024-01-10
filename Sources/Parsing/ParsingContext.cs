using Lexing;

namespace Parsing;

public class ParsingContext(Lexer lexer)
{
    public Lexer Lexer { get; } = lexer;
}