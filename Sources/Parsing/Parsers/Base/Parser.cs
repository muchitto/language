using Syntax.Nodes;

namespace Parsing.Parsers.Base;

public abstract class Parser<T>(ParsingContext context) : BaseParser(context) where T : BaseNode
{
    public abstract T Parse();
}