using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class LetStatement : StatetmentBase
    {
        public string Variable { get; set; }
        public Expression Value { get; set; }

        public override string ToString()
        {
            return "let " + Variable + " = " + Value + ";";
        }

        public override void Parse(TokensStack sTokens)// let ID = EXP;
        {
            Token tLet = sTokens.Pop();          //let
            if (!(tLet.ToString().Equals("let")))
                throw new SyntaxErrorException("Expected let vartype, recived " + tLet, tLet);
            Token tName = sTokens.Pop();         //ID
            if(!(tName is Identifier))
                throw new SyntaxErrorException("Expected var name, received " + tName, tName);
            Variable = ((Identifier)tName).Name;
            Token tEqual = sTokens.Pop();
            if (!tEqual.ToString().Equals("="))        //=
                throw new SyntaxErrorException("Expected symbol '=', received " + tEqual, tEqual);
            Value = Expression.Create(sTokens);
            Value.Parse(sTokens);                //EXP
            Token tEnd = sTokens.Pop();          //;
            if (!(tEnd is Separator))
                throw new SyntaxErrorException("Expected seperator ';', recived " + tEnd, tEnd);

        }

    }
}
