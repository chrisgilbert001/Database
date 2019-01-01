// Generated from C:/Users/chris gilbert/source/repos/Database/Database/SQLGrammar\SQLGrammar.g4 by ANTLR 4.7
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link SQLGrammarParser}.
 */
public interface SQLGrammarListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#dmlstatements}.
	 * @param ctx the parse tree
	 */
	void enterDmlstatements(SQLGrammarParser.DmlstatementsContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#dmlstatements}.
	 * @param ctx the parse tree
	 */
	void exitDmlstatements(SQLGrammarParser.DmlstatementsContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#select_statement}.
	 * @param ctx the parse tree
	 */
	void enterSelect_statement(SQLGrammarParser.Select_statementContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#select_statement}.
	 * @param ctx the parse tree
	 */
	void exitSelect_statement(SQLGrammarParser.Select_statementContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#column_list}.
	 * @param ctx the parse tree
	 */
	void enterColumn_list(SQLGrammarParser.Column_listContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#column_list}.
	 * @param ctx the parse tree
	 */
	void exitColumn_list(SQLGrammarParser.Column_listContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#column_element}.
	 * @param ctx the parse tree
	 */
	void enterColumn_element(SQLGrammarParser.Column_elementContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#column_element}.
	 * @param ctx the parse tree
	 */
	void exitColumn_element(SQLGrammarParser.Column_elementContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#column_name}.
	 * @param ctx the parse tree
	 */
	void enterColumn_name(SQLGrammarParser.Column_nameContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#column_name}.
	 * @param ctx the parse tree
	 */
	void exitColumn_name(SQLGrammarParser.Column_nameContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#asterisk}.
	 * @param ctx the parse tree
	 */
	void enterAsterisk(SQLGrammarParser.AsteriskContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#asterisk}.
	 * @param ctx the parse tree
	 */
	void exitAsterisk(SQLGrammarParser.AsteriskContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#table_name}.
	 * @param ctx the parse tree
	 */
	void enterTable_name(SQLGrammarParser.Table_nameContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#table_name}.
	 * @param ctx the parse tree
	 */
	void exitTable_name(SQLGrammarParser.Table_nameContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#compileUnit}.
	 * @param ctx the parse tree
	 */
	void enterCompileUnit(SQLGrammarParser.CompileUnitContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#compileUnit}.
	 * @param ctx the parse tree
	 */
	void exitCompileUnit(SQLGrammarParser.CompileUnitContext ctx);
	/**
	 * Enter a parse tree produced by {@link SQLGrammarParser#id}.
	 * @param ctx the parse tree
	 */
	void enterId(SQLGrammarParser.IdContext ctx);
	/**
	 * Exit a parse tree produced by {@link SQLGrammarParser#id}.
	 * @param ctx the parse tree
	 */
	void exitId(SQLGrammarParser.IdContext ctx);
}