using OrbitalCore.Parse.Elements;

namespace OrbitalCore;

public class Scope
{
    private readonly Dictionary<VariableElement, object?> _variables = new();
    public Scope? Parent { get; set; }

    public bool Contains(VariableElement variable)
    {
        foreach (var kvp in _variables)
        {
            if (Equals(kvp.Key, variable))
            {
                return true;
            }
        }
        return Parent?.Contains(variable) ?? false;
    }

    public void Update(VariableElement variable, object? value)
    {
        foreach (var kvp in _variables)
        {
            if (Equals(kvp.Key, variable))
            {
                _variables[kvp.Key] = value;
                return;
            }
        }
        Parent?.Update(variable, value);
    }

    public void Add(VariableElement variable, object? value)
    {
        _variables[variable] = value;
    }

    public object? this[VariableElement variable]
    {
        get
        {
            foreach (var kvp in _variables)
            {
                if (Equals(kvp.Key, variable))
                {
                    return kvp.Value;
                }
            }
            return Parent?[variable];
        }
    }
}