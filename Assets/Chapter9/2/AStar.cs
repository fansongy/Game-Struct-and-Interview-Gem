using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour {

	public static int wide = 6;
	public static int height = 5;

	public static int[,] pathTable = new int[,]{
		{ 0, 0, 0, 0,-1, 0},
		{ 0, 0, 0, 0,-1, 0},
		{ 0, 0, 0, 0,-1, 0},
		{ 0, 0,-1,-1,-1, 0},
		{ 0, 0, 0, 0, 0, 0},
	};

	public class SearchNode
	{
		public Vector2 pos;
		public float f;
		public float g;
		public float h;
		public SearchNode parent;

		public SearchNode(Vector2 pos)
		{
			this.pos = pos;
			parent = null;
		}
	};

	void Start()
	{
		SearchNode start = new SearchNode(new Vector2(0, 1));
		SearchNode end = new SearchNode(new Vector2(5, 1));
		FindWay(start, end);
	}


	void FindWay(SearchNode startNode, SearchNode endNode)
	{
		List<SearchNode> openset = new List<SearchNode>();
		List<SearchNode> closeset = new List<SearchNode>();
		SearchNode currentNode = startNode;

		while (!isEqualNode(currentNode,endNode))
		{
			//获取currentNode节点周围的节点
			List<SearchNode> adjacentNode = getAdjacent(currentNode);
			for (int i = 0; i < adjacentNode.Count; ++i)
			{
				//计算f(x)，并做集合操作
				SearchNode tryNode = adjacentNode[i];
				if (ContainNode(closeset,tryNode))
				{
					continue;
				}
				if (ContainNode(openset,tryNode))
				{
					//以当前节点为基础，计算g
					var cur_g = currentNode.g+1;
					//有更优的解
					if (cur_g < tryNode.g)
					{
						tryNode.parent = currentNode;
						tryNode.g = cur_g;
						tryNode.f = tryNode.g + tryNode.h;
					}
				}
				else
				{
					tryNode.parent = currentNode;
					tryNode.h = Mathf.Abs(tryNode.pos.x - endNode.pos.x) + Mathf.Abs(tryNode.pos.y - endNode.pos.y);
					tryNode.g = currentNode.g+1;
					tryNode.f = tryNode.g + tryNode.h;
					openset.Add(tryNode);
				}
			}

			if (openset.Count == 0)
			{
				break;
			}

			//找到f(x)最小的节点
			currentNode = getMinFNode(openset);
			RemoveNode(openset,currentNode);
			closeset.Add(currentNode);
		}

		//输出结果逻辑
		if (isEqualNode(currentNode, endNode))
		{
			//使用栈反转链表
			Stack<SearchNode> path = new Stack<SearchNode>();
			SearchNode node = GetNode(closeset, endNode);
			while (node != null)
			{
				path.Push(node);
				node = node.parent;
			}

			//输出结果
			string str = "Path is: ";
			node = path.Pop();
			while (path.Count > 0)
			{
				str += " [" + (int)node.pos.x + "," + (int)node.pos.y + "] ";
				node = path.Pop();
			}
			//在输出中增加最后的节点
			str += " [" + (int)endNode.pos.x + "," + (int)endNode.pos.y + "] ";
			Debug.Log(str);
		}
		else
		{
			Debug.Log("Can't find the way!");
		}
	}

	SearchNode GetNode(List<SearchNode> set, SearchNode node)
	{
		for (int i = 0; i < set.Count; ++i)
		{
			if (isEqualNode(set[i],node))
			{
				return set[i];
			}
		}
		return null;
	}

	bool ContainNode(List<SearchNode> set,SearchNode node)
	{
		for (int i = 0; i < set.Count; ++i)
		{
			if (isEqualNode(set[i],node))
			{
				return true;
			}
		}
		return false;
	}

	void RemoveNode(List<SearchNode> set, SearchNode node)
	{
		for (int i = 0; i < set.Count; ++i)
		{
			if (isEqualNode(set[i],node))
			{
				set.RemoveAt(i);
				return;
			}
		}
	}

	SearchNode getMinFNode(List<SearchNode> openset)
	{
		int index = 0;
		float min = wide+height;
		for (int i = 0; i < openset.Count; ++i)
		{
			if (min > openset[i].f)
			{
				min = openset[i].f;
				index = i;
			}
		}
		return openset[index];
	}

	List<SearchNode> getAdjacent(SearchNode centerNode)
	{
		Vector2 up = new Vector2(centerNode.pos.x, centerNode.pos.y - 1);
		Vector2 down = new Vector2(centerNode.pos.x, centerNode.pos.y + 1);
		Vector2 left = new Vector2(centerNode.pos.x - 1, centerNode.pos.y);
		Vector2 right = new Vector2(centerNode.pos.x + 1, centerNode.pos.y);

		List<SearchNode> nodeList = new List<SearchNode>();
		if (isValidPos(up))
		{
			nodeList.Add(new SearchNode(up));
		}

		if (isValidPos(down))
		{
			nodeList.Add(new SearchNode(down));
		}

		if (isValidPos(left))
		{
			nodeList.Add(new SearchNode(left));
		}

		if (isValidPos(right))
		{
			nodeList.Add(new SearchNode(right));
		}

		return nodeList;
	}


	bool isEqualNode(SearchNode a, SearchNode b)
	{
		return ((int)a.pos.x == (int)b.pos.x &&
			(int)a.pos.y == (int)b.pos.y);
	}

	bool isValidPos(Vector2 pos)
	{
		if (pos.x < 0 || pos.x > wide-1 || 
			pos.y < 0 || pos.y > height-1)
		{
			return false;
		}
		return pathTable[(int)pos.y,(int)pos.x] == 0 ? true:false;
	}
}
