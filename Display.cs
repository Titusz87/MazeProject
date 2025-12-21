namespace AStar_MazeProject
{
    internal class Display
    {
        public static void DisplayMaze(Node[,] maze)
        {
            // Initializes maze components with their corresponding integer values for rendering the map to the console.
            int space = (int)Maze.MazeComponents.Space;
            int wall = (int)Maze.MazeComponents.Wall;
            int startingPosition = (int)Maze.MazeComponents.StartingPosition;
            int goalPosition = (int)Maze.MazeComponents.GoalPosition;
            int firstSearchPath = (int)Maze.MazeComponents.FirstSearchPath;
            int shortestPath = (int)Maze.MazeComponents.ShortestPath;
            int lastColumn = Maze.mazeLastColumn;

            for (int row = 0; row < 12; row++)
            {
                for (int column = 0; column < 24; column++)
                {
                    switch (maze[row, column])
                    {
                        case Node node when node.State == shortestPath:

                            Console.BackgroundColor = ConsoleColor.White;
                            Console.Write($" {shortestPath}, ");
                            Console.ResetColor();
                            break;

                        case Node node when node.State == startingPosition:

                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write($" {startingPosition}, ");
                            Console.ResetColor();
                            break;

                        case Node node when node.State == goalPosition:

                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write($" {goalPosition}, ");
                            Console.ResetColor();
                            break;

                        case Node node when column < lastColumn
                                         && node.State != startingPosition
                                         && node.State != goalPosition
                                         && node.State != firstSearchPath
                                         && node.State != shortestPath:

                            switch (node.State)
                            {
                                case int nestState when nestState == wall:

                                    Console.BackgroundColor = ConsoleColor.Green;
                                    break;
                            }

                            Console.Write($" {space}, ");
                            Console.ResetColor();
                            break;

                        case Node node when column == lastColumn:

                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write($" {wall}\n");
                            Console.ResetColor();
                            break;
                    }
                }
            }
        }

        // Handles display logic for the maze simulation.
        public static void simulationPartOne(Node[,] maze)
        {
            Console.Write("The maze has been built.");
            Thread.Sleep(1500);
            Console.Clear();
            Console.Write("Positioning the start (Red) and end goal node (Blue)...");
            Thread.Sleep(4000);
            Console.Clear();
            Console.Write("Displaying...");
            Thread.Sleep(3000);
            Console.Clear();
            Display.DisplayMaze(maze);
            Console.Write("Press Enter to start the A* search.");
            Console.ReadKey();
            Console.Clear();
            Display.DisplayMaze(maze);
            Console.Write("Searching...");
            Thread.Sleep(6000);
            Console.Clear();
            Display.DisplayMaze(maze);
        }
        public static void simulationPartTwo(Node[,] maze)
        {
            Console.Write($"The goal marked blue has been reached.");
            Thread.Sleep(2500);
            Console.Clear();
            Display.DisplayMaze(maze);
            Console.Write("Press Enter to calculate the shortest path to the goal.");
            Console.ReadKey();
            Console.Clear();
            Display.DisplayMaze(maze);
            Console.Write("Calculating...");
            Thread.Sleep(4000);
            Console.Clear();
            Display.DisplayMaze(maze);
            Console.Write("The shortest path has been found.");
            Thread.Sleep(2000);
            Console.Clear();
            Display.DisplayMaze(maze);
            Console.Write("Displaying the solution...");
            Thread.Sleep(2000);
            Console.Clear();

        }
         public static void closeProgram(Node[,] maze)
        {
            Console.Write("Press Enter to close the program.");
            Console.ReadKey();
        }
    }
}
