// Jalton Jan Jumawid
// 2020-30010
// April 24, 2021

using System;
using System.Collections.Generic;

namespace Exercise2
{
    class Node //class for a single node of a tree
    {
        public int value; //actual value of the node
        public Node left, right; //pointers

        public Node(int val) //node constructors
        {
            value = val;
        }
    } //End class Node
    class Program //class containing the tree methods and its sub methods
    {
        static Node root = null;
        static void Main(string[] args)
        {
            int option;
            List<int> options = new List<int>()
            { //display menu 
                1,2,3,4,5,6,7,8
            };

            do
            {
                Console.WriteLine("Select an Option");
                Console.WriteLine("");
                Console.WriteLine("[1] Insert node to binary tree");
                Console.WriteLine("[2] Delete node from binary tree");
                Console.WriteLine("[3] Minimum");
                Console.WriteLine("[4] Maximum");
                Console.WriteLine("[5] Successor");
                Console.WriteLine("[6] Predecessor");
                Console.WriteLine("[7] Search");
                Console.WriteLine("[8] Print BST");
                Console.WriteLine("");
                Console.WriteLine("Enter Choice: ");

                String sOptions = Console.ReadLine();

                if (!int.TryParse(sOptions, out option))
                {
                    break;
                }

                switch (option)
                {
                    case 1: //To satisfy feature 3
                        InsertNode();
                        break;
                    case 2: //To satisfy feature 4
                        DeleteNode();
                        break;
                    case 3: //To satisfy feature 5
                        Minimum();
                        break;
                    case 4://To satisfy feature 6
                        Maximum();
                        break;
                    case 5://To satisfy feature 7
                        Successor();
                        break;
                    case 6: //To satisfy feature 8
                        Predecessor();
                        break;
                    case 7: //To satisfy feature 9
                        Search();
                        break;
                    case 8://To satisfy feature 10
                        PrintBST();
                        break;
                    default:
                        option = 0;
                        Console.Clear();
                        break;
                }
            } while (options.Contains(option));

            Console.WriteLine("Exiting application");
            Console.ReadKey();
        }

        static void InsertNode() //Inserts node to the tree
        { //to know where the value will be put in the tree
            int val;
            Console.Write("Enter value to add: ");

            if (!int.TryParse(Console.ReadLine(), out val))
            {
                Console.WriteLine("Could not parse value");
                return;
            }

            if (root == null)
            {
                root = new Node(val);
                return;
            }

            Node current = root;

            while (current != null)
            {
                if (val.Equals(current.value))
                {
                    break;
                }
                else if (val < current.value)
                {
                    if (current.left != null)
                    {
                        current = current.left;
                    }
                    else
                    {
                        current.left = new Node(val);
                        current = current.left;
                    }
                } //Section for navigating of nodes
                else
                {
                    if (current.right != null)
                    {
                        current = current.right;
                    }
                    else
                    {
                        current.right = new Node(val);
                        current = current.right;
                    }
                } //If left is occupied, then proceed to right
            }
        } //end InsertNode()

        static void DeleteNode() //To delete a node in the tree 
        {
            if (root == null)
            {
                Console.WriteLine("BST is empty!");
                return;
            } //If BST is empty

            int val;
            Console.Write("Enter value to delete: ");

            if (!int.TryParse(Console.ReadLine(), out val))
            {
                Console.WriteLine("Could not parse value");
                return;
            }

            if (root.value.Equals(val))
            {
                root = null;
                return;
            }

            Node current = root;

            while (current != null)
            {
                if (val < current.value)
                {

                    if (current.left != null)
                    {
                        if (current.left.Equals(val))
                        {
                            current.left = null;
                            break;
                        }

                        current = current.left;
                    } //if there is a single node
                    else
                    {
                        Console.WriteLine("Value could not be found!");
                        break;
                    }
                } //if value is not present
                else
                {
                    if (current.right != null)
                    {
                        if (current.right.value.Equals(val))
                        {
                            current.right = null;
                            break;
                        } 

                        current = current.right;
                    } //if there are two nodes
                    else
                    {
                        Console.WriteLine("Value could not be found!");
                        break;
                    } //if value is not present
                }
            }
        } //end delete node()

