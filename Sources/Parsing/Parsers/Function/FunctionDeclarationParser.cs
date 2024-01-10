using Lexing;
using Parsing.Parsers.Type;
using Syntax.Nodes;
using Syntax.Nodes.Declaration.Function;

namespace Parsing.Parsers.Function;

public struct FunctionDeclarationParserData
{
    public bool IsMethod { get; set; }
}

public class FunctionDeclarationParser(ParsingContext context)
    : ParserWithData<FunctionDeclarationNode, FunctionDeclarationParserData>(context)
{
    public override FunctionDeclarationNode Parse(FunctionDeclarationParserData parserData)
    {
        ExpectAndEat(TokenType.Identifier, "func", "expected func");

        var name = ParseSingleIdentifier();

        var argumentStartToken = PeekToken();

        ExpectAndEat(TokenType.Symbol, "(", null);

        var arguments = new List<FunctionArgumentNode>();

        while (!IsNext(TokenType.Symbol, ")"))
        {
            var identifier = ParseSingleIdentifier();

            TypeNode? type = null;
            var isDynamic = IsNext(TokenType.Symbol, "?");

            if (!IsNextAndEat(TokenType.Symbol, "?"))
            {
                type = new TypeAnnotationDataParser(context).Parse();
            }

            BaseNode? defaultValue = null;

            if (IsNextAndEat(TokenType.Symbol, "="))
            {
                defaultValue = new ExpressionParser(context).Parse();
            }

            arguments.Add(new FunctionArgumentNode(identifier, type, defaultValue, isDynamic));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the arguments");

        var canThrow = IsNextAndEat(TokenType.Identifier, "throws");

        var returnType = GetIdentifierIfNext();

        ExpectAndEatNewline();

        var body = new BodyParser(context).Parse(new BodyParserData
        {
            IsExpr = false
        });

        return new FunctionDeclarationNode(
            name,
            arguments,
            body,
            canThrow,
            parserData.IsMethod,
            returnType
        );
    }
}