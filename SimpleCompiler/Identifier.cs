using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCompiler
{
    public class Identifier : Token
    {
        public string Name { get; set; }
        public Identifier(string name, int line, int position)
        {
             if (name.Length > 1 && char.IsDigit(name[0]))
            {
                for (int i = 1; i < name.Length; i++)
                    if (!char.IsDigit(name[i]))
                        throw new SyntaxErrorException("Wrong Syntax", this);
            }
            Line = line;
            Position = position;
            Name = name;

            //you can add code here to identify invalid identifiers and throw an exception
        }
        public override bool Equals(object obj)
        {
            if (obj is Identifier)
            {
                Identifier t = (Identifier)obj;
                if (Name == t.Name)
                    return base.Equals(t);
            }
            return false;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
