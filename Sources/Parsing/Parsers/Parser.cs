using Lexing;
using Syntax.Nodes;

namespace Parsing.Parsers;

public abstract class Parser<T>(Lexer lexer)
    where T : BaseNode
{
    private Lexer Lexer { get; } = lexer;

    public abstract T Parse();
}