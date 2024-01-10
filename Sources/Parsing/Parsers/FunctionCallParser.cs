using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Expression;

namespace Parsing.Parsers;

public struct FunctionCallParserData
{
    public BaseNode Name { get; set; }
}

public class FunctionCallParser(ParsingContext context)
    : ParserWithData<FunctionCallNode, FunctionCallParserData>(context)
{
    public override FunctionCallNode Parse(FunctionCallParserData data)
    {
        if (IsNextAndEat(TokenType.Symbol, "("))
        {
            return ParseFunctionCallWithParenthesis(data);
        }

        return ParseFunctionCallWithoutParenthesis(data);
    }

    private FunctionCallNode ParseFunctionCallWithParenthesis(FunctionCallParserData data)
    {
        var arguments = new List<FunctionCallArgumentNode>();

        while (!IsNext(TokenType.Symbol, ")"))
        {
            arguments.Add(ParseArgumentWithParenthesis());

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected a closing parenthesis for the function call");

        return new FunctionCallNode(
            data.Name,
            arguments
        );
    }

    private FunctionCallNode ParseFunctionCallWithoutParenthesis(FunctionCallParserData data)
    {
        var arguments = new List<FunctionCallArgumentNode>();

        if (IsEnd)
        {
            return new FunctionCallNode(
                data.Name,
                arguments
            );
        }

        arguments.Add(ParseArgumentWithoutParenthesis());

        if (IsNext(TokenType.Identifier, "do"))
        {
            var doBlock = new DoBlockParser(Context).Parse(new DoBlockParserData
            {
                IsExpr = true
            });

            arguments.Add(new FunctionCallArgumentNode(doBlock.PositionData, null, doBlock));
        }

        Expect(TokenType.Newline, "expected a newline after the function call");

        return new FunctionCallNode(
            data.Name,
            arguments
        );
    }

    private FunctionCallArgumentNode ParseArgumentWithParenthesis()
    {
        var argumentValue = new ExpressionParser(Context).Parse();

        IdentifierNode? argumentName = null;

        if (IsNextAndEat(TokenType.Symbol, ":"))
        {
            if (argumentValue is IdentifierNode argumentValueIdentifier)
            {
                argumentName = argumentValueIdentifier;
                argumentValue = new ExpressionParser(Context).Parse();
            }
            else
            {
                throw new ParseError(
                    argumentValue.PositionData,
                    "argument name needs to be an identifier"
                );
            }
        }

        return new FunctionCallArgumentNode(argumentValue.PositionData, argumentName, argumentValue);
    }

    private FunctionCallArgumentNode ParseArgumentWithoutParenthesis()
    {
        var argumentValue = new ExpressionParser(Context).Parse();

        return new FunctionCallArgumentNode(argumentValue.PositionData, null, argumentValue);
    }
}