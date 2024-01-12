using ErrorReporting;

namespace Syntax.Nodes.Literal;

public class BackTickStringLiteralNode(PositionData positionData, string value)
    : StringLiteralNode(positionData, value);