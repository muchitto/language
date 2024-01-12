using Syntax.Nodes;

namespace Syntax.NodeHandlers;

public interface IBasicNodeHandler : IStatementListNodeHandler, IAnnotationNodeHandler
{
    public void Handle(IdentifierNode identifierNode);

    public void Handle(FieldAccessNode fieldAccessNode);

    public void Handle(ArrayAccessNode arrayAccessNode);
}