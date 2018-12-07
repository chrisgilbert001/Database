grammar SQLGrammar;

/*
* Parser Rules
*/

dmlstatements	: select_statement;

select_statement	: SELECT column_list
                      FROM table_name
;

column_list	: column_element (',' column_element)*
;

column_element	: asterisk | column_name
;

column_name	: (table_name '.')? column=id
;

asterisk	: '*' | table_name '.' asterisk
;

table_name	: table=id
;

compileUnit	: dmlstatements EOF
;

id	: ID
;


/*
* Lexer Rules
*/

SELECT : S E L E C T;
FROM : F R O M;

ID : (LETTER | DIGIT) (LETTER | DIGIT)*;

LETTER : A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T | U | V | W | X | Y | Z;

//Numbers
fragment DIGIT : [0-9];

// Letters (CASE INSENSITIVE)
fragment A : [aA];
fragment B : [bB];
fragment C : [cC];
fragment D : [dD];
fragment E : [eE];
fragment F : [fF];
fragment G : [gG];
fragment H : [hH];
fragment I : [iI];
fragment J : [jJ];
fragment K : [kK];
fragment L : [lL];
fragment M : [mM];
fragment N : [nN];
fragment O : [oO];
fragment P : [pP];
fragment Q : [qQ];
fragment R : [rR];
fragment S : [sS];
fragment T : [tT];
fragment U : [uU];
fragment V : [vV];
fragment W : [wW];
fragment X : [xX];
fragment Y : [yY];
fragment Z : [zZ];

WS :	' ' -> channel(HIDDEN)
;