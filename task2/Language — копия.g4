grammar Language;

program returns [string res]
    : function program {$res=$function.res+$program.res;}
    | var program {$res=$var.res+$program.res;}
    | {$res="";}
    ;

function returns [string res]
	: LET NAME LB args RB COLON type OPEN functionBody CLOSE {$res="public "+$type.res+" " + $NAME.text+" (" + $args.res+") \r\n {\r\n"+$functionBody.res+"} \r\n";}
	;

functionBody returns [string res]
	: newVar {$res= $newVar.res+"\r\n";}
    | @if {$res=$@if.res+"\r\n";}
    | expr {$res="return "+$expr.res+";\r\n";}
    | bool_exp {$res="return "+$bool_exp.res+";\r\n";}
    ;
    
newVar returns [string res]
	: var functionBody {$res =$var.res+"\r\n"+$functionBody.res; }
	;

var returns [string res]
	: LET NAME COLON type SET expr {$res=$type.res+" "+$NAME.text+" = "+$expr.res;}
	;

@if returns [string res]
	: IF LB bool_exp RB OPEN functionBody CLOSE if2 {$res="if ("+$bool_exp.res+") \r\n {\r\n"+$functionBody.res+"} \r\n" + $if2.res;}
	;

if2 returns [string res]
   : @else {$res = $@else.res;}
   | elif2 {$res=$elif2.res;}
   ;
    
@else returns [string res]
	: ELSE OPEN functionBody CLOSE {$res="else \r\n {\r\n"+$functionBody.res+"}\r\n";}
	;
    
elif2 returns [string res]
	:  ELIF LB bool_exp RB OPEN functionBody CLOSE elif2 {$res = "else if ("+$bool_exp.res+") \r\n {\r\n" + $functionBody.res+"}\r\n"+$elif2.res;}
    | else {$res=$else.res;}
    ;
    
expr returns [string res]
	:str {$res=$str.text;}
    ;
    
bool_exp returns [string res]
	: str {$res=$str.text;}
    ;
    
arguments returns [string res]
    : expr arguments2 {$res=$expr.res+$arguments2.res;}
	;
    
arguments2 returns [string res]
    : COMMA expr arguments2 {$res=", "+$expr.res+$arguments2.res;}
    | {$res="";}
    ;
    
args returns [string res]
    : arg arg2 {$res=$arg.res+$arg2.res;}
    | {$res="";}
    ;
    
arg2 returns [string res]
    : COMMA arg arg2 {$res=", "+$arg.res+$arg2.res;}
    | {$res="";}
    ;
    
arg returns [string res]
	: NAME COLON type {$res=$type.res+" "+$NAME.text;}
	;

type returns [string res]
    : INT {$res="int";}
    | BOOL {$res="bool";}
    ;

str
	: NAME str 
	| NUM str
	| EQ str
	| LES str
	| LESE str
	| M str
	| MOREE str
	| AND str
	| OR str
	| PLUS str
	| MINUS str
	| MUL str
	| DIV str
	| MOD str
	| COMMA str
	| LB str
	| RB str
	| TRUE str 
	| FALSE str
	|
	;


NUM: "[0-9]";
INT: 'Int';
BOOL: 'Bool';
LET: 'Let';
IF: 'If';
ELSE: 'Else';
TRUE: 'true';
FALSE: 'false';
WS: [ \t\r\n]+ -> skip;
LB: '(';
RB: ')';
COLON: ':';
EQ: '==';
SET: '=';
LES: '<';
LESE: '<=';
M: '>';
MOREE: '>=';
AND: '&&';
OR: '||';
PLUS: '+';
MINUS: '-';
MUL: '*';
DIV: '/';
MOD: '%';
OPEN: '{';
CLOSE: '}';
COMMA: ',';
ELIF: 'ElIf';
NAME: '[a-z][a-zA-Z_0-9]*' ;