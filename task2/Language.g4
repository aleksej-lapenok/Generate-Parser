grammar Language;

@header
{
	using static task2.Utils.Translator;
    using task2.Utils;
}

program returns [string res]
    : function program {$res=$function.res+$program.res;}
    | var program {$res=$var.res+$program.res;}
    | {$res=EmptyStr;}
    ;

function returns [string res]
	: LET NAME LB args RB COLON type OPEN functionBody CLOSE {$res=Public+$type.res+Space + $NAME.Text+Translator.LB + $args.res+Translator.RB+NewLine+Open+NewLine+$functionBody.res+Close+NewLine;}
	;

functionBody returns [string res]
	: newVar {$res= $newVar.res+NewLine;}
    | iff {$res=$iff.res+NewLine;}
    | expr {$res=Return+$expr.res+End+NewLine;}
    ;
    
newVar returns [string res]
	: var functionBody {$res =$var.res+NewLine+$functionBody.res; }
	;

var returns [string res]
	: LETS NAME COLON type SET expr {$res=$type.res+Space+$NAME.Text+Is+$expr.res;}
	;

iff returns [string res]
	: IF LB expr RB OPEN functionBody CLOSE if2 {$res=If+$bool_exp.res+Translator.RB+NewLine+Open+NewLine+$functionBody.res+Close+NewLine+ $if2.res;}
	;

if2 returns [string res]
   : elsee {$res = $elsee.res;}
   | elif2 {$res=$elif2.res;}
   ;
    
elsee returns [string res]
	: ELSE OPEN functionBody CLOSE {$res=Else+NewLine+Open+$functionBody.res+Close+NewLine;}
	;
    
elif2 returns [string res]
	: ELIF LB expr RB OPEN functionBody CLOSE if2 {$res = Else + If+$bool_exp.res+Translator.RB+NewLine+Open+NewLine + $functionBody.res+Close+NewLine+$if2.res;}
    ;
    
expr returns [string res]
	:str2 {$res=$str.Text;}
    ;
    
arguments returns [string res]
    : expr arguments2 {$res=$expr.res+$arguments2.res;}
	;
    
arguments2 returns [string res]
    : COMMA expr arguments2 {$res=Comma+$expr.res+$arguments2.res;}
    | {$res=EmptyStr;}
    ;
    
args returns [string res]
    : arg argg2 {$res=$arg.res+$argg2.res;}
    | {$res=EmptyStr;}
    ;
    
argg2 returns [string res]
    : COMMA arg argg2 {$res=Comma+$arg.res+$argg2.res;}
    | {$res=EmptyStr;}
    ;
    
arg returns [string res]
	: NAME COLON type {$res=$type.res+Space+$NAME.Text;}
	;

type returns [string res]
    : INT {$res=Int;}
    | BOOL {$res=Bool;}
    ;

str2
	: NUM str
	| NAME str
	| EQ str
	| LES str
	| LESE str
	| M str
	| MOREE str
	| AND str
	| NOT str
	| OR str
	| PLUS str
	| MINUS str
	| MUL str
	| DIV str
	| MOD str
	;
str
	: str2
	| 
	;


NUM: "[0-9]";
INT: 'Int';
BOOL: 'Bool';
LET: 'Let';
LETS: 'let';
IF: 'If';
ELSE: 'Else';
TRUE: 'true';
FALSE: 'false';
WS: "[ \t\r\n]+" -> skip;
LB: '\(';
RB: '\)';
COLON: ':';
EQ: '==';
SET: '=';
LES: '<';
LESE: '<=';
M: '>';
MOREE: '>=';
AND: '&&';
NOT: '!';
OR: '\|\|';
PLUS: '\+';
MINUS: '-';
MUL: '\*';
DIV: '/';
MOD: '%';
OPEN: '{';
CLOSE: '}';
COMMA: ',';
ELIF: 'ElIf';
NAME: '[a-z][a-zA-Z_0-9]*' ;