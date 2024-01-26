using Lexing;
using Parsing.Parsers.Base;
using Syntax.Nodes.Declaration;

namespace Parsing.Parsers;

public class TypeAliasParser(ParsingContext context) : DeclarationParser<TypeAliasDeclarationNode>(context)
{
    public override TypeAliasDeclarationNode Parse()
    {
        ExpectAndEat(TokenType.Identifier, "type", "expected type");

        var name = ParseDeclarationName();

        ExpectAndEat(TokenType.Symbol, "=", "expected an equals sign for the type alias");

        var type = new TypeAnnotationParser(Context).Parse();

        return new TypeAliasDeclarationNode(name, type);
    }
}