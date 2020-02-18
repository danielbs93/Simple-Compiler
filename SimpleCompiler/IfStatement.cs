using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class IfStatement : StatetmentBase
    {
        public Expression Term { get; private set; }
        public List<StatetmentBase> DoIfTrue { get; private set; }
        public List<StatetmentBase> DoIfFalse { get; private set; }

        public override void Parse(TokensStack sTokens)
        {
            DoIfTrue = new List<StatetmentBase>();
            DoIfFalse = new List<StatetmentBase>();
            Token tIf = sTokens.Pop();
            if (!(tIf.ToString().Equals("if")))
                throw new SyntaxErrorException("Expected if statment, recived " + tIf, tIf);
            Token termS_Parentheses = sTokens.Pop();
            if (!(termS_Parentheses is Parentheses) && !(termS_Parentheses).ToString().Equals("("))
                throw new SyntaxErrorException("Expected Parentheses '(', recived " + termS_Parentheses, termS_Parentheses);
            Term = Expression.Create(sTokens);
            Term.Parse(sTokens);
            Token termE_Parentheses = sTokens.Pop();
            if (!(termE_Parentheses is Parentheses) && !(termE_Parentheses).ToString().Equals(")"))
                throw new SyntaxErrorException("Expected Parentheses ')', recived " + termE_Parentheses, termE_Parentheses);
            Token tS_Parentheses = sTokens.Pop();
            if (!(tS_Parentheses is Parentheses) && !(tS_Parentheses).ToString().Equals("{"))
               throw new SyntaxErrorException("Expected Parentheses '{', recived " + tS_Parentheses, tS_Parentheses);
            while (!(sTokens.Peek(0) is Parentheses && ((Parentheses)sTokens.Peek(0)).Name == '}'))
            {
                Token tStatment = sTokens.Peek();
                StatetmentBase statetment = StatetmentBase.Create(tStatment);
                statetment.Parse(sTokens);
                DoIfTrue.Add(statetment);
            }
            Token tE_Parentheses = sTokens.Pop();
            if (!(tE_Parentheses is Parentheses) && !(tE_Parentheses).ToString().Equals("}"))
                throw new SyntaxErrorException("Expected Parentheses '}', recived " + tE_Parentheses, tE_Parentheses);
            if (sTokens.Peek(0).ToString().Equals("else"))        // else statment
            {
                Token tElse = sTokens.Pop();
                Token elseS_Parentheses = sTokens.Pop();
                if (!(elseS_Parentheses is Parentheses) && !(elseS_Parentheses).ToString().Equals("{"))
                    throw new SyntaxErrorException("Expected Parentheses '{', recived " + elseS_Parentheses, elseS_Parentheses);
                while (!(sTokens.Peek(0) is Parentheses && (sTokens.Peek(0)).ToString().Equals("}")))
                {
                    Token tStatment = sTokens.Peek();
                    StatetmentBase statetment = StatetmentBase.Create(tStatment);
                    statetment.Parse(sTokens);
                    DoIfFalse.Add(statetment);
                }
                Token elseE_Parentheses = sTokens.Pop();
                if (!(elseE_Parentheses is Parentheses) && !(elseE_Parentheses).ToString().Equals("}"))
                    throw new SyntaxErrorException("Expected Parentheses '}', recived " + elseE_Parentheses, elseE_Parentheses);
            }
        }

        public override string ToString()
        {
            string sIf = "if(" + Term + "){\n";
            foreach (StatetmentBase s in DoIfTrue)
                sIf += "\t\t\t" + s + "\n";
            sIf += "\t\t}";
            if (DoIfFalse.Count > 0)
            {
                sIf += "else{";
                foreach (StatetmentBase s in DoIfFalse)
                    sIf += "\t\t\t" + s + "\n";
                sIf += "\t\t}";
            }
            return sIf;
        }

    }
}
