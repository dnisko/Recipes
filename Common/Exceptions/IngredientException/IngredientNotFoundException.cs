﻿namespace Common.Exceptions.IngredientException
{
    public class IngredientNotFoundException : Exception
    {
        public IngredientNotFoundException(string message) : base(message)
        {
        }
        public IngredientNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
