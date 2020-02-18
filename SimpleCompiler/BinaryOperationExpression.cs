using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class BinaryOperationExpression : Expression
    {
        public string Operator { get;  set; }
        public Expression Operand1 { get;  set; }
        public Expression Operand2 { get;  set; }
        private int NumOfParent = 0; // num of parentheses to identify how much binaryOpEXP exists in it

        public override string ToString()
        {
            return "(" + Operator + " " + Operand1 + " " + Operand2 + ")";
        }

        public override void Parse(TokensStack sTokens)// ( operator opnd1 opnd2 )
        {
            Token tParentheses = sTokens.Pop();         // (
            if (!(tParentheses is Parentheses))
                throw new SyntaxErrorException("Expected Parentheses '(', recived " + tParentheses, tParentheses);
            NumOfParent++;
            Token tOperator = sTokens.Pop();            // operator
            if (!(tOperator is Operator))
                throw new SyntaxErrorException("Expected Binary Operator, recived " + tOperator, tOperator);
            Operator = tOperator.ToString();
            TokensStack temp = new TokensStack();
            Operand1 = Expression.Create(sTokens);
            Operand1.Parse(sTokens);
            Operand2 = Expression.Create(sTokens);
            Operand2.Parse(sTokens);
            Token tE_Parentheses = sTokens.Pop();
            if (sTokens.Count < 1)
                throw new Exception("Code Terminated");
            if (!(tE_Parentheses is Parentheses) && (tE_Parentheses).ToString().Equals(")"))
                throw new SyntaxErrorException("Expected Parentheses ')', recived " + tE_Parentheses, tE_Parentheses);
            /*while (NumOfParent != 0)
            {
                if (sTokens.Peek(0) is Parentheses && (((Parentheses)sTokens.Peek(0)).Name  == '('))
                    NumOfParent++;
                if (sTokens.Peek(0) is Parentheses && (((Parentheses)sTokens.Peek(0)).Name  == ')'))
                    NumOfParent--;
                temp.Push(sTokens.Pop());
            }
            if (sTokens.Peek(0) is Separator && ((Separator)sTokens.Peek(0)).Name == ';') // if ';' exist OR it's BinaryEXPRESSION in functionCall ~ foo(BinEXP) ~ foo((*xy))
            {    
                Token tEnd = temp.Pop();
            }
            Token tParenthesesEnd = temp.Pop();        // ')' last one
            if (!(tParenthesesEnd is Parentheses && (((Parentheses)tParenthesesEnd).Name  == ')')))
                throw new SyntaxErrorException("Expected Parentheses ')', recived " + tParenthesesEnd, tParenthesesEnd);
            if (temp.Peek(0) is Parentheses && (((Parentheses)temp.Peek(0)).Name  == ')')) // if opnd2 is Expression - ')' because we ineserted backward from sTokens to temp stack
            {
                TokensStack Operand2Stack = new TokensStack();
                Operand2Stack.Push(temp.Pop()); // opernad2 stack <-- ')'
                while (!(temp.Peek(0) is Parentheses))
                    Operand2Stack.Push(temp.Pop()); // operand2 stack <-- 'operator opnd1 opnd2'
                Operand2Stack.Push(temp.Pop()); // operand2 stack <-- '('
                Operand2 = Expression.Create(Operand2Stack);
                Operand2.Parse(Operand2Stack);
            }
            else if (!(temp.Peek(0) is Parentheses))// if opnd2 is number or variable
            {
                if (temp.Peek(0) is Identifier)
                {
                    Operand2 = new VariableExpression();
                    Operand2.Parse(temp);
                }
                else if (temp.Peek(0) is Number)
                {
                    Operand2 = new NumericExpression();
                    Operand2.Parse(temp);
                }
            }
            if (!(temp.Peek(0) is Parentheses))// if opnd1 is Number or variable
            {
                if (temp.Peek(0) is Identifier)
                {
                    Operand1 = new VariableExpression();
                    Operand1.Parse(temp);
                }
                else if (temp.Peek(0) is Number)
                {
                    Operand1 = new NumericExpression();
                    Operand1.Parse(temp);
                }
            }
            else                          // if opnd1 is EXPRESSION
            {
                TokensStack Operand1Stack = new TokensStack();
                while (!(temp.Count < 0))   // reverse EXPRESSION from temp stack to Operand1Stack to hold the legal expression for opnd1
                    Operand1Stack.Push(temp.Pop());
                Operand1 = Expression.Create(Operand1Stack);
                Operand1.Parse(Operand1Stack);
            }*/
            
            
            
        }
    }
}
