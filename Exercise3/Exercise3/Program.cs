// Jalton Jumawid
// 2020-30010
// May 5, 2021

using System;
using System.Collections;
using System.Collections.Generic;

namespace Exercise3
{
    public class Vertex //general class of vertex
    {
        public string Tag; //status
        public bool IsVisited; //status (if visited) [for 3 and 4]

        public Vertex(string tag)
        {
            Tag = tag;
            IsVisited = false; //conditional
        }
    }

    public class Graph //class of graph (graph 1 and graph 2)
    {
        private const int VERTEX_CAP = 20;
        private Vertex[] Vertices;
        private int[,] Matrix;
        public int VertexCount;

        public Graph()
        {
            Vertices = new Vertex[VERTEX_CAP];
            Matrix = new int[VERTEX_CAP, VERTEX_CAP];
            VertexCount = 0;
            // to insert matrix
            for (int i = 0; i < VERTEX_CAP; i++)
            {
                for (int j = 0; j < VERTEX_CAP; j++)
                {
                    Matrix[i, j] = 0;
                }
            }
        }

        //Add verrtex to matrix 
        public int AddVertex(string tag)
        {
            Vertices[VertexCount] = new Vertex(tag);
            VertexCount++;
            return VertexCount - 1;
        }

        // To see next
        public void AddEdge(int source, int dest)
        {
            AddEdge(source, dest, true);  // default to directed
        }

        // Add an edge to matrix
        public void AddEdge(int source, int dest, bool directed)
        {
            Matrix[source, dest] = 1;
            if (!directed)  // only add symmetry if undirected
                Matrix[dest, source] = 1;
        }

        //check if vertex exist in graph 
        public int SearchVertex(string tag)
        {
            int index = -1;
            for (int i = 0; i < Vertices.Length; i++)
            {
                if (Vertices[i] != null && Vertices[i].Tag == tag)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        //helper for easier identification of graph
        public void Insert(string A, string Direction, string B)
        {
            var Source = A;
            var Destination = B;
            bool directed = false;
            switch (Direction)
            {
                case "<-": //Second element is directed to first element
                    Source = A;
                    Destination = B;
                    directed = true;
                    break;
                case "->": //first element is directed to second element
                    directed = true;
                    break;
                case "<->":  // if it can go either way
                case "-":
                default:
                    break;
            }
            int SourceIndex = SearchVertex(Source);
            if (SourceIndex < 0)
                SourceIndex = AddVertex(Source);
            int DestinationIndex = SearchVertex(Destination);
            if (DestinationIndex < 0)
                DestinationIndex = AddVertex(Destination);
            AddEdge(SourceIndex, DestinationIndex, directed);
        }

        //Display vertex tag
        public void DisplayVertex(int vertexID)
        {
            System.Console.Write(Vertices[vertexID].Tag + " ");
        }

        //Returns next unvivisted graph
        private int GetAdjacentUnvisitedVertex(int v)
        {
            for (int j = 0; j <= VertexCount - 1; j++)
                if ((Matrix[v, j] == 1) && (!Vertices[j].IsVisited))
                    return j;
            return -1;
        }

        //reset all vertices as unvisited
        public void ResetVisits()
        {
            for (int j = 0; j <= VertexCount - 1; j++)
                Vertices[j].IsVisited = false;
        }

        //perform feature 1 (DFS)
        public void DepthFirstSearchTraverse()
        {
            Vertices[0].IsVisited = true;
            DisplayVertex(0);
            Stack nodeStack = new Stack();
            nodeStack.Push(0);  // to keep backtrack
            int vNext;
            while (nodeStack.Count > 0)
            {
                vNext = GetAdjacentUnvisitedVertex((int)nodeStack.Peek());
                if (vNext == -1)
                    nodeStack.Pop();  // if the vertex is not connected to any other vertex
                else
                {
                    Vertices[vNext].IsVisited = true;
                    DisplayVertex(vNext);
                    nodeStack.Push(vNext);  // keep backtrack
                }
            }

            ResetVisits();
        }

        //perform DFS
        public bool DepthFirstSearch(string Key)
        {
            bool IsFound = false;
            Vertices[0].IsVisited = true;
            DisplayVertex(0);
            if (Vertices[0].Tag == Key)
            {
                IsFound = true;
                goto Breakdown;
            }

            Stack nodeStack = new Stack();
            nodeStack.Push(0);  // backtrack
            int vNext;
            while (nodeStack.Count > 0)
            {
                vNext = GetAdjacentUnvisitedVertex((int)nodeStack.Peek());
                if (vNext == -1)
                    nodeStack.Pop();  // if the vertex is not connected to any other vertex
                else
                {
                    Vertices[vNext].IsVisited = true;
                    DisplayVertex(vNext);
                    nodeStack.Push(vNext);  // backtrack

                    if (Vertices[vNext].Tag == Key)
                    {
                        IsFound = true;
                        goto Breakdown;
                    }
                }
            }
            goto Breakdown;

        Breakdown:
            ResetVisits();
            return IsFound;
        }

        //perform feature 2 (BFS)
        public void BreadthFirstSearchTraverse()
        {
            Queue nodeQueue = new Queue();
            Vertices[0].IsVisited = true;
            DisplayVertex(0);
            nodeQueue.Enqueue(0);
            int currentVertex, nextVertex;
            while (nodeQueue.Count > 0)
            {
                currentVertex = (int)nodeQueue.Dequeue();
                nextVertex = GetAdjacentUnvisitedVertex(currentVertex);
                while (nextVertex != -1)
                {
                    Vertices[nextVertex].IsVisited = true;
                    DisplayVertex(nextVertex);
                    nodeQueue.Enqueue(nextVertex);
                    nextVertex = GetAdjacentUnvisitedVertex(currentVertex);
                }
            }

            ResetVisits();
        }

        //perform BFS
        public bool BreadthFirstSearch(string Key)
        {
            bool IsFound = false;
            Queue nodeQueue = new Queue();
            Vertices[0].IsVisited = true;
            DisplayVertex(0);
            nodeQueue.Enqueue(0);

            if (Vertices[0].Tag == Key)
            {
                IsFound = true;
                goto Breakdown;
            }

            int currentVertex, nextVertex;
            while (nodeQueue.Count > 0)
            {
                currentVertex = (int)nodeQueue.Dequeue();
                nextVertex = GetAdjacentUnvisitedVertex(currentVertex);
                while (nextVertex != -1)
                {
                    Vertices[nextVertex].IsVisited = true;
                    DisplayVertex(nextVertex);
                    nodeQueue.Enqueue(nextVertex);

                    if (Vertices[nextVertex].Tag == Key)
                    {
                        IsFound = true;
                        goto Breakdown;
                    }

                    nextVertex = GetAdjacentUnvisitedVertex(currentVertex);
                }
            }
            goto Breakdown;

        Breakdown:
            ResetVisits();
            return IsFound;
        }

    }
    class Program
    {
        static void DisplayMenu()
        {
            System.Console.WriteLine("___________________________________________");
            System.Console.WriteLine("|| Enter Choice Here                     ||");
            System.Console.WriteLine("||                                       ||");
            System.Console.WriteLine("|| [1] Perform Depth First Traversal     ||");
            System.Console.WriteLine("|| [2] Perform Breadth First Traversal   ||");
            System.Console.WriteLine("|| [3] Search Graph 1 (DFS)              ||");
            System.Console.WriteLine("|| [4] Search Graph 2 (BFS)              ||");
            System.Console.WriteLine("|| [5] Exit                              ||");
            System.Console.WriteLine("||_______________________________________||");
            System.Console.WriteLine("");
            System.Console.Write("Enter option: ");
        }

        static void Main(string[] args)
        {
            System.Console.Title = "PANG SUMMARY KO PAG TAMAD";

            // definition for graph 1 (alphabet graph)
            Graph alphabet_graph = new Graph();
            alphabet_graph.Insert("A", "->", "B");
            alphabet_graph.Insert("B", "->", "E");
            alphabet_graph.Insert("B", "->", "C");
            alphabet_graph.Insert("C", "->", "A");
            alphabet_graph.Insert("C", "->", "D");
            alphabet_graph.Insert("C", "->", "G");
            alphabet_graph.Insert("D", "->", "A");
            alphabet_graph.Insert("E", "->", "C");
            alphabet_graph.Insert("E", "->", "F");
            alphabet_graph.Insert("F", "->", "G");
            alphabet_graph.Insert("G", "->", "D");

            // definition for graph 2 (number graph)
            Graph number_graph = new Graph();
            number_graph.Insert("0", "->", "1");
            number_graph.Insert("1", "->", "2");
            number_graph.Insert("1", "->", "5");
            number_graph.Insert("2", "->", "3");
            number_graph.Insert("2", "->", "4");
            number_graph.Insert("4", "->", "5");

            DisplayMenu();
            string UserInput = System.Console.ReadLine();
            while (UserInput != "5")
            {
                switch (UserInput)
                {
                    case "1": // Perform Depth First Traversal
                        System.Console.WriteLine("Perform Depth Fist Traversal");
                        System.Console.Write("What graph do you want to use? [1 or 2]: ");
                        string GraphInput = System.Console.ReadLine();
                        Graph selectedGraph = GraphInput == "1" ? alphabet_graph : GraphInput == "2" ? number_graph : null;
                        while (selectedGraph == null)
                        {
                            System.Console.Write("What graph do you want to use? [1 or 2]: ");
                            GraphInput = System.Console.ReadLine();
                            selectedGraph = GraphInput == "1" ? alphabet_graph : GraphInput == "2" ? number_graph : null;
                        }
                        System.Console.WriteLine("DFS on Graph {0}", GraphInput);
                        selectedGraph.DepthFirstSearchTraverse();
                        break;

                    case "2": // Perform Breadth First Traversal
                        System.Console.WriteLine("Perform Breadth Fist Traversal");
                        System.Console.Write("What graph do you want to use? [1 or 2]: ");
                        string GraphInput2 = System.Console.ReadLine();
                        Graph selectedGraph2 = GraphInput2 == "1" ? alphabet_graph : GraphInput2 == "2" ? number_graph : null;
                        while (selectedGraph2 == null)
                        {
                            System.Console.Write("What graph do you want to use? [1 or 2]: ");
                            GraphInput2 = System.Console.ReadLine();
                            selectedGraph2 = GraphInput2 == "1" ? alphabet_graph : GraphInput2 == "2" ? number_graph : null;
                        }
                        System.Console.WriteLine("DFS on Graph {0}", GraphInput2);
                        selectedGraph2.BreadthFirstSearchTraverse();
                        break;

                    case "3": // Search Graph 1 (DFS)
                        System.Console.WriteLine("Search Graph 1 (DFS)");
                        System.Console.Write("Enter vertex: ");
                        string vertex1 = System.Console.ReadLine();
                        System.Console.Write("Searching ");
                        if (alphabet_graph.DepthFirstSearch(vertex1))
                        {
                            System.Console.WriteLine("...\n'{0}' is found in graph!", vertex1);
                        }
                        else
                        {
                            System.Console.WriteLine("...\n'{0}' is not found in graph!", vertex1);
                        }
                        break;

                    case "4": // Search Graph 2
                        System.Console.WriteLine("Search Graph 2 (BFS)");
                        System.Console.Write("Enter vertex: ");
                        string vertex2 = System.Console.ReadLine();
                        System.Console.Write("Searching ");
                        if (number_graph.DepthFirstSearch(vertex2))
                        {
                            System.Console.WriteLine("...\n'{0}' is found in graph!", vertex2);
                        }
                        else
                        {
                            System.Console.WriteLine("...\n'{0}' is not found in graph!", vertex2);
                        }
                        break;

                    default:
                        System.Console.Clear();
                        break;
                }

                System.Console.WriteLine();
                DisplayMenu();
                UserInput = System.Console.ReadLine();
            }

        }
    }
}
