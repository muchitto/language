using ErrorReporting;

namespace Semantics;

public class SemanticError(PositionData positionData, string message) : CompileError(positionData, message);