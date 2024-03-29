using Lexing;
using Parsing.Parsers.Base;
using Syntax.Nodes.Declaration.Enum;

namespace Parsing.Parsers;

public class EnumDeclarationParser(ParsingContext context) : DeclarationParser<EnumDeclarationNode>(context)
{
    public override EnumDeclarationNode Parse()
    {
        var startToken = PeekToken();

        ExpectAndEat(TokenType.Identifier, "enum", "expected enum");

        var name = ParseDeclarationName();

        ExpectAndEatNewline();

        var cases = new List<EnumCaseNode>();
        var functions = new List<EnumFunctionNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var token = PeekToken();
            if (IsNext(TokenType.Identifier, "func"))
            {
                var functionDeclaration =
                    new FunctionDeclarationParser(Context).Parse(new FunctionDeclarationParserData
                    {
                        IsMethod = true
                    });

                functions.Add(new EnumFunctionNode(token.PositionData, functionDeclaration));

                ExpectAndEatNewline();
            }
            else if (IsNext(TokenType.Identifier))
            {
                var identifier = ParseSingleIdentifier();

                if (IsNextAndEat(TokenType.Newline))
                {
                    cases.Add(new EnumCaseNode(identifier.PositionData, identifier, []));
                }
                else
                {
                    ExpectAndEat(TokenType.Symbol, "(", null);

                    var associatedValues = new List<EnumCaseAssociatedValueNode>();

                    while (!IsNextAndEat(TokenType.Symbol, ")"))
                    {
                        var identifierOrType = ParseSingleIdentifier();
                        var type = GetIdentifierIfNext();

                        if (type != null)
                        {
                            associatedValues.Add(
                                new EnumCaseAssociatedValueNode(
                                    identifierOrType.PositionData,
                                    identifierOrType,
                                    type
                                )
                            );
                        }
                        else
                        {
                            associatedValues.Add(
                                new EnumCaseAssociatedValueNode(
                                    identifierOrType.PositionData,
                                    null,
                                    identifierOrType
                                )
                            );
                        }
                    }

                    ExpectAndEatNewline();

                    cases.Add(new EnumCaseNode(identifier.PositionData, identifier, associatedValues));
                }
            }
            else
            {
                throw new ParseError.UnexpectedToken(
                    token,
                    "expected enum case (func, identifier)"
                );
            }
        }

        return new EnumDeclarationNode(name, cases, functions);
    }
}