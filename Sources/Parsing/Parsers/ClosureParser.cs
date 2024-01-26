using Lexing;
using Parsing.Parsers.Base;
using Syntax.Nodes;
using Syntax.Nodes.Declaration.Closure;

namespace Parsing.Parsers;

public record struct ClosureParserData
{
    public bool IsExpr;
}

public class ClosureParser(ParsingContext context)
    : DeclarationParserWithData<ClosureDeclarationNode, ClosureParserData>(context)
{
    public override ClosureDeclarationNode Parse(ClosureParserData data)
    {
        ExpectAndEat(TokenType.Identifier, "do", "expected do");

        var positionData = PeekToken().PositionData;

        var argumentData = ParseArguments();

        var onTheSameLine = !IsNextAndEat(TokenType.Newline);

        if (!argumentData.IsUsingParenthesis
            && onTheSameLine
            && !IsNext(TokenType.Identifier, "end")
            && !IsNext(TokenType.Newline))
        {
            throw new ParseError(
                PeekToken().PositionData,
                "must use parenthesis on the arguments when declaring statements on the same line as the arguments"
            );
        }

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

            ExpectAndEat(
                TokenType.Identifier,
                "end",
                "expected an end after the statement if it's all on the same line"
            );
        }
        else
        {
            bodyContainerNode = new BodyParser(Context).Parse(new BodyParserData
            {
                IsExpr = data.IsExpr
            });
        }

        return new ClosureDeclarationNode(positionData, argumentData.Arguments, bodyContainerNode);
    }

    private ArgumentListData ParseArguments()
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

        return new ArgumentListData
        {
            IsUsingParenthesis = isUsingParenthesis,
            Arguments = arguments
        };
    }

    private struct ArgumentListData
    {
        public bool IsUsingParenthesis;
        public List<ClosureArgumentNode> Arguments;
    }
}