namespace OrbitalCore.Lex;

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
    Comma,
    
    // Comparison Operators
    Align, // ==
    Disrupt, // !=
    Above, // >
    Below, // <
    Safe, // >=
    Unsafe, // <=
    
    // Mathematical Operators
    Gain, // +
    Drain, // -
    Amplify, // *
    Disperse, // /
    
    // Logical Operators
    Stable, // &&
    Path, // ||
}