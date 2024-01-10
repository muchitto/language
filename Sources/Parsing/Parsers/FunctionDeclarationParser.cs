using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Declaration.Function;

namespace Parsing.Parsers;

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

        IdentifierNode name;
        if (IsNext(TokenType.BackTickStringLiteral))
        {
            var token = GetNextToken();

            name = new IdentifierNode(token.PositionData, token.Value);
        }
        else
        {
            name = ParseSingleIdentifier();
        }

        var arguments = ParseArguments();
        var canThrow = IsNextAndEat(TokenType.Identifier, "throws");

        var returnType = GetIdentifierIfNext();

        ExpectAndEatNewline();

        var body = new BodyParser(Context).Parse(new BodyParserData
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

    private List<FunctionArgumentNode> ParseArguments()
    {
        var arguments = new List<FunctionArgumentNode>();

        ExpectAndEat(TokenType.Symbol, "(", "expected an opening parenthesis for the arguments");

        while (!IsNext(TokenType.Symbol, ")"))
        {
            var identifier = ParseSingleIdentifier();

            TypeNode? type = null;
            var isDynamic = IsNext(TokenType.Symbol, "?");

            if (!IsNextAndEat(TokenType.Symbol, "?"))
            {
                type = new TypeAnnotationParser(Context).Parse();
            }

            BaseNode? defaultValue = null;

            if (IsNextAndEat(TokenType.Symbol, "="))
            {
                defaultValue = new ExpressionParser(Context).Parse();
            }

            arguments.Add(new FunctionArgumentNode(identifier, type, defaultValue, isDynamic));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the arguments");

        return arguments;
    }
}