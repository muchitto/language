using Syntax.Nodes;

namespace Syntax.NodeHandlers;

public interface IAnnotationNodeHandler
{
    public void Handle(AnnotationNode annotationNode);

    public void Handle(AnnotationsNode annotationsNode);

    public void Handle(AnnotationArgumentNode annotationArgumentNode);
}