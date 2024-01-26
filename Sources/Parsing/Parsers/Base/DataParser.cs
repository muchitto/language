using Syntax.Nodes;

namespace Parsing.Parsers.Base;

public abstract class ParserWithData<T, T2>(ParsingContext context) : BaseParser(context) where T : BaseNode
{
    public abstract T Parse(T2 data);
}