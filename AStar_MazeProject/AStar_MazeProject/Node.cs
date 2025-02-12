namespace AStar_MazeProject
{
	// Defines the Node and Child classes, which represent positions and states in the pathfinding algorithm.
	public class Node
	{
		private int _row;
		private int _column;
		private int _state;
		private int _edgeCost;
		private int _heuristicCost;
		private int _totalCost;
		private int[] _parent;

		public int Row { get { return _row; } set { _row = value; } }
		public int Column { get { return _column; } set { _column = value; } }
		public int State { get { return _state; } set { _state = value; } }
		public int EdgeCost { get { return _edgeCost; } set { _edgeCost = value; } }
		public int HeuristicCost { get { return _heuristicCost; } set { _heuristicCost = value; } }
		public int TotalCost { get { return _totalCost; } set { _totalCost = value; } }
		public int[] Parent { get { return _parent; } set { _parent = value; } }


		public Node(int[] currentPosition, int[] startPoint, int[] goal, int[] parent)
		{
			Row = currentPosition[0];
			Column = currentPosition[1];
			State = Maze.MazeIntGrid[Row, Column];
			Parent = parent;
			EdgeCost = NodeExplorer.EdgeCostMethod(currentPosition, startPoint);
			HeuristicCost = NodeExplorer.HeuristicCostMethod(currentPosition, goal);
			TotalCost = NodeExplorer.TotalCostMethod(HeuristicCost, EdgeCost);
		}

	}
	public class Child : Node
	{
		public Child(int[] currentPosition, int[] startPoint, int[] goal, int[] changeOnPosition, int[] parent)
														: base(currentPosition, startPoint, goal, parent)
		{
			Row += changeOnPosition[0];
			Column += changeOnPosition[1];
			State = Maze.MazeIntGrid[Row, Column];
			EdgeCost = NodeExplorer.EdgeCostMethod([Row, Column], startPoint);
			HeuristicCost = NodeExplorer.HeuristicCostMethod([Row, Column], goal);
			TotalCost = NodeExplorer.TotalCostMethod(HeuristicCost, EdgeCost);
		}
	}
}