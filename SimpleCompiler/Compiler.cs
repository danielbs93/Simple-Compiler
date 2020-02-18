using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCompiler
{
    public class Compiler
    {

        private Dictionary<string, int> m_dSymbolTable;
        private int m_cLocals;

        public Compiler()
        {
            m_dSymbolTable = new Dictionary<string, int>();
            m_cLocals = 0;

        }

        public List<string> Compile(string sInputFile)
        {
            List<string> lCodeLines = ReadFile(sInputFile);
            List<Token> lTokens = Tokenize(lCodeLines);
            TokensStack sTokens = new TokensStack();
            for (int i = lTokens.Count - 1; i >= 0; i--)
                sTokens.Push(lTokens[i]);
            JackProgram program = Parse(sTokens);
            return null;
        }

        private JackProgram Parse(TokensStack sTokens)
        {
            JackProgram program = new JackProgram();
            program.Parse(sTokens);
            return program;
        }

        public List<string> Compile(List<string> lLines)
        {

            List<string> lCompiledCode = new List<string>();
            foreach (string sExpression in lLines)
            {
                List<string> lAssembly = Compile(sExpression);
                lCompiledCode.Add("// " + sExpression);
                lCompiledCode.AddRange(lAssembly);
            }
            return lCompiledCode;
        }



        public List<string> ReadFile(string sFileName)
        {
            StreamReader sr = new StreamReader(sFileName);
            List<string> lCodeLines = new List<string>();
            while (!sr.EndOfStream)
            {
                lCodeLines.Add(sr.ReadLine());
            }
            sr.Close();
            return lCodeLines;
        }

        private bool Contains(string[] a, string s)
        {
            foreach (string s1 in a)
                if (s1 == s)
                    return true;
            return false;
        }
        private bool Contains(char[] a, char c)
        {
            foreach (char c1 in a)
                if (c1 == c)
                    return true;
            return false;
        }

        private List<string> Split(string s, char[] aDelimiters)
        {
            List<string> lTokens = new List<string>();
            while (s.Length > 0)
            {
                string sToken = "";
                int i = 0;
                for (i = 0; i < s.Length; i++)
                {
                    if (Contains(aDelimiters, s[i]))
                    {
                        if (sToken.Length > 0)
                            lTokens.Add(sToken);
                        lTokens.Add(s[i] + "");
                        break;
                    }
                    else
                        sToken += s[i];
                }
                if (i == s.Length)
                {
                    lTokens.Add(sToken);
                    s = "";
                }
                else
                    s = s.Substring(i + 1);
            }
            return lTokens;
        }



        public List<Token> Tokenize(List<string> lCodeLines)
        {
            List<Token> lTokens = new List<Token>();
            int lineNum = 0;
            foreach (string Line in lCodeLines)
            {
                if (Line.Contains("//"))
                {
                    lineNum++;
                    continue;
                }
                int pos = 0;
                List<string> afterSplit = new List<string>();
                afterSplit = Split(Line,Delimiters);
                foreach (string lineToken in afterSplit)
                {
                    Token thisToken;
                    if (Contains(Statements, lineToken))
                    {
                        thisToken = new Statement(lineToken, lineNum, pos);
                        lTokens.Add(thisToken);
                        pos += lineToken.Length;
                    }
                    else if (Contains(VarTypes, lineToken))
                    {
                        thisToken = new VarType(lineToken, lineNum, pos);
                        lTokens.Add(thisToken);
                        pos += lineToken.Length;
                    }
                    else if (Contains(Constants, lineToken))
                    {                        
                        thisToken = new Constant(lineToken, lineNum, pos);
                        lTokens.Add(thisToken);
                        pos += lineToken.Length;
                    }
                    else if (char.IsLetter(lineToken[0]))
                    {                        
                        thisToken = new Identifier(lineToken, lineNum, pos);
                        lTokens.Add(thisToken);
                        pos += lineToken.Length;
                    }
                    else if (char.IsDigit(lineToken[0]))
                    {                        
                        thisToken = new Number(lineToken, lineNum, pos);
                        lTokens.Add(thisToken);
                        pos += lineToken.Length;
                    }
                    else if (lineToken.Length < 2 && !char.IsDigit(lineToken[0]))
                    {
                        char clineToken = lineToken[0];
                        if (clineToken.Equals(' ') || clineToken.Equals('\t'))
                            pos++;
                        else if (Contains(Operators, clineToken))
                        {                           
                           thisToken = new Operator(clineToken, lineNum, pos);
                           lTokens.Add(thisToken);
                            pos++;
                        }
                        else if (Contains(Parentheses, clineToken))
                        {                            
                            thisToken = new Parentheses(clineToken, lineNum, pos);
                            lTokens.Add(thisToken);
                            pos++;
                        }
                        else if (Contains(Separators, clineToken))
                        {                            
                            thisToken = new Separator(clineToken, lineNum, pos);
                            lTokens.Add(thisToken);
                            pos++;
                        }
                        else if (!Contains(Delimiters, clineToken))
                        {
                            pos++;
                            thisToken = new Separator(clineToken, lineNum, pos);                            
                            throw new SyntaxErrorException("Wrong Syntax", thisToken);                            
                        }

                    }
                   
                }
                lineNum++;
            }
            
            return lTokens;
        }
        private string removeTabe(string s)
        {
            while (s.Contains("/t"))
            {
                s = s.Substring(1);
            }
            return s;
        }

        private char[] Delimiters = new char[] { '*', '+', '-', '/', '<', '>', '&', '!', '=', '|', '~', ' ', '(', ')', '[', ']', '{', '}', ',', ';', '\t'}; 
        private string[] Statements = { "function", "var", "let", "while", "if", "else", "return" };
        private string[] VarTypes = { "int", "char", "boolean", "array" };
        private string[] Constants = { "true", "false", "null" };
        private char[] Operators = new char[] { '*', '+', '-', '/', '<', '>', '&', '=', '|', '~' };
        private char[] Parentheses = new char[] { '(', ')', '[', ']', '{', '}' };
        private char[] Separators = new char[] {  ',', ';'};

    }
}
