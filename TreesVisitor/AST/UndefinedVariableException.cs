using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AST
{
    public class UndefinedVariableException : Exception
    {
        public UndefinedVariableException(Variable variable) : base($"{variable.identifier} was not defined")
        {

        }
    }
}
