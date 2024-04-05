using System.Numerics;

// Robert Ware 4/4/2024
// Day 3 of learning C#.
// Algorithm challenge 1 - spiral printer.

namespace SpiralPrinter
{
    public class SpiralPrinter
    {
        #region # Variables

        private static int currentNumber = 1;
        private static int currentSteps = 1;
        private static int targetNumber;
        private static int targetPadding;
        private static int maximumSteps;
        private static int axisCenter;
        private static int axisOffset;
        private static Vector2 currentPosition;
        private static int[,] numberArray = new int[,]{};

        #endregion Variables

        static void Main()
        {
            // Set targetNumber based on user input.
            // Get the number of digits in targetNumber for use in padding numbers with spaces during console printing.
            // Square root targetNumber, then take the ceiling to ensure numberArray is perfectly square and large enough to fit non-square numbers.
            // Divide maximumSteps by 2 and floor it to get its center value, rounded down. Offset it by -1 so it functions as an array index value.
            // Get the remainder of (maximumSteps / 2) to determine an offset value for axisCenter. Returns 0 if targetNumber is even, 1 if odd.
            // Combine axisCenter and axisOffset to determine the true axial center for both even and odd maximumStep values.
            // Create 2D array using maximumSteps to size each axis.
            
            Console.WriteLine("Enter a number that won't blow up your computer.");
            
            targetNumber = Convert.ToInt32(Console.ReadLine());
            targetPadding = targetNumber.ToString().Length;
            maximumSteps = (int)MathF.Ceiling(MathF.Sqrt(targetNumber));
            axisCenter = (int)MathF.Floor(maximumSteps / 2) - 1;
            axisOffset = maximumSteps % 2;
            currentPosition = new(axisCenter + axisOffset, axisCenter + axisOffset);
            numberArray = new int[maximumSteps, maximumSteps];

            Console.WriteLine("");

            // Add first index to numberArray using currentPosition before stepping through spiral pattern.
            StepPositionInDirection(Vector2.Zero, currentSteps);

            // Step currentPosition in each direction n times where n = currentSteps.
            // Increment currentSteps after every other directional change until currentSteps equals maximumSteps.
            while (currentSteps <= maximumSteps)
            {
                StepPositionInDirection(Vector2.UnitX, currentSteps);
                StepPositionInDirection(Vector2.UnitY, currentSteps);
                currentSteps ++;
                StepPositionInDirection(-Vector2.UnitX, currentSteps);
                StepPositionInDirection(-Vector2.UnitY, currentSteps);
                currentSteps ++;
            }

            // Iterate over Y, then X to pull numbers from numberArray by rows instead of columns.
            // For each row of numbers in numberArray, output a string containing that row's numbers.
            for (int y = 0; y < maximumSteps; y ++)
            {
                string line = new("");

                for (int x = 0; x < maximumSteps; x ++)
                {
                    int number = numberArray[x, y];
                    
                    if (number == 0)
                    {
                        // Generate a generic string of letters the length of targetNumber for all numberArray indices that exceed targetNumber.
                        // Totally unneccessary, but looks prettier than just showing zeros for numbers above targetNumber.
                        for (int i = 0; i < targetPadding; i ++)
                        {
                            line = String.Concat(line, "X");
                        }
                        
                        number = targetNumber;
                    }
                    else
                    {
                        line = String.Concat(line, number.ToString());
                    }

                    // Add padding to each number based on its length vs targetNumber's length.
                    int padding = number.ToString().Length;
                    
                    for (int i = padding; i <= targetPadding; i ++)
                    {
                        line = String.Concat(line, " ");
                    }
                }
                
                Console.WriteLine(line);
            }

            Console.ReadKey();
        }

        private static void StepPositionInDirection(Vector2 direction, int steps)
        {
            // Accelerate currentPosition in direction n times where n = steps.
            for (int i = 0; i < steps; i++)
            {
                // Accelerate and set currentNumber to numberArray at currentPosition.
                currentPosition += direction;
                numberArray[(int)currentPosition.X, (int)currentPosition.Y] = currentNumber;
                
                // Break if currentNumber is greater than targetNumber.
                if (currentNumber == targetNumber) {break;}
                
                currentNumber ++;
            }
        }
    }
}