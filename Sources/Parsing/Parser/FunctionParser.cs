using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Declaration.Function;

namespace Parsing.Parser;

public partial class Parser
{
    private FunctionDeclarationNode ParseFunctionDeclaration(bool isMethod)
    {
        ExpectAndEat(TokenType.Identifier, "func", "expected func");

        var name = ParseSingleIdentifier();

        var argumentStartToken = Lexer.PeekToken();

        ExpectAndEat(TokenType.Symbol, "(", null);

        var arguments = new List<FunctionArgumentNode>();

        while (!IsNext(TokenType.Symbol, ")"))
        {
            var identifier = ParseSingleIdentifier();

            TypeNode? type = null;
            var isDynamic = IsNext(TokenType.Symbol, "?");

            if (!IsNextAndEat(TokenType.Symbol, "?"))
            {
                type = ParseTypeAnnotation();
            }

            BaseNode? defaultValue = null;

            if (IsNextAndEat(TokenType.Symbol, "="))
            {
                defaultValue = ParseExpressionPrimary();
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

        var body = ParseBody(true);

        return new FunctionDeclarationNode(
            name,
            arguments,
            body,
            canThrow,
            isMethod,
            returnType
        );
    }
}