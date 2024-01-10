using Lexing;
using Syntax.Nodes;

namespace Parsing.Parsers;

public class AnnotationParser(ParsingContext context) : Parser<AnnotationNode>(context)
{
    public override AnnotationNode Parse()
    {
        ExpectAndEat(TokenType.Symbol, "@", "expected @");

        throw new NotImplementedException();

        /*
        var name = ParseSingleIdentifier(false);

        ExpectAndEat(TokenType.Symbol, "(", "expected an opening parenthesis for the annotation");

        var annotationArguments = new List<AnnotationArgumentNode>();
        while (!IsNext(TokenType.Symbol, ")"))
        {
            var token = Lexer.PeekToken();

            if (IsEnd)
            {
                throw new ParseError.ExpectedToken(
                    new Token(TokenType.Symbol, token.PosData, ")"),
                    token,
                    "expected ')' and not end of file"
                );
            }

            var value = ParseExpressionPrimary();

            // annotationArguments.Add(new AnnotationArgumentNode(token.PosData, null, value));
        }

        ExpectAndEat(TokenType.Symbol, ")", "expected an ending parenthesis for the annotation");

        //return new AnnotationNode(name, annotationArguments, null);
        */
    }
}