        static void Minimum() //Looks for the least value in the tree
        {
            if (root == null)
            {
                Console.WriteLine("BST is empty!");
                return;
            }

            Node current = root;

            while (current.left != null)
            {
                current = current.left;
            }

            Console.WriteLine("Minimum value is: " + current.value.ToString());
        } //End Minimum()

        static void Maximum() //looks for greatest value in the tree
        {
            if (root == null)
            {
                Console.WriteLine("BST is empty!");
                return;
            }

            Node current = root;

            while (current.right != null)
            {
                current = current.right;
            }

            Console.WriteLine("Maximum value is: " + current.value.ToString());
        } //End MAximum()

        static void Successor() //To find next value
        {
            if (root == null)
            {
                Console.WriteLine("BST is empty!");
                return;
            }

            int val;
            Console.Write("Enter value: ");

            if (!int.TryParse(Console.ReadLine(), out val))
            {
                Console.WriteLine("Could not parse value");
                return;
            }

            Node current = root;

            while (current != null)
            {
                if (current.value.Equals(val))
                {
                    break;
                }
                else if (val < current.value)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }

            if (current == null)
            {
                Console.WriteLine($"{val} was not found in BST");
            }
            else if (current.left != null && current.right != null)
            {
                Console.WriteLine($"{val} has two successors: {current.left.value} and {current.right.value}");
            }
            else if (current.left != null)
            {
                Console.WriteLine($"{val} has one successor: {current.left.value}");
            }
            else if (current.right != null)
            {
                Console.WriteLine($"{val} has one successor: {current.right.value}");
            }
            else
            {
                Console.WriteLine($"{val} has no successors");
            }
        } //End Successor()

        static void Predecessor() //To find the value before the input value
        {
            if (root == null)
            {
                Console.WriteLine("BST is empty!");
                return;
            }

            int val;
            Console.Write("Enter value: ");

            if (!int.TryParse(Console.ReadLine(), out val))
            {
                Console.WriteLine("Could not parse value");
                return;
            }

            if (val.Equals(root.value))
            {
                Console.WriteLine("Node has no Predecessor it is the root");
                return;
            } 

            Node current = root;

            while (current != null)
            {
                if (current.left.value.Equals(val) || current.right.value.Equals(val))
                {
                    break;
                }
                else if (val < current.value)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }

            if (current != null)
            {
                Console.WriteLine($"Predecessor of {val} is {current.value}");
            }
            else
            {
                Console.WriteLine(val + " was not found in BST");
            }
        } //End Predecessor()

        static void Search() //Search for the value within the tree
        {
            if (root == null)
            {
                Console.WriteLine("BST is empty!");
                return;
            }

            int val;
            Console.Write("Enter value: ");

            if (!int.TryParse(Console.ReadLine(), out val))
            {
                Console.WriteLine("Could not parse value");
                return;
            }

            Node current = root;

            while (current != null)
            {
                if (current.value.Equals(val))
                {
                    break;
                }
                else if (val < current.value)
                {
                    current = current.left;
                }
                else
                {
                    current = current.right;
                }
            }

            if (current == null)
            {
                Console.WriteLine($"{val} was not found in BST!");
            }
            else
            {
                Console.WriteLine($"{val} has been found!");
            }
        } //End Search()

        static void PrintBST() //Prints the tree itself
        {
            if (root == null)
            {
                Console.WriteLine("BST is empty!");
                return;
            }

            Print(root);
        }

        static void Print(Node node)
        {
            if (node.left != null)
            {
                Print(node.left);
            }

            Console.WriteLine(node.value.ToString());

            if (node.right != null)
            {
                Print(node.right);
            } // Prints the values present in the tree in increasing order (first output, least)
        } //End Print()
    } //End Mainclass()
} //End program()
