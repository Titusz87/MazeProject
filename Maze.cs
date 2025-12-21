namespace AStar_MazeProject
{
    public static class Maze
    {
        public enum MazeComponents
        {
            Space,            //0
            Wall,             //1
            StartingPosition, //2
            GoalPosition,     //3
            FirstSearchPath,  //4
            ShortestPath,     //5
        }

        public static int[,] MazeIntGrid { get { return _mazeIntGrid; } }

        // Declares the map structure in a 2D array
        private static int[,] _mazeIntGrid
        ={{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
          { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
          { 1, 0, 2, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1 },
          { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
          { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 3, 1, 0, 1 },
          { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
          { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
          { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
          { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1 },
          { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1 },
          { 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
          { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
          };

        // Variables to retrieve maze dimensions
        public static int mazeFirstColumn { get; } = 0;
        public static int mazeLastColumn { get; } = _mazeIntGrid.GetLength(1) - 1;
        public static int mazeFirstRow { get; } = 0;
        public static int mazeLastRow { get; } = _mazeIntGrid.GetLength(0);


        public static Node[,] CreateMaze(int[] startPoint, int[] goal)
        {
            int[] currentPosition;
            Node[,] maze = new Node[12, 24];

            for (int row = 0; row < 12; row++)
            {
                for (int column = 0; column < 24; column++)
                {
                    currentPosition = new int[] { row, column };
                    maze[row, column] = new Node(currentPosition, startPoint, goal, null);
                }
            }
            return maze;
        }

        //Updates the maze corresponding to node states
        public static void UpdateMaze(Node[,] maze, List<Node> list, int stateChange)
        {
            int initialState = 2;
            int goalState = 3;
            foreach (Node steps in list)
            {
                int step0 = steps.Row;
                int step1 = steps.Column;
                for (int row = 0; row < 12; row++)
                {
                    for (int column = 0; column < 24; column++)
                    {
                        if (row == step0 && column == step1
                            && maze[row, column].State != initialState
                            && maze[row, column].State != goalState)
                        {
                            maze[row, column].State = stateChange;
                        }
                    }
                }
            }
        }
    }
   
    public static class MazeState
    {
        public static Node InitState { get; }
        public static Node FirstGoal { get; }
        public static Node EndGoal { get; }

        // Static constructor for initialization
        static MazeState()
        {
            InitState = new Node([2, 2], [2, 2], [4, 20], [2, 2]);
            FirstGoal = new Node([7, 2], [2, 2], [7, 2], [7, 1]);
            EndGoal = new Node([4, 20], [2, 2], [4, 20], [5, 20]);
        }
    }

    // Declares data structures to track the state of the pathfinding algorithm.
    public static class PathFinderState
    {
        public static Queue<Node> OpenListQueue { get; set; } = new Queue<Node>();
        public static Queue<Node> ExploredQueue { get; set; } = new Queue<Node>();
        public static List<Node> ShortestPathList { get; set; } = new List<Node>();
        public static List<Node> ListOpenList { get; set; } = new List<Node>();
        public static List<Node> PathList { get; set; } = new List<Node>();
    }
}
