namespace AStar_MazeProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isFirstgoalPassed = false;
            bool isEndGoalFound = false;
            Node startNode = MazeState.InitState;
            Node goalNode = MazeState.FirstGoal;

            //Creates and displays the maze environment
            Node[,] maze = Maze.CreateMaze([startNode.Row, startNode.Column], [goalNode.Row, goalNode.Column]);
            Display.simulationPartOne(maze);

            // A* search logic ↓
            // Marks starting position as explored        
            PathFinderState.ExploredQueue.Enqueue(startNode);
            PathFinderState.OpenListQueue.Enqueue(startNode);

            while (PathFinderState.OpenListQueue.Count > 0)
            {
                // Searches for lowest Total cost
                int bestCost = NodeExplorer.FindBestCost();
                Node bestNode = NodeExplorer.GetNodeWithBestCost(bestCost);
                NodeExplorer.RemoveNodeFromOpenList(bestNode);
                PathFinderState.OpenListQueue = new Queue<Node>(PathFinderState.ListOpenList);

                // Checks if the First goal is found. In case it is found, it sets the new goal
                // and computes costs from that position to the end goal.
                Node currentNode = bestNode;
                if (!isFirstgoalPassed)
                {
                    if (currentNode.Row == goalNode.Row
                     && currentNode.Column == goalNode.Column)
                    {
                        startNode = MazeState.FirstGoal;
                        goalNode = MazeState.EndGoal;
                        maze = Maze.CreateMaze([startNode.Row, startNode.Column], [goalNode.Row, goalNode.Column]);

                        isFirstgoalPassed = true;
                    }
                }
                // Checks if the End goal is found
                if (currentNode.Row == goalNode.Row
                 && currentNode.Column == goalNode.Column)
                {
                    Display.simulationPartTwo(maze);
                    isEndGoalFound = true;
                    break;
                }
                else
                {
                    // Searches for neighbours inside the maze's boundaries
                    if (currentNode.Column <= Maze.mazeLastColumn
                     && currentNode.Column > Maze.mazeFirstColumn
                     && currentNode.Row <= Maze.mazeLastRow
                     && currentNode.Row > Maze.mazeFirstRow)
                    {
                        NodeExplorer.GetValidNeighbourVisited(currentNode, startNode, goalNode);
                    }
                }
            }
            if (isEndGoalFound)
            {
                //Displays the shortest path to reach the goal
                PathFinderState.PathList.Reverse();
                NodeExplorer.Backtracking(MazeState.EndGoal, MazeState.InitState);
                Maze.UpdateMaze(maze, PathFinderState.ShortestPathList, (int)Maze.MazeComponents.ShortestPath);
                Display.DisplayMaze(maze);
                Display.closeProgram(maze);
            }
            else
            {
                Console.WriteLine("There is no solution for this problem.");
                return;
            }
        }
    }
 }
