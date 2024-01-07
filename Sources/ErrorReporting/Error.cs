namespace ErrorReporting;

public class CompileError : Exception
{
    public CompileError(PositionData positionData, string message)
    {
        PositionData = positionData;
        Message = message;
    }

    public PositionData PositionData { get; set; }
    public string Message { get; set; }
}