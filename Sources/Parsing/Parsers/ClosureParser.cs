using Lexing;
using Syntax.Nodes;
using Syntax.Nodes.Declaration.Closure;

namespace Parsing.Parsers;

public record struct ClosureParserData
{
    public bool IsExpr;
}

public class ClosureParser(ParsingContext context) : ParserWithData<ClosureNode, ClosureParserData>(context)
{
    public override ClosureNode Parse(ClosureParserData data)
    {
        ExpectAndEat(TokenType.Identifier, "do", "expected do");

        var positionData = PeekToken().PositionData;

        var arguments = ParseArguments();

        var onTheSameLine = !IsNextAndEat(TokenType.Newline);

        BodyContainerNode bodyContainerNode;
        if (onTheSameLine)
        {
            var statement = new StatementParser(Context).Parse();

            bodyContainerNode = new BodyContainerNode(
                statement.PositionData,
                [
                    statement
                ],
                false
            );

            ExpectAndEat(TokenType.Identifier, "end", "expected an end");
        }
        else
        {
            bodyContainerNode = new BodyParser(Context).Parse(new BodyParserData
            {
                IsExpr = data.IsExpr
            });
        }

        return new ClosureNode(positionData, arguments, bodyContainerNode);
    }

    private List<ClosureArgumentNode> ParseArguments()
    {
        var arguments = new List<ClosureArgumentNode>();

        var isUsingParenthesis = IsNextAndEat(TokenType.Symbol, "(");

        while (!IsNext(TokenType.Identifier, "end") && !IsNext(TokenType.Newline))
        {
            var name = ParseSingleIdentifier();
            TypeNode? typeNode = null;

            if (IsNext(TokenType.Identifier))
            {
                typeNode = new TypeAnnotationParser(Context).Parse();
            }

            arguments.Add(new ClosureArgumentNode(name, typeNode));

            if (isUsingParenthesis)
            {
                if (IsNextAndEat(TokenType.Symbol, ")"))
                {
                    break;
                }
            }

            if (!IsNextAndEat(TokenType.Symbol, ","))
            {
                break;
            }
        }

        return arguments;
    }
}