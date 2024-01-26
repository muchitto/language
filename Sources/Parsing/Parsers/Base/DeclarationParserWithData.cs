using Syntax.Nodes.Declaration;

namespace Parsing.Parsers.Base;

public abstract class DeclarationParserWithData<T, T2>(ParsingContext context)
    : BaseDeclarationParser(context) where T : DeclarationNode
{
    public abstract T Parse(T2 data);
}