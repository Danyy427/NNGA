using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNGA
{
    internal enum NodeType
    {
        InternalNode = 0,
        OutputNode = 1,
        InputNode = 2,
        Reserved = 3
    }

    internal class Node
    {
        private NodeType _nodeType;
        List<Connection> _incoming = new List<Connection>();
        List<Connection> _outgoing = new List<Connection>();

        public NodeType NodeType => _nodeType;
        public double Value { get; set; }
        public double Bias { get; set; }
        public List<Connection> Outgoing => _outgoing;
        public List<Connection> Incoming => _incoming;

        public Node(NodeType type, bool isRandom = false, int range = 1)
        {
            _nodeType = type;
            if (isRandom == false)
            {
                Bias = 0.0;
            }
            else
            {
                RandomizeBias(range);
            }

        }

        public Node(NodeType type, double Bias)
        {
            _nodeType = type;
            this.Bias = Bias;
        }

        public void RandomizeBias(int minMax = 1)
        {
            Bias = Utils.NextDouble(-minMax, minMax);
        }

        public void Connect(Node other, bool isRandom = false, int range = 4)
        {
            if (NodeType == NodeType.OutputNode)
            {
                throw new Exception("Output nodes cannot have outgoing connections, their results are absolute.");
            }

            if (other.NodeType == NodeType.InputNode)
            {
                throw new Exception("Input nodes cannot have incoming connections, their values are absolute.");
            }

            Connection connection = new Connection(this, other, isRandom, range);
            Outgoing.Add(connection);
            other.Incoming.Add(connection);
        }

        public void Connect(Node other, double weight)
        {
            if (NodeType == NodeType.OutputNode)
            {
                throw new Exception("Output nodes cannot have outgoing connections, their results are absolute.");
            }

            if (other.NodeType == NodeType.InputNode)
            {
                throw new Exception("Input nodes cannot have incoming connections, their values are absolute.");
            }

            Connection connection = new Connection(this, other, weight);
            Outgoing.Add(connection);
            other.Incoming.Add(connection);
        }

        public void ConnectRandomWeight(Node other, int range = 4)
        {
            if (NodeType == NodeType.OutputNode)
            {
                throw new Exception("Output nodes cannot have outgoing connections, their results are absolute.");
            }

            if (other.NodeType == NodeType.InputNode)
            {
                throw new Exception("Input nodes cannot have incoming connections, their values are absolute.");
            }

            Connection connection = new Connection(this, other, true, range);
            Outgoing.Add(connection);
            other.Incoming.Add(connection);
        }

        public void Input(double input)
        {
            if (NodeType != NodeType.InputNode)
            {
                throw new Exception("Only input type nodes can be inputted values.");
            }
            if (input > 1 || input < 0)
            {
                throw new ArgumentException("Input value must be between 0 and 1");
            }
            Value = input;
        }

        public void CalculateValue()
        {

            if(NodeType == NodeType.InputNode)
            {
                throw new Exception("The values of input nodes cannot be calculated, the values are inputted using Node.Input(). They are absolute.");
            }

            double weightedSum = 0.0;
            foreach (var connection in Incoming)
            {
                weightedSum += connection.Weight * connection.NodePair.From.Value;
            }
            
            Value = Utils.Tanh(weightedSum + Bias);
        }

        public double Output()
        {
            if (NodeType != NodeType.OutputNode)
            {
                throw new Exception("Only output type nodes can output values in a possibility range.");
            }
            return Value;
        }

        public void ClearConnections()
        {
            Outgoing.Clear();
            Incoming.Clear();
        }
    }

    internal class NodePair
    {
        public Node To { get; set; }
        public Node From { get; set; }

        public NodePair(Node node1, Node node2)
        {
            From = node1;
            To = node2;
        }
    }

    internal class Connection
    {
        public double Weight { get; set; }
        private NodePair _nodePair;
        public NodePair NodePair => _nodePair;

        public Connection(Node node1, bool isRandom = false, int range = 4)
        {
            if (isRandom == false)
            {
                Weight = 0.0;
            }
            else
            {
                RandomizeWeight(range);
            }
            _nodePair = new NodePair(node1, node1);
        }

        public Connection(Node node1, double weight)
        {
            Weight = weight;
            _nodePair = new NodePair(node1, node1);
        }

        public Connection(Node node1, Node node2, bool isRandom = false, int range = 4)
        {
            if (isRandom == false)
            {
                Weight = 0.0;
            }
            else
            {
                RandomizeWeight(range);
            }
            _nodePair = new NodePair(node1, node2);
        }

        public Connection(Node node1, Node node2, double weight)
        {
            Weight = weight;
            _nodePair = new NodePair(node1, node2);
        }

        public void RandomizeWeight(int range)
        {
            Weight = Utils.NextDouble(-range, range);
        }
    }
}
