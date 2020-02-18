using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    class Number : Token
    {
        public int Value { get; set; }
        public Number(string name, int line, int position)
        {
             if (name.Length > 1 && char.IsDigit(name[0]))
            {
                for (int i = 1; i < name.Length; i++)
                    if (!char.IsDigit(name[i]))
                        throw new SyntaxErrorException("Wrong Syntax", this);
            }
            Line = line;
            Position = position;
            Value = int.Parse(name);
        }
        public override bool Equals(object obj)
        {
            if (obj is Number)
            {
                Number t = (Number)obj;
                if (Value == t.Value)
                    return base.Equals(t);
            }
            return false;
        }
        public override string ToString()
        {
            return Value + "";
        }


    }
}
