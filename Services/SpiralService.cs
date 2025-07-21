namespace SpinutechCodeTest.Services
{
    public class SpiralService : ISpiralService
    {
        public string GenerateSpiral(int maxNumber)
        {
            if (maxNumber < 0)
                return "Error: Please enter a non-negative number";

            try
            {
                int[,] grid = GenerateSpiralGrid(maxNumber);
                return GridToString(grid, maxNumber);
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private int[,] GenerateSpiralGrid(int maxNumber)
        {
            // Calculate grid size
            int size = (int)Math.Ceiling(Math.Sqrt(maxNumber + 1));
            int[,] grid = new int[size, size];
            
            // Initialize with -1 to mark empty cells
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    grid[i, j] = -1;
            
            // Start from center
            int center = size / 2;
            int currentNumber = 0;
            int row = center, col = center;
            
            // Directions: right, down, left, up
            int[] dr = { 0, 1, 0, -1 };
            int[] dc = { 1, 0, -1, 0 };
            int direction = 0;
            int steps = 1;
            int stepCount = 0;
            
            while (currentNumber <= maxNumber)
            {
                // Place current number
                if (row >= 0 && row < size && col >= 0 && col < size)
                {
                    grid[row, col] = currentNumber;
                }
                
                // Move to next position
                row += dr[direction];
                col += dc[direction];
                stepCount++;
                
                // Change direction when step count reaches current step size
                if (stepCount == steps)
                {
                    direction = (direction + 1) % 4;
                    stepCount = 0;
                    
                    // Increase step size every 2 direction changes
                    if (direction % 2 == 0)
                    {
                        steps++;
                    }
                }
                
                currentNumber++;
            }
            
            return grid;
        }

        private string GridToString(int[,] grid, int maxNumber)
        {
            int size = grid.GetLength(0);
            int maxWidth = maxNumber.ToString().Length;
            
            var result = new System.Text.StringBuilder();
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == -1)
                    {
                        result.Append("".PadLeft(maxWidth + 1));
                    }
                    else
                    {
                        result.Append(grid[i, j].ToString().PadLeft(maxWidth) + " ");
                    }
                }
                result.AppendLine();
            }
            
            return result.ToString();
        }
    }
} 