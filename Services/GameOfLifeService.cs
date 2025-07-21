using SpinutechCodeTest.Constants;

namespace SpinutechCodeTest.Services
{
    public class GameOfLifeService : IGameOfLifeService
    {
        public string NextGeneration(string boardInput)
        {
            try
            {
                int[,] board = ParseBoard(boardInput);
                int[,] newBoard = CalculateNextGeneration(board);
                return BoardToString(newBoard);
            }
            catch (Exception ex)
            {
                return AppConstants.GameOfLife.Error + ex.Message;
            }
        }

        public string EvolveMultipleGenerations(string boardInput, int generations)
        {
            try
            {
                if (generations < 0)
                    return AppConstants.GameOfLife.Error + AppConstants.GameOfLife.NumberOfGenerationsMustBeNonNegative;

                int[,] currentBoard = ParseBoard(boardInput);
                
                for (int gen = 0; gen < generations; gen++)
                {
                    currentBoard = CalculateNextGeneration(currentBoard);
                }
                
                return BoardToString(currentBoard);
            }
            catch (Exception ex)
            {
                return AppConstants.GameOfLife.Error + ex.Message;
            }
        }

        private int[,] ParseBoard(string boardInput)
        {
            var lines = boardInput.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
                throw new ArgumentException(AppConstants.GameOfLife.EmptyBoardInput);

            int rows = lines.Length;
            int cols = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

            int[,] board = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                var numbers = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (numbers.Length != cols)
                    throw new ArgumentException(string.Format(AppConstants.GameOfLife.RowHasDifferentNumberOfColumns, i + 1));

                for (int j = 0; j < cols; j++)
                {
                    if (!int.TryParse(numbers[j], out int value) || (value != 0 && value != 1))
                        throw new ArgumentException($"Invalid value at row {i + 1}, column {j + 1}: {numbers[j]}");
                    
                    board[i, j] = value;
                }
            }

            return board;
        }

        private int[,] CalculateNextGeneration(int[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            int[,] newBoard = new int[rows, cols];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int neighbors = CountLiveNeighbors(board, i, j);
                    bool isAlive = board[i, j] == 1;
                    
                    // Apply Game of Life rules
                    if (isAlive)
                    {
                        // Survival: 2 or 3 neighbors
                        newBoard[i, j] = (neighbors == 2 || neighbors == 3) ? 1 : 0;
                    }
                    else
                    {
                        // Reproduction: exactly 3 neighbors
                        newBoard[i, j] = (neighbors == 3) ? 1 : 0;
                    }
                }
            }
            
            return newBoard;
        }
        
        private int CountLiveNeighbors(int[,] board, int row, int col)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            int count = 0;
            
            // Check all 8 neighbors
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue; // Skip the cell itself
                    
                    int newRow = row + i;
                    int newCol = col + j;
                    
                    // Check bounds
                    if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                    {
                        count += board[newRow, newCol];
                    }
                }
            }
            
            return count;
        }
        
        private string BoardToString(int[,] board)
        {
            int rows = board.GetLength(0);
            int cols = board.GetLength(1);
            var result = new System.Text.StringBuilder();
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result.Append(board[i, j] + " ");
                }
                result.AppendLine();
            }
            
            return result.ToString().TrimEnd();
        }
    }
} 