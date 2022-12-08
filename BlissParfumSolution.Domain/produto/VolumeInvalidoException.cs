using System;

namespace BlissParfumSolution.Domain.produto
{
    [Serializable]
    public class VolumeInvalidoException : Exception
    {
        public VolumeInvalidoException() : base("O volume do perfume deve ser maior que zero!")
        {
        }
    }
}