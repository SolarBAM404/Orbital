using OrbitalCore.Parse.Visitors;

namespace OrbitalCore.Parse.Elements;

public class ProbeElement(
    IOrbitalElement expression,
    IOrbitalElement? ifCondition = null,
    List<IOrbitalElement>? elseIfConditions = null,
    IOrbitalElement? elseCondition = null,
    IOrbitalElement? ifThenCondition = null,
    List<IOrbitalElement>? elseIfThenConditions = null,
    IOrbitalElement? elseThenCondition = null
) : IOrbitalElement
{
    public IOrbitalElement IfCondition { get; set; } = ifCondition ?? expression!;
    public List<IOrbitalElement> ElseIfConditions { get; set; } = elseIfConditions ?? new();
    public IOrbitalElement? ElseCondition { get; set; } = elseCondition;
    
    public IOrbitalElement IfThenCondition { get; set; } = ifThenCondition ?? null!;
    public List<IOrbitalElement> ElseIfThenConditions { get; set; } = elseIfThenConditions ?? new();
    public IOrbitalElement? ElseThenCondition { get; set; } = elseThenCondition;
    
    public object? Accept(IOrbitalVisitor visitor)
    {
        return visitor.VisitProbe(this);
    }
}
