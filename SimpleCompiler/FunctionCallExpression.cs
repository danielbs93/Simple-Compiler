using System;
using System.Collections.Generic;

namespace SimpleCompiler
{
    public class FunctionCallExpression : Expression
    {
        public string FunctionName { get; private set; }
        public List<Expression> Args { get; private set; }

        public override void Parse(TokensStack sTokens)
        {
            Args = new List<Expression>();
            Token tFuncName = sTokens.Pop();
            if (!(tFuncName is Identifier))
                throw new SyntaxErrorException("Expected Identifier var, recived " + tFuncName, tFuncName);
            FunctionName = tFuncName.ToString();
            Token tParentheses = sTokens.Pop();
            if (!(tParentheses is Parentheses) && (tParentheses).ToString().Equals("("))
                throw new SyntaxErrorException("Expected Parentheses '(', recived " + tParentheses, tParentheses);
            while (sTokens.Count > 0 && !(sTokens.Peek() is Parentheses && (sTokens.Peek(0)).ToString().Equals(")")))
            {
                if (sTokens.Peek() is Separator)
                {
                    Token tComa = sTokens.Pop();
                    if (!(tComa.ToString().Equals(",")))
                        throw new SyntaxErrorException("Expected Seperator ',', recived " + tComa, tComa);
                }
                else
                {
                    Expression eArg = Expression.Create(sTokens);
                    eArg.Parse(sTokens);
                    Args.Add(eArg);
                }
                //else
                //    throw new SyntaxErrorException("Expectes Expressions Args or Comas, recived " + sTokens.Peek(0), sTokens.Peek(0));
            }
            if (sTokens.Count < 1)
                throw new Exception("Code Terminated");
            Token eParentheses = sTokens.Pop();
            if (!(eParentheses is Parentheses) && (eParentheses).ToString().Equals(")"))
                throw new SyntaxErrorException("Expected Parentheses ')', recived " + eParentheses, eParentheses);
        }

        public override string ToString()
        {
            string sFunction = FunctionName + "(";
            for (int i = 0; i < Args.Count - 1; i++)
                sFunction += Args[i] + ",";
            if (Args.Count > 0)
                sFunction += Args[Args.Count - 1];
            sFunction += ")";
            return sFunction;
        }
    }
}