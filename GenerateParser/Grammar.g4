grammar Grammar;

@header {
	using GenerateParser.Rules;
}

file
    : GRAMMAR LexerName END (header)? line+ EOF
    ;
    
header
    : HEADER CODE
    ;
    
line returns[Rule ret]
    : grammarRule END {$ret=$grammarRule.ret;}
    | grammarRuleArg END {$ret=$grammarRuleArg.ret;}
    | lexerRule END {$ret=$lexerRule.ret;}
    ;
    
grammarRule returns [GrammarRule ret]
    : grammarName IS productions {$ret=new GrammarRule($grammarName.text, $productions.ret);}
    ;
    
grammarRuleArg returns [Rule ret]
    : grammarName RETURN RETURNBLOCK IS productionsArg {$ret=new RuleWithCode($grammarName.text,$RETURNBLOCK.text,$productionsArg.ret);}
    ;
    
productions returns[Productions ret]
    : production {$ret=new Productions($production.ret);}
    | production OR productions {$ret=new Productions($production.ret, $productions.ret);}
    ;
    
productionsArg returns [ProductionsWithCode ret]
    : productionArg {$ret=new ProductionsWithCode($productionArg.ret);}
    | productionArg OR productionsArg {$ret=new ProductionsWithCode($productionArg.ret,$productionsArg.ret);}
    ;
    
production returns [Production ret]
    : grammarName {$ret=new Production($grammarName.text);}
	| (var=grammarName ASSIGN) grammarName {$ret=new Production($grammarName.text, $var.text);}
    | lexerToken {$ret=new Production($lexerToken.text);}
	| (var=grammarName ASSIGN) lexerToken {$ret=new Production($lexerToken.text, $var.text);}
    | grammarName production {$ret = new Production($grammarName.text,$production.ret);}
	| (var=grammarName ASSIGN) grammarName production {$ret = new Production($grammarName.text, $var.text,$production.ret);}
    | lexerToken production {$ret=new Production($lexerToken.text,$production.ret);}
	| (var=grammarName ASSIGN) lexerToken production {$ret=new Production($lexerToken.text, $var.text,$production.ret);}
    | {$ret=new Production();}
    ;
    
productionArg returns [ProductionWithCode ret]
    : grammarName CODE {$ret = new ProductionWithCode($grammarName.text, $grammarName.text, $CODE.text);}
	| (var=grammarName ASSIGN) grammarName CODE {$ret = new ProductionWithCode($grammarName.text, $var.text, $CODE.text);}
    | lexerToken CODE {$ret = new ProductionWithCode($lexerToken.text, $lexerToken.text, $CODE.text);}
	| (var=grammarName ASSIGN) lexerToken CODE {$ret = new ProductionWithCode($lexerToken.text, $var.text, $CODE.text);}
    | grammarName p=productionArgTail CODE {$ret = new ProductionWithCode($grammarName.text, $grammarName.text, $p.ret, $CODE.text);}
	| (var=grammarName ASSIGN) grammarName p=productionArgTail CODE {$ret = new ProductionWithCode($grammarName.text, $var.text, $p.ret, $CODE.text);}
    | lexerToken p=productionArgTail CODE {$ret = new ProductionWithCode($lexerToken.text, $lexerToken.text, $p.ret, $CODE.text);}
	| (var=grammarName ASSIGN) lexerToken p=productionArgTail CODE {$ret = new ProductionWithCode($lexerToken.text, $var.text, $p.ret, $CODE.text);}
    | CODE {$ret=new ProductionWithCode($CODE.text);}
    ;

productionArgTail returns [ProductionWithCode ret]
	: grammarName {$ret=new ProductionWithCode($grammarName.text,$grammarName.text);}
	| (var=grammarName ASSIGN)? grammarName {$ret=new ProductionWithCode($grammarName.text,$var.text);}
	| lexerToken {$ret=new ProductionWithCode($lexerToken.text,$lexerToken.text); }
	| (var=grammarName ASSIGN) lexerToken {$ret=new ProductionWithCode($lexerToken.text,$var.text); }
	| grammarName p=productionArgTail {$ret=new ProductionWithCode($grammarName.text,$grammarName.text,$p.ret);}
	| (var=grammarName ASSIGN) grammarName p=productionArgTail {$ret=new ProductionWithCode($grammarName.text,$var.text,$p.ret);}
	| lexerToken p=productionArgTail  {$ret=new ProductionWithCode($lexerToken.text,$lexerToken.text,$p.ret);}
	| (var=grammarName ASSIGN) lexerToken p=productionArgTail  {$ret=new ProductionWithCode($lexerToken.text,$var.text,$p.ret);}
	;
    
grammarName returns [string text]
    : GrammarName {$text=$GrammarName.text;}
    ;
    
lexerToken returns [string text]
    : lexerRuleName {$text=$lexerRuleName.text;}
    /*| STRING {$text=$STRING.text;}*/
	;
    
lexerRuleName 
    : LexerName
    ;
    
lexerRule returns[LexerRule ret]
    : LexerName IS STRING  {$ret=new LexerRule($LexerName.text, $STRING.text,false);}
    | LexerName IS STRING '->' SKIP_ACTION {$ret=new LexerRule($LexerName.text,$STRING.text,true);}
    ;


WS 
    : [ \t\n\r]+ -> skip
    ;
    
GRAMMAR
    : 'grammar'
    ;
    
SKIP_ACTION
    : 'skip'
    ;
    
RETURN
    : 'returns'
    ;
    
HEADER
    : '@header'
    ;

GrammarName 
    : [a-z][a-zA-Z0-9_]*
    ;
    
LexerName
    : [A-Z][a-zA-Z0-9_]*
    ;
    
END
    : ';' 
    ;
    
IS
    : ':' 
    ;
    
OR
    : '|'
    ;
    
OPEN
    : '['
    ;
    
CLOSE
    : ']'
    ;
    
ASSIGN
    : '='
    ;
    
RETURNBLOCK
    : OPEN .*? ' ' GrammarName CLOSE
    ;
    
STRING
    : ["].*?["]
    | ['].*?[']
    ;
    
CODE
    : [{].*?[}]
    ;
