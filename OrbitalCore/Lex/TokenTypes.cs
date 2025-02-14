namespace OrbitalCore.Tokens;

public enum TokenTypes
{ 
    Identifier,
    String, 
    Number,
    Boolean,
    
    // Misc
    SingleLineComment,
    MultiLineComment,
    Assignment,
    EoF,
    LeftParenthesis,
    RightParenthesis,
    LeftBrace,
    RightBrace,
    SemiColon,
    
    // Mathematical Operators
    Gain, // +
    Drain, // -
    Amplify, // *
    Disperse, // /
    
    // Logical Operators
    Stable, // &&
    Path, // ||
    Negate, // !
    
    // Comparison Operators
    Align, // ==
    Disrupt, // !=
    Above, // >
    Below, // <
    Safe, // >=
    Unsafe, // <=
}