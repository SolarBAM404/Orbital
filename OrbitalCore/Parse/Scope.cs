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
            if (!_variables.ContainsKey(name))
            {
                return Parent?.GetVariable(name) ?? null;
            }
            
            return _variables[name] ?? Parent?.GetVariable(name) ?? null;
        }

        public void SetVariable(string name, object? value)
        {
            if (Parent != null && Parent.Exists(name))
            {
                Parent.SetVariable(name, value);
            }
            
            if (_variables.ContainsKey(name))
            {
                _variables[name] = value;
            }
        }
        
        public bool Exists(string name)
        {
            return GetVariable(name) != null;
        }
    }