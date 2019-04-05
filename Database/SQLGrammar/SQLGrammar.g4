grammar SQLGrammar;

// Parser Rules
ddlstatements : insert_statement | create_table_statement;
dmlstatements : select_statement;

//Parser rules for create statement
create_table_statement : CREATE TABLE tablename=id '(' column_name (',' column_name)*?  (',' unique_constraint)? ')';
unique_constraint : CONSTRAINT constraint_name=id UNIQUE '(' unique_column=id ')';
column_name	: (table_name '.')? column=id;
delete_statement : DELETE FROM table_name;
insert_statement : INSERT INTO  table_name VALUES values (',' values)*?;
select_statement : SELECT column_list FROM table_name (join)*? (where)?;   
where : WHERE column_name '=' '\'' equals_string=id '\'';
values :  '(' val(',' val)*? ')';
val : QUOTE value=id QUOTE;
join : INNER JOIN table_name ON column_name EQ column_name;
column_list	: column_element (',' column_element)*;
column_element	: asterisk | column_name;
asterisk	: ALL | table_name '.'ALL;
table_name	: table=id;
compileUnit	: dmlstatements | ddlstatements EOF;
id	: ID;

// Lexer Rules
INSERT : I N S E R T;
CREATE : C R E A T E;
TABLE  : T A B L E;
DELETE : D E L E T E;
INTO : I N T O; 
WHERE : W H E R E;
VALUES: V A L U E S;
SELECT : S E L E C T;
FROM : F R O M;
JOIN: J O I N;
INNER : I N N E R;
CONSTRAINT: C O N S T R A I N T;
UNIQUE : U N I Q U E;
ON : O N;
ID : (LETTER | DIGIT) (LETTER | DIGIT)*;
EQ : '=';
ALL : '*';
LETTER : A | B | C | D | E | F | G | H | I | J | K | L | M | N | O | P | Q | R | S | T | U | V | W | X | Y | Z;
QUOTE : '\'';
// Numbers
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

WS : ' ' -> channel(HIDDEN)
;