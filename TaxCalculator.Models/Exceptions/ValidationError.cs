namespace TaxCalculator.Models.Exceptions
{
    public class ValidationError
    {
        public ValidationError(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }

        public string FieldName { get; set; }

        public string Message { get; set; }
    }
}
