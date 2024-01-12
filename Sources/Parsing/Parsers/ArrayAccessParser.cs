using Lexing;
using Syntax.Nodes;

namespace Parsing.Parsers;

public class ArrayAccessParserData
{
    public BaseNode Left { get; set; }
}

public class ArrayAccessParser(ParsingContext context)
    : ParserWithData<ArrayAccessNode, ArrayAccessParserData>(context)
{
    public override ArrayAccessNode Parse(ArrayAccessParserData data)
    {
        ExpectAndEat(TokenType.Symbol, "[", "expected an opening square bracket for the array access");

        var right = new ExpressionParser(Context).Parse();

        ExpectAndEat(TokenType.Symbol, "]", "expected a closing square bracket for the array access");

        return new ArrayAccessNode(data.Left, (ExpressionNode)right);
    }
}