using System;
using System.Collections.Generic;

namespace HomeWork2
{
    public class MyNode
    {
        public int data;
        public MyNode next;
        public MyNode(int i)
        {
            data = i;
            next = null;
        }
        public void AddToEnd(int data)
        {
            
            if (next == null)
            {
                next = new MyNode(data);
            }
            else
            {
                next.AddToEnd(data);
            }
        }
        public void PrintNode()
        {
            Console.Write(data + "->");
            if (next != null)
            {
                next.PrintNode();
            }
        }
    }
    public class MyList
    {
            public MyNode headNode;
            public  MyList()
            {
                headNode = null;
            }
            public void AddToEnd(int data)
            {
                if (headNode == null)
                {
                    headNode = new MyNode(data);
                }
                else
                {
                    headNode.AddToEnd(data);
                }
                     
            }
        
            public void PrintList()
            {
                if (headNode != null)
                {
                    headNode.PrintNode();
                }
            }
    }
   
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This code was written by Rayene BECH 18011115 for the 2nd Homework of Discrete Mathemathics." +
                "\n\t Subject: Checking if a graph is Bipartite or not");
            Console.WriteLine("First method: Adjacency Matrix: ");
            readMatrix();
            Console.WriteLine("Second method: Adjacency List: ");
            readlist();
        }
        static void readMatrix()
        {
            int n;
            Console.Write("Please enter the total number of vertices(nodes):");
            n = Convert.ToInt32(Console.ReadLine());
            int[,] matrix = new int[n, n];
            Console.WriteLine("\n Now please enter the distance between each two nodes if the graph is weighted.\n" +
                "If the graph is not weighted please enter 1 if the two nodes are connected and 0 if they are not.");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("\n The node {0} with the node {1}:", i + 1, j + 1);
                    matrix[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            Console.WriteLine("The adjacency matrix is:");
            displayMatrix(matrix, n);
            if (isbipartite(matrix, n))
            {
                Console.WriteLine("Yes the graph is Bipartite");
            }
            else
            {
                Console.WriteLine("No the graph is not Bipartite");
            }
        }
        static void displayMatrix(int[,] matrix, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + "|");

                }
                Console.Write("\n");
            }

        }
        static bool isbipartite(int[,] matrix, int n)
        {
            
            
            int i = 0, j, k ;
            //sets is an array to define the set of each node:
            // D means Domain
            // R means Range
            // N means Not defined
            char[] sets = new char[n];
            //initialize the nodes with 'N' Not definied 
            for(int m = 0; m < n; m++)
            {
                sets[m] = 'N';
            }
            //Suppose that the first node is in the domain set
            sets[0]= 'D';
            while (i < n) //n is the total number of nodes. The loop checks all the nodes' connections.
            {
                j = 0;
                while (j < n)
                {
                    if (matrix[i, j] != 0) // if 2 nodes are connected
                    {
                        if (sets[i] == sets[j] || i == j) //if the two nodes are in the same set or there is a loop edge (case of diagonal i==j)
                        {
                            return false; //The graph is then not Bipartite
                        }
                        //Else assign all the nodes connected to the node i to the other set
                        else if (sets[i] == 'D')
                        {
                            sets[j] = 'R';
                        }
                        else
                        {
                            sets[j] = 'D';
                        }
                        //Now search for all the nodes that are connected to the second set and assign them to the first set as i 
                        k = i+1;
                        while (k < n)
                        {
                            if (matrix[k, j] != 0)
                            {
                                if (sets[k] == sets[j])
                                {
                                    return false;
                                }
                                else
                                {
                                    sets[k] = sets[i];
                                }
                            }
                            k++;
                        }
                    }
                    j++;

                }
            i++;
               
            }
            return true;
        }
        static void readlist()
        {
            int n,input;
            Console.Write("Please enter the total number of vertices (nodes):");
            n = Convert.ToInt32(Console.ReadLine());
            MyList[] graph = new MyList[n]; 
            //This is an array of linked lists. Each element represents a list of nodes that are connected to a node
            Console.WriteLine("Please enter ONE BY ONE only the nodes that are connected to each node\n" +
                "For example if node 1 is connected with 2 and 4 please type 2 then press ENTER then type 4 then press Enter " +
                "and then type -1 to finish");
            for(int i = 0; i < n; i++)
            {
                graph[i] = new MyList();
                graph[i].AddToEnd(i+1);
                Console.WriteLine("\n  Enter nodes that are connected to node number {0} ", i + 1);
                input=Convert.ToInt32(Console.ReadLine());
                while (input != -1)
                {
                    graph[i].AddToEnd(input); //Adding nodes that are connected to node number i
                    input = Convert.ToInt32(Console.ReadLine());
                }
                graph[i].PrintList();
                Console.WriteLine("\n");
            }
            for (int i = 0; i < n; i++)
            {
                graph[i].PrintList();
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n Check if the graph is Bipartite:");
            if(listIsBiPartitite(graph, n))
            {
                Console.WriteLine("Yes the graph is Bipartite");
            }
            else
            {
                Console.WriteLine("NO the graph is not Bipartite");
            }
        }
        static bool listIsBiPartitite(MyList[] array, int n)
        {
          
            int i = 0; 
            // This is an array to define the set of each node:
            // D means Domain
            // R means Range
            // N means Not defined
            char[] sets = new char[n];
            //initialize the nodes with 'N' Not definied 
            for (int m = 1; m < n; m++)
            {
                sets[m] = 'N';
            }
            sets[0] = 'D'; //Suppose that the first node is in the domain set
            while (i < n)
            {
                MyNode tmp = array[i].headNode; //tmp node to scan the set
                int headNumber = array[i].headNode.data-1; //Getting the number of the first node of the list
                if(tmp != null)
                {
                    tmp = tmp.next;
                    while (tmp != null)
                    {
                        if (sets[headNumber] == 'N') //If the  headnode is not in any set we check the set of the other nodes that have connections with it
                        {
                            while (tmp != null)
                            {
                                if (sets[tmp.data-1] != 'N') 
                                    //If one of the connections is already in a set then assign the headnode to the opposite set
                                {
                                    if (sets[tmp.data-1] == 'D')
                                    {
                                        sets[headNumber] = 'R';
                                    }
                                    else
                                    {
                                        sets[headNumber] = 'D';
                                    }
                                    tmp = array[i].headNode; //Go back to the beginning of the list to check the previous unassigned nodes
                                    break;
                                }
                                tmp = tmp.next;
                            }
                            if (tmp==null) //If none of the nodes is assigned then assign the headnode to an arbitary set
                            {
                                sets[headNumber] = 'D';
                                tmp = array[i].headNode;
                            }
                        }
                        else if (sets[headNumber] == sets[tmp.data-1]) // If two nodes that are connected with each other belong to the same set
                        {
                            return false; //Then the graph is not Bipartite
                        }
                        //Else assign all the nodes connected to the headnode to the opposite set of the headnode
                        else if (sets[headNumber] == 'D')
                        {
                            sets[tmp.data-1] = 'R';
                        }
                        else
                        {
                            sets[tmp.data-1] = 'D';
                        }
                        tmp = tmp.next;

                    }
                }
                  i++;       
                }
                 return true;
            }
               
               
        }
    }

