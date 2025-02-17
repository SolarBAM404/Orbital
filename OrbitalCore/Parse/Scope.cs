public class Scope
    {
        private readonly Dictionary<string, object?> _variables = new();
        public Scope? Parent { get; }
    
        public Scope(Scope? parentScope = null)
        {
            Parent = parentScope;
        }
    
        public void DefineVariable(string name, object? value)
        {
            _variables[name] = value;
        }
    
        public object? GetVariable(string name)
        {
            if (_variables.TryGetValue(name, out var value))
            {
                return value;
            }
            if (Parent == null)
            {
                return null;
            }
            return Parent?.GetVariable(name);
        }
    
        public void SetVariable(string name, object? value)
        {
            if (_variables.ContainsKey(name))
            {
                _variables[name] = value;
            }
            else
            {
                Parent?.SetVariable(name, value);
            }
        }
    }