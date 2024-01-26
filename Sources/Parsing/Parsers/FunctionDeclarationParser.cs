using Lexing;
using Parsing.Parsers.Base;
using Syntax.Nodes;
using Syntax.Nodes.Declaration;
using Syntax.Nodes.Declaration.Function;

namespace Parsing.Parsers;

public struct FunctionDeclarationParserData
{
    public bool IsMethod { get; set; }
}

public class FunctionDeclarationParser(ParsingContext context)
    : DeclarationParserWithData<FunctionDeclarationNode, FunctionDeclarationParserData>(context)
{
    public override FunctionDeclarationNode Parse(FunctionDeclarationParserData parserData)
    {
        ExpectAndEat(TokenType.Identifier, "func", "expected func");

        DeclarationNameNode name;
        if (IsNext(TokenType.BackTickStringLiteral))
        {
            var token = GetNextToken();

            name = new DeclarationNameNode(token.PositionData, token.Value);
        }
        else
        {
            name = ParseDeclarationName();
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
            var argumentName = ParseDeclarationName();

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

            arguments.Add(new FunctionArgumentNode(argumentName, type, defaultValue, isDynamic));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the arguments");

        return arguments;
    }
}