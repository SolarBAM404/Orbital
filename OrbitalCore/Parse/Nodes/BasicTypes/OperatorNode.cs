using OrbitalCore.Lex;
using OrbitalCore.Parse.Nodes.Abstract;

namespace OrbitalCore.Parse.Nodes.BasicTypes;

public class OperatorNode(TokenTypes value) : AbstractValueNode(value)
{  
    public new TokenTypes Value { get; set; } = value;
    
}