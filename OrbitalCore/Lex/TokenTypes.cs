namespace OrbitalCore.Lex;

public enum TokenTypes
{ 
    Identifier,
    String, 
    Number,
    Boolean,
    
    Signal, // true
    Void, // false
    
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
    Probe, // if
    ProbeScan, // else if
    Scan, // else
    Orbit, // while
    Nova, //  define
    Land, // return
    Null,
    
    Uplink, // print
    Negate, // !
    
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