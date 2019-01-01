// Generated from C:/Users/chris gilbert/source/repos/Database/Database/SQLGrammar\SQLGrammar.g4 by ANTLR 4.7
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link SQLGrammarParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface SQLGrammarVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#dmlstatements}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitDmlstatements(SQLGrammarParser.DmlstatementsContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#select_statement}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitSelect_statement(SQLGrammarParser.Select_statementContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#column_list}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitColumn_list(SQLGrammarParser.Column_listContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#column_element}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitColumn_element(SQLGrammarParser.Column_elementContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#column_name}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitColumn_name(SQLGrammarParser.Column_nameContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#asterisk}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAsterisk(SQLGrammarParser.AsteriskContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#table_name}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitTable_name(SQLGrammarParser.Table_nameContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#compileUnit}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCompileUnit(SQLGrammarParser.CompileUnitContext ctx);
	/**
	 * Visit a parse tree produced by {@link SQLGrammarParser#id}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitId(SQLGrammarParser.IdContext ctx);
}