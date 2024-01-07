using ErrorReporting;

namespace Lexing;

public enum TokenType
{
    Identifier,
    NumberLiteral,
    StringLiteral,
    Operator,
    Symbol,
    Newline,
    EndOfFile
}

public record struct Token(TokenType Type, PositionData PositionData, string Value = "")
{
    public bool Is(params TokenType[] types)
    {
        return types.Contains(Type);
    }

    public bool Is(TokenType type, string value)
    {
        return Type == type && Value == value;
    }

    public bool IsOperator()
    {
        return Is(TokenType.Operator);
    }

    public Operator ToOperator()
    {
        return Value switch
        {
            "+" => Operator.Add,
            "-" => Operator.Subtract,
            "*" => Operator.Multiply,
            "/" => Operator.Divide,
            "%" => Operator.Modulo,
            "==" => Operator.Equal,
            "!=" => Operator.NotEqual,
            "<" => Operator.LessThan,
            ">" => Operator.GreaterThan,
            "<=" => Operator.LessThanOrEqual,
            ">=" => Operator.GreaterThanOrEqual,
            "&&" => Operator.And,
            "||" => Operator.Or,
            "!" => Operator.Not,
            "&" => Operator.BitwiseAnd,
            "|" => Operator.BitwiseOr,
            "^" => Operator.BitwiseXor,
            "~" => Operator.BitwiseNot,
            "<<" => Operator.BitwiseLeftShift,
            ">>" => Operator.BitwiseRightShift,
            _ => throw new Exception($"Unknown operator: {Value}")
        };
    }
}