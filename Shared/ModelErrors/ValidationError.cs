namespace Shared.ModelErrors
{
    public class ValidationError
    {
        public string Feild { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}