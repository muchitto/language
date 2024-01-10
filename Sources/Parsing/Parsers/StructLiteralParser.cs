using Lexing;
using Syntax.Nodes.Literal;

namespace Parsing.Parsers;

public class StructLiteralParser(ParsingContext context)
    : Parser<StructLiteralNode>(context)
{
    public override StructLiteralNode Parse()
    {
        ExpectAndEat(
            TokenType.Symbol,
            "{",
            "expected an opening curly brace for the struct declaration"
        );

        var fields = new List<StructLiteralFieldNode>();

        var everyFieldHasNewline = IsNextAndEat(TokenType.Newline);
        bool? usesCommas = null;

        while (!IsNext(TokenType.Symbol, "}"))
        {
            var field = ParseStructLiteralField();

            if (usesCommas == null)
            {
                usesCommas = IsNextAndEat(TokenType.Symbol, ",");
            }
            else if (usesCommas.Value && !IsNext(TokenType.Symbol, "}"))
            {
                ExpectAndEat(
                    TokenType.Symbol,
                    ",",
                    "expected a comma"
                );
            }

            string? name = null;

            if (field is { Name: { } identifier })
            {
                name = identifier.Name;
            }

            if (fields.Any(f => f.Name.Name == name))
            {
                throw new ParseError(
                    field.PositionData,
                    $"field {name} already defined in struct literal"
                );
            }

            fields.Add(field);

            if (!IsNext(TokenType.Symbol, "}") && !IsNext(TokenType.Newline))
            {
                everyFieldHasNewline = false;

                if (!usesCommas.Value)
                {
                    throw new ParseError(
                        field.PositionData,
                        "if the struct literal is all on one line, it needs to use commas"
                    );
                }
            }

            if (everyFieldHasNewline)
            {
                ExpectAndEatNewline();
            }
        }

        ExpectAndEat(
            TokenType.Symbol,
            "}",
            "expected an ending curly brace for the struct declaration"
        );

        return new StructLiteralNode(PeekToken().PositionData, fields);
    }

    private StructLiteralFieldNode ParseStructLiteralField()
    {
        var name = ParseSingleIdentifier();

        ExpectAndEat(
            TokenType.Symbol,
            "=",
            "expected an equals sign for the struct literal field"
        );

        var value = new ExpressionParser(Context).Parse();

        return new StructLiteralFieldNode(name.PositionData, name, value);
    }
}