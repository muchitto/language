using Parsing.Parsers.Base;
using Syntax.Nodes.Declaration.Interface;

namespace Parsing.Parsers;

public class InterfaceDeclarationParser(ParsingContext context) : Parser<InterfaceDeclarationNode>(context)
{
    public override InterfaceDeclarationNode Parse()
    {
        throw new NotImplementedException();

        /*
        ExpectAndEat(TokenType.Identifier, "interface", "expected interface");

        var name = ParseSingleIdentifier(false);

        ExpectAndEatNewline();

        var functions = new List<FunctionDeclarationNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var function = ParseFunctionDeclaration();

            ExpectAndEatNewline();

            functions.Add(function);
        }

        //return new InterfaceDeclarationNode(name, functions, );
        */
    }
}