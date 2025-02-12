namespace AStar_MazeProject
{
     class NodeExplorer
    {
        // Calculates the Manhattan distance between the current point and the start point.
        public static int EdgeCostMethod(int[] currentPoint, int[] startPoint)
        {
            return Math.Abs((currentPoint[0] - startPoint[0]))
                                   + Math.Abs((currentPoint[1] - startPoint[1]));
        }

        // Calculates the squared Euclidean distance (hypotenuse squared) between the current point and the goal.
        public static int HeuristicCostMethod(int[] currentPoint, int[] goal)
        {
            int a_Leg = Math.Abs(currentPoint[0] - goal[0]);
            int b_Leg = Math.Abs(currentPoint[1] - goal[1]);
            double hypotenuse = Math.Pow(a_Leg, 2) + Math.Pow(b_Leg, 2);

            return (int)hypotenuse;
        }

        // Combines the heuristic cost and edge cost to calculate the total cost.
        public static int TotalCostMethod(int heuristicCost, int edgeCost)
        {
            return heuristicCost + edgeCost;
        }

        // Iterates through the open list of nodes to find the node with the lowest total cost.
        public static int FindBestCost()
        {
            int bestCost = 500;

            foreach (Node node in PathFinderState.OpenListQueue)
            {
                int totalCost = node.TotalCost;
                if (totalCost < bestCost)
                {
                    bestCost = totalCost;
                }
            }
            return bestCost;
        }

        // Retrieves the node with the best (lowest) total cost from the open list.
        // This node is then added to the path list for further processing.
        public static Node GetNodeWithBestCost(int bestCost)
        {
            PathFinderState.ListOpenList = PathFinderState.OpenListQueue.ToList();

            Node bestNode = PathFinderState.ListOpenList.Find(node => node.TotalCost == bestCost);
            PathFinderState.PathList.Add(bestNode);
            return bestNode;
        }

        // Removes a node from the open list once it has been explored.
        public static void RemoveNodeFromOpenList(Node bestNode)
        {
            int index = PathFinderState.ListOpenList.IndexOf(bestNode);
            PathFinderState.ListOpenList.RemoveAt(index);
        }

        // Checks if a node has already been explored by comparing its coordinates with those in the explored queue.
        public static bool CheckIfNodeExplored(Node obj, int[] coordChanges)
        {

            foreach (Node exploredNode in PathFinderState.ExploredQueue.ToList())
            {
                int checkForNodeRow = obj.Row + coordChanges[0];
                int checkForNodeColumn = obj.Column + coordChanges[1];

                if (exploredNode.Row == checkForNodeRow
                 && exploredNode.Column == checkForNodeColumn)
                {
                    return true;
                }
            }
            return false;
        }

        // Explores a node by calculating its position and adding it to the explored queue.
        public static void ExploreNode(Node currentNode, Node startNode,
                                       Node goalNode, int[] coordChanges)
        {
            int[] currentNodePosition = [currentNode.Row, currentNode.Column];
            int[] startNodePosition = [startNode.Row, startNode.Column];
            int[] goalNodePosition = [goalNode.Row, goalNode.Column];

            Child child = new Child(currentNodePosition, startNodePosition, goalNodePosition, coordChanges, currentNodePosition);
            PathFinderState.ExploredQueue.Enqueue(child);

            if (child.State != (int)Maze.MazeComponents.Wall)
            {
                PathFinderState.OpenListQueue.Enqueue(child);
            }
        }

        // Iterates through possible directions (down, right, up, left) to find valid neighboring nodes.
        // If a neighboring node has not been explored, it is added to the open list.
        public static void GetValidNeighbourVisited(Node current, Node start, Node goal)
        {
            List<int[]> directions = new List<int[]>();
            directions.Add([-1, 0]);    // going down 
            directions.Add([ 0, 1]);    // going right
            directions.Add([ 1, 0]);    // going up
            directions.Add([ 0,-1]);    // going left

            foreach (int[] direction in directions)
            {
                bool result = NodeExplorer.CheckIfNodeExplored(current, direction);

                if (!result)
                {
                    NodeExplorer.ExploreNode(current, start, goal, direction);
                }
            }
        }

        // Recursively backtracks from the current node to the start node to reconstruct the shortest path.
        public static void Backtracking(Node backtrackingFrom, Node backtrackingTo)
        {
            PathFinderState.ShortestPathList.Add(backtrackingFrom);
            int[] currentParrent = [backtrackingFrom.Parent[0], backtrackingFrom.Parent[1]];

            foreach (Node neighbour in PathFinderState.PathList)
            {
                if (currentParrent[0] == backtrackingTo.Row && currentParrent[1] == backtrackingTo.Column)
                {
                    break;
                }
                if (neighbour.Row == currentParrent[0] && neighbour.Column == currentParrent[1])
                {
                    PathFinderState.ShortestPathList.Add(neighbour);
                    Backtracking(neighbour, backtrackingTo);
                }
            }
        }
    }
}
