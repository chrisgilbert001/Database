// Generated from C:/Users/chris gilbert/source/repos/Database/Database/SQLGrammar\SQLGrammar.g4 by ANTLR 4.7
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class SQLGrammarLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, SELECT=4, FROM=5, JOIN=6, ON=7, ID=8, EQ=9, LETTER=10, 
		WS=11;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	public static final String[] ruleNames = {
		"T__0", "T__1", "T__2", "SELECT", "FROM", "JOIN", "ON", "ID", "EQ", "LETTER", 
		"DIGIT", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", 
		"N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "WS"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "','", "'.'", "'*'", null, null, null, null, null, "'='", null, 
		"' '"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, null, null, null, "SELECT", "FROM", "JOIN", "ON", "ID", "EQ", "LETTER", 
		"WS"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public SQLGrammarLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "SQLGrammar.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\r\u00cc\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\3\2\3\2\3\3\3\3\3\4\3\4\3"+
		"\5\3\5\3\5\3\5\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\b"+
		"\3\b\3\b\3\t\3\t\5\tl\n\t\3\t\3\t\7\tp\n\t\f\t\16\ts\13\t\3\n\3\n\3\13"+
		"\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13"+
		"\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\3\13\5\13\u0091\n\13"+
		"\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\21\3\21\3\22\3\22\3\23"+
		"\3\23\3\24\3\24\3\25\3\25\3\26\3\26\3\27\3\27\3\30\3\30\3\31\3\31\3\32"+
		"\3\32\3\33\3\33\3\34\3\34\3\35\3\35\3\36\3\36\3\37\3\37\3 \3 \3!\3!\3"+
		"\"\3\"\3#\3#\3$\3$\3%\3%\3&\3&\3\'\3\'\3\'\3\'\2\2(\3\3\5\4\7\5\t\6\13"+
		"\7\r\b\17\t\21\n\23\13\25\f\27\2\31\2\33\2\35\2\37\2!\2#\2%\2\'\2)\2+"+
		"\2-\2/\2\61\2\63\2\65\2\67\29\2;\2=\2?\2A\2C\2E\2G\2I\2K\2M\r\3\2\35\3"+
		"\2\62;\4\2CCcc\4\2DDdd\4\2EEee\4\2FFff\4\2GGgg\4\2HHhh\4\2IIii\4\2JJj"+
		"j\4\2KKkk\4\2LLll\4\2MMmm\4\2NNnn\4\2OOoo\4\2PPpp\4\2QQqq\4\2RRrr\4\2"+
		"SSss\4\2TTtt\4\2UUuu\4\2VVvv\4\2WWww\4\2XXxx\4\2YYyy\4\2ZZzz\4\2[[{{\4"+
		"\2\\\\||\2\u00cc\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13"+
		"\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2"+
		"\2\2M\3\2\2\2\3O\3\2\2\2\5Q\3\2\2\2\7S\3\2\2\2\tU\3\2\2\2\13\\\3\2\2\2"+
		"\ra\3\2\2\2\17f\3\2\2\2\21k\3\2\2\2\23t\3\2\2\2\25\u0090\3\2\2\2\27\u0092"+
		"\3\2\2\2\31\u0094\3\2\2\2\33\u0096\3\2\2\2\35\u0098\3\2\2\2\37\u009a\3"+
		"\2\2\2!\u009c\3\2\2\2#\u009e\3\2\2\2%\u00a0\3\2\2\2\'\u00a2\3\2\2\2)\u00a4"+
		"\3\2\2\2+\u00a6\3\2\2\2-\u00a8\3\2\2\2/\u00aa\3\2\2\2\61\u00ac\3\2\2\2"+
		"\63\u00ae\3\2\2\2\65\u00b0\3\2\2\2\67\u00b2\3\2\2\29\u00b4\3\2\2\2;\u00b6"+
		"\3\2\2\2=\u00b8\3\2\2\2?\u00ba\3\2\2\2A\u00bc\3\2\2\2C\u00be\3\2\2\2E"+
		"\u00c0\3\2\2\2G\u00c2\3\2\2\2I\u00c4\3\2\2\2K\u00c6\3\2\2\2M\u00c8\3\2"+
		"\2\2OP\7.\2\2P\4\3\2\2\2QR\7\60\2\2R\6\3\2\2\2ST\7,\2\2T\b\3\2\2\2UV\5"+
		"=\37\2VW\5!\21\2WX\5/\30\2XY\5!\21\2YZ\5\35\17\2Z[\5? \2[\n\3\2\2\2\\"+
		"]\5#\22\2]^\5;\36\2^_\5\65\33\2_`\5\61\31\2`\f\3\2\2\2ab\5+\26\2bc\5\65"+
		"\33\2cd\5)\25\2de\5\63\32\2e\16\3\2\2\2fg\5\65\33\2gh\5\63\32\2h\20\3"+
		"\2\2\2il\5\25\13\2jl\5\27\f\2ki\3\2\2\2kj\3\2\2\2lq\3\2\2\2mp\5\25\13"+
		"\2np\5\27\f\2om\3\2\2\2on\3\2\2\2ps\3\2\2\2qo\3\2\2\2qr\3\2\2\2r\22\3"+
		"\2\2\2sq\3\2\2\2tu\7?\2\2u\24\3\2\2\2v\u0091\5\31\r\2w\u0091\5\33\16\2"+
		"x\u0091\5\35\17\2y\u0091\5\37\20\2z\u0091\5!\21\2{\u0091\5#\22\2|\u0091"+
		"\5%\23\2}\u0091\5\'\24\2~\u0091\5)\25\2\177\u0091\5+\26\2\u0080\u0091"+
		"\5-\27\2\u0081\u0091\5/\30\2\u0082\u0091\5\61\31\2\u0083\u0091\5\63\32"+
		"\2\u0084\u0091\5\65\33\2\u0085\u0091\5\67\34\2\u0086\u0091\59\35\2\u0087"+
		"\u0091\5;\36\2\u0088\u0091\5=\37\2\u0089\u0091\5? \2\u008a\u0091\5A!\2"+
		"\u008b\u0091\5C\"\2\u008c\u0091\5E#\2\u008d\u0091\5G$\2\u008e\u0091\5"+
		"I%\2\u008f\u0091\5K&\2\u0090v\3\2\2\2\u0090w\3\2\2\2\u0090x\3\2\2\2\u0090"+
		"y\3\2\2\2\u0090z\3\2\2\2\u0090{\3\2\2\2\u0090|\3\2\2\2\u0090}\3\2\2\2"+
		"\u0090~\3\2\2\2\u0090\177\3\2\2\2\u0090\u0080\3\2\2\2\u0090\u0081\3\2"+
		"\2\2\u0090\u0082\3\2\2\2\u0090\u0083\3\2\2\2\u0090\u0084\3\2\2\2\u0090"+
		"\u0085\3\2\2\2\u0090\u0086\3\2\2\2\u0090\u0087\3\2\2\2\u0090\u0088\3\2"+
		"\2\2\u0090\u0089\3\2\2\2\u0090\u008a\3\2\2\2\u0090\u008b\3\2\2\2\u0090"+
		"\u008c\3\2\2\2\u0090\u008d\3\2\2\2\u0090\u008e\3\2\2\2\u0090\u008f\3\2"+
		"\2\2\u0091\26\3\2\2\2\u0092\u0093\t\2\2\2\u0093\30\3\2\2\2\u0094\u0095"+
		"\t\3\2\2\u0095\32\3\2\2\2\u0096\u0097\t\4\2\2\u0097\34\3\2\2\2\u0098\u0099"+
		"\t\5\2\2\u0099\36\3\2\2\2\u009a\u009b\t\6\2\2\u009b \3\2\2\2\u009c\u009d"+
		"\t\7\2\2\u009d\"\3\2\2\2\u009e\u009f\t\b\2\2\u009f$\3\2\2\2\u00a0\u00a1"+
		"\t\t\2\2\u00a1&\3\2\2\2\u00a2\u00a3\t\n\2\2\u00a3(\3\2\2\2\u00a4\u00a5"+
		"\t\13\2\2\u00a5*\3\2\2\2\u00a6\u00a7\t\f\2\2\u00a7,\3\2\2\2\u00a8\u00a9"+
		"\t\r\2\2\u00a9.\3\2\2\2\u00aa\u00ab\t\16\2\2\u00ab\60\3\2\2\2\u00ac\u00ad"+
		"\t\17\2\2\u00ad\62\3\2\2\2\u00ae\u00af\t\20\2\2\u00af\64\3\2\2\2\u00b0"+
		"\u00b1\t\21\2\2\u00b1\66\3\2\2\2\u00b2\u00b3\t\22\2\2\u00b38\3\2\2\2\u00b4"+
		"\u00b5\t\23\2\2\u00b5:\3\2\2\2\u00b6\u00b7\t\24\2\2\u00b7<\3\2\2\2\u00b8"+
		"\u00b9\t\25\2\2\u00b9>\3\2\2\2\u00ba\u00bb\t\26\2\2\u00bb@\3\2\2\2\u00bc"+
		"\u00bd\t\27\2\2\u00bdB\3\2\2\2\u00be\u00bf\t\30\2\2\u00bfD\3\2\2\2\u00c0"+
		"\u00c1\t\31\2\2\u00c1F\3\2\2\2\u00c2\u00c3\t\32\2\2\u00c3H\3\2\2\2\u00c4"+
		"\u00c5\t\33\2\2\u00c5J\3\2\2\2\u00c6\u00c7\t\34\2\2\u00c7L\3\2\2\2\u00c8"+
		"\u00c9\7\"\2\2\u00c9\u00ca\3\2\2\2\u00ca\u00cb\b\'\2\2\u00cbN\3\2\2\2"+
		"\7\2koq\u0090\3\2\3\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}