namespace ChangeCalculatorApi.Models
{
    public class ChangeResponse
    {
        public Dictionary<string, int> Breakdown { get; set; } = new();
    }
}
