namespace SpinutechCodeTest.Services
{
    public interface IPokerHandService
    {
        string EvaluateHand(string handInput);
        string GetHandDescription(string handInput);
    }
} 