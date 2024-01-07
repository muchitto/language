using ErrorReporting;

namespace Lexing;

public class LexingError(PositionData positionData, string message) : CompileError(positionData, message);