namespace Lexing;

public class LexingError(PosData posData, string message) : Exception(message)
{
    public PosData PosData { get; set; } = posData;
}