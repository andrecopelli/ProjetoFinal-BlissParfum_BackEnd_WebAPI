using System;

namespace BlissParfumSolution.Domain.cliente
{
    [Serializable]
    public class DataInvalidaException : Exception
    {
        public DataInvalidaException() : base ("A data de nascimento deve ser menor do que a data atual!")
        {
        }
    }
}