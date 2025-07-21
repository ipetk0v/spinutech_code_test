namespace SpinutechCodeTest.Services
{
    public interface IGameOfLifeService
    {
        string NextGeneration(string boardInput);
        string EvolveMultipleGenerations(string boardInput, int generations);
    }
} 