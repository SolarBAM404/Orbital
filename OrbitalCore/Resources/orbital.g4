grammar orbital;   

unaryOperator
    : 'amplify'
    | 'gain'
    | 'drain'
    | 'disperse'
    | 'reverse'
    ;
    
castExpression
    : '__extension__'? '(' typeSpecifier ')' castExpression 
    ;
    
assignmentOperator
    : '='
    ;
 
typeSpecifier
    : 'void'
    | 'str'
    | 'int'
    | 'float'
    ;

Signal 
    : 'signal'
    ;

Void
    : 'void'
    ;

Orbit
    : 'orbit'
    ;
    
Probe
    : 'probe'
    ;
    
Scan
    : 'scan'
    ;

Nova
    : 'nova'
    ;
    
Land 
    : 'land'
    ;
    
Align
    : 'align'
    ;
 
Disrupt
    : 'disrupt'
    ;

Above
    : 'above'
    ;

Below
    : 'below' 
    ; 
    
Safe
    : 'safe'
    ;

Unsafe
    : 'unsafe'
    ;

Gain
    : 'gain'
    ;

Drain
    : 'drain'
    ;

Amplify
    : 'amplify'
    ;

Disperse
    : 'disperse'
    ;

Stable
    : 'stable'
    ;

Path
    : 'path'
    ;

    
Identifier
    : IdentifierNonDigit (IdentifierNonDigit | Digit)*
    ;
    
fragment IdentifierNonDigit
    : Nondigit
    ;
    
fragment Nondigit
    : [a-zA-Z_]
    ;

fragment Digit
    : [0-9]
    ;
    

Newline 
    : ('\r' '\n'? | '\n') -> channel(HIDDEN)
    ;
    
Whitespace
    : [ \t]+ -> channel(HIDDEN)
    ;

Keywords
    : Amplify
    |