using Lexing;
using Syntax.Nodes;

namespace Parsing.Parsers;

public class AnnotationsParser(ParsingContext context) : Parser<AnnotationsNode>(context)
{
    public override AnnotationsNode Parse()
    {
        var start = PeekToken().PositionData;
        var annotations = new List<AnnotationNode>();
        while (IsNextAndEat(TokenType.Symbol, "@"))
        {
            annotations.Add(ParseAnnotation());
        }

        return new AnnotationsNode(start, annotations);
    }

    private AnnotationNode ParseAnnotation()
    {
        var name = ParseSingleIdentifier();

        if (IsNext(TokenType.Symbol, "("))
        {
            return ParseAnnotationWithArguments(name);
        }

        return new AnnotationNode(name, []);
    }

    private AnnotationNode ParseAnnotationWithArguments(IdentifierNode name)
    {
        ExpectAndEat(TokenType.Symbol, "(", "expected an opening parenthesis for the annotation");

        var annotationArguments = new List<AnnotationArgumentNode>();
        while (!IsNext(TokenType.Symbol, ")"))
        {
            var token = PeekToken();

            if (IsEnd)
            {
                throw new ParseError.ExpectedToken(
                    new Token(TokenType.Symbol, token.PositionData, ")"),
                    token,
                    "expected ')' and not end of file"
                );
            }

            var value = new ExpressionParser(Context).Parse();

            if (value is not ExpressionNode expressionNode)
            {
                throw new ParseError(
                    value.PositionData,
                    "expected an expression as an annotation argument"
                );
            }

            annotationArguments.Add(new AnnotationArgumentNode(token.PositionData, null, expressionNode));
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the annotation");

        return new AnnotationNode(name, annotationArguments);
    }
}