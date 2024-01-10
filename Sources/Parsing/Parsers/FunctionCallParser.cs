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
        var arguments = new List<FunctionCallArgumentNode>();

        ExpectAndEat(
            TokenType.Symbol,
            "(",
            "expected an opening parenthesis for the arguments"
        );

        while (!IsNext(TokenType.Symbol, ")"))
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

            arguments.Add(new FunctionCallArgumentNode(argumentValue.PositionData, argumentName, argumentValue));

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        ExpectAndEat(
            TokenType.Symbol,
            ")",
            "expected an ending parenthesis for the arguments"
        );

        return new FunctionCallNode(data.Name, arguments);
    }
}