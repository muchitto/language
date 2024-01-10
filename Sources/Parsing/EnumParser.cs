using Lexing;
using Syntax.Nodes.Declaration.Enum;

namespace Parsing;

public partial class Parser
{
    private EnumDeclarationNode ParseEnum(bool isExpr)
    {
        var startToken = Lexer.PeekToken();

        ExpectAndEat(TokenType.Identifier, "enum", "expected enum");

        var name = ParseSingleIdentifier();

        ExpectAndEatNewline();

        var cases = new List<EnumCaseNode>();
        var funcs = new List<EnumFunctionNode>();

        while (!IsNextAndEat(TokenType.Identifier, "end"))
        {
            var token = Lexer.PeekToken();
            if (IsNext(TokenType.Identifier, "func"))
            {
                funcs.Add(new EnumFunctionNode(token.PositionData, ParseFunctionDeclaration(true)));

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

        return new EnumDeclarationNode(name, cases, funcs);
    }
}