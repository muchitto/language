using Syntax.Nodes;

namespace Parsing.Parsers;

public abstract class Parser<T>(ParsingContext context) : BaseParser(context) where T : BaseNode
{
    public abstract T Parse();
}