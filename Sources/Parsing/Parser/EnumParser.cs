using Lexing;
using Parsing.Nodes.Declaration.Enum;

namespace Parsing.Parser;

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
                funcs.Add(new EnumFunctionNode(token.PosData, ParseFunctionDeclaration()));

                ExpectAndEatNewline();
            }
            else if (IsNext(TokenType.Identifier))
            {
                var identifier = ParseSingleIdentifier();

                if (IsNextAndEat(TokenType.Newline))
                {
                    cases.Add(new EnumCaseNode(identifier.PosData, identifier, []));
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
                                    identifierOrType.PosData,
                                    identifierOrType,
                                    type
                                )
                            );
                        }
                        else
                        {
                            associatedValues.Add(
                                new EnumCaseAssociatedValueNode(
                                    identifierOrType.PosData,
                                    null,
                                    identifierOrType
                                )
                            );
                        }
                    }

                    ExpectAndEatNewline();

                    cases.Add(new EnumCaseNode(identifier.PosData, identifier, associatedValues));
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