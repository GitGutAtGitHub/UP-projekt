using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarMonoGameTest
{
	class GridManager
	{
		private int cellRowCount;
		private float cellSize;
		private List<Node> grid;
		private  Node[,] nodes = new Node[10, 10];

		public  Node[,] Nodes { get => nodes;}

		public GridManager(int cellRowCount, float cellSize)
		{
			this.cellRowCount = cellRowCount;
			this.cellSize = cellSize;
		}

		public List<Node> CreateGrid()
		{
			grid = new List<Node>();
			for (int y = 0; y < cellRowCount; y++)
			{
				for (int x = 0; x < cellRowCount; x++)
				{
					NodeType type;
					bool walkable;
					if (x == 0 && y == 5)
					{
						type = NodeType.Start;
						walkable = true;
					}
					else if (x == 9 && y == 5)
					{
						type = NodeType.Goal;
						walkable = true;
					}

					else if (x == 9 && (y >= 0) && y != 5)
					{
						type = NodeType.Tower;
						walkable = false;
					}

					else if (x == 0 && (y >= 0) && y != 5)
					{
						type = NodeType.Tower;
						walkable = false;
					}

					else if (y == 0 && (x >= 0))
					{
						type = NodeType.Tower;
						walkable = false;
					}

					else if (y == 9 && (x >= 0))
					{
						type = NodeType.Tower;
						walkable = false;
					}
					else
					{
						type = NodeType.Empty;
						walkable = true;
					}

					Node tmp = new Node(new Vector2(x * cellSize, y * cellSize), type, walkable);

					grid.Add(tmp);
					Nodes[x, y] = tmp;
				}
			}
			return grid;
		}

		private int CalculateDistance(Node inputNode, Node targetNode)
		{
			//afstanden mellem de to noder, x og y
			int dstX = Math.Abs((int)inputNode.GetCoordinate().X - (int)targetNode.GetCoordinate().X);
			int dstY = Math.Abs((int)inputNode.GetCoordinate().Y - (int)targetNode.GetCoordinate().Y);

			//hvis X værdien er højere end y værdien.
			//
			if (dstX > dstY)
			{
				//formel: y + (x-y)
				//formel med ganget op: 14y + 10*(x-y);

				//Y-afstanden er hvor langt den går op, men diagonalt.
				// Derefter finder man ud af X-afstanden ved trække det laveste tal (denne gang y) fra det højeste (x) for at finde 
				//hvor langt den resterende x-strækning er. 

				return 14 * dstY + 10 * (dstX - dstY);
			}
			else
			{
				//det omvendte hvis y er større. 
				return 14 * dstX + 10 * (dstY - dstX);
			}
		}
		private List<Node> GetNeighbours(Node node)
		{
			List<Node> neighbours = new List<Node>();

			//tjekker et 3x3 grid rundt om cellen
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					//når den rammer positionen hvor cellen er, springer den denne over.
					if (x == 0 && y == 0)
					{
						continue;
					}

					//der bliver brugt GetCoordinate så hver tile er fx 1,2,3 osv istedet for 450. 468 etc.
					//gemmer den position den er nået til i forloopet i forhold til hvor den skal søge
					int checkX = (int)node.GetCoordinate().X + x;
					int checkY = (int)node.GetCoordinate().Y + y;

					//tjekker om det er inden for 10x10 gridet der er "banen"
					if (checkX >= 0 && checkX < cellRowCount && checkY >= 0 && checkY < cellRowCount)
					{
						//naboen tilføjes. 

						// Der tilføjes til nodeArrayet
						neighbours.Add(Nodes[checkX, checkY]);
					}
				}
			}
			return neighbours;
		}

		private Stack<Node> RetracePath(Node startNode, Node endNode)
		{
			//laver en ny liste med den fundne sti
			Stack<Node> path = new Stack<Node>();

			//starter ved den sidste node.
			Node currentNode = endNode;

			//så længe man ikke er nået til starten
			while (currentNode != startNode)
			{
				
				if (currentNode != endNode)
				{
					currentNode.Sprite = Asset.pathSprite;
				}
			
				//tilføjer den nuværende node til Sti listen
				path.Push(currentNode);


				//sætter den nuværende node til at være lig med den nuværende nodes Parent node. Dvs den næste i stien
				currentNode = currentNode.Parent;
			}

			//fordi listen starter fra slut og går mod start, skal den køres omvendt.
			path.Reverse();
			return path;
		}

		public Stack<Node> FindPath(Node startNode, Node targetNode)
		{
			Stack<Node> pathStack = new Stack<Node>();

			//liste alle cells, med udregnet Fcost
			List<Node> openList = new List<Node>();

			//allerede evaluerede celler
			List<Node> closedList = new List<Node>();

			//tilføj den første cell til listen
			openList.Add(startNode);

			while (openList.Count > 0)
			{
				//starter ved den første. Current node er den node med den laveste Fcost
				Node currentNode = openList[0];

				//kører hele openlisten igennem
				for (int i = 0; i < openList.Count; i++)
				{
					//tjekker om der er en der har mindre fCost end den nuværende, hvis de er ens, sammenlignes deres Hcost
					if (openList[i].FCost < currentNode.FCost || (openList[i].FCost == currentNode.FCost && openList[i].HCost < currentNode.HCost))
					{
						currentNode = openList[i];
					}
				}

				//færdig med at undersøge cellen, og den fjernes fra den åbne liste, og tilføjes til den lukkede liste
				openList.Remove(currentNode);
				closedList.Add(currentNode);

				//hvis den har fundet den rigtige celle, så skal den trace vejen tilbage til start cellen
				if (currentNode == targetNode)
				{
					
					pathStack = RetracePath(startNode, targetNode);
					
				}

				foreach (Node neighbour in GetNeighbours(currentNode))
				{
					//hvis den node ikke er walkable, en obstruction, eller allerede undersøgt og er på den lukkede lise
					//skip denne her node
					if (neighbour.walkable != true || closedList.Contains(neighbour))
					{
						continue;
					}

					//hvis den nye sti til naboen fra de nuværende er kortere end den gamle (nuværende sti), 
					//eller naboen ikke er i den åbne liste
					int newMovementCostToNeighbour = currentNode.GCost + CalculateDistance(currentNode, neighbour);

					if (newMovementCostToNeighbour < neighbour.GCost || !openList.Contains(neighbour))
					{
						////Naboens gCost er lig med distancen
						neighbour.GCost = newMovementCostToNeighbour;
						//neighbour.GCost = currentNode.GCost + CalculateDistance(currentNode, neighbour);

						//Naboens hCost er lig med afstanden fra naboen til targetNode
						neighbour.HCost = CalculateDistance(neighbour, targetNode);

						//Sætter naboens parent til den nuværende node den kom fra, så den altid ved hvem den kom fra, når den skal bruge den. 
						neighbour.Parent = currentNode;

						//hvis den nabo ikke er i den åbne liste, så tilføjes den.
						if (!openList.Contains(neighbour))
						{
							openList.Add(neighbour);
						}
					}
				}
			}

			return pathStack;
		}
	}
}
