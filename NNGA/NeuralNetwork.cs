using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNGA
{
    internal class NodeData
    {
        public int index { get; set; }
        public NodeType type { get; set; }
    }

    internal class ConnectionData
    {
        public int sourceIndex { get; set; }
        public NodeType sourceType { get; set; }

        public int destinationIndex { get; set; }
        public NodeType destinationType { get; set; }

        public double weight { get; set; }
    }

    internal class NeuralNetwork
    {
        private List<Node> _inputNodes;
        private List<Node> _internalNodes;
        private List<Node> _outputNodes;

        public List<Node> InputNodes => _inputNodes;
        public List<Node> InternalNodes => _internalNodes;
        public List<Node> OutputNodes => _outputNodes;

        public NeuralNetwork(int inputCount, int internalCount, int outputCount)
        {
            _inputNodes = new List<Node>(inputCount);
            _internalNodes = new List<Node>(internalCount);
            _outputNodes = new List<Node>(outputCount);

            for (int i = 0; i < inputCount; i++)
            {
                _inputNodes[i] = new Node(NodeType.InputNode);
            }
            for (int i = 0; i < internalCount; i++)
            {
                _internalNodes[i] = new Node(NodeType.InternalNode);
            }
            for (int i = 0; i < outputCount; i++)
            {
                _outputNodes[i] = new Node(NodeType.OutputNode);
            }
        }

        public void Input(int node, double input)
        {
            _inputNodes[node].Input(input);
        }

        public void InputArray(double[] input)
        {
            for (int i = 0; i < _inputNodes.Count; i++)
            {
                _inputNodes[i].Input(input[i]);
            }
        }

        public void InputList(List<double> input)
        {
            for (int i = 0; i < _inputNodes.Count; i++)
            {
                _inputNodes[i].Input(input[i]);
            }
        }

        public double Output(int node)
        {
            return _outputNodes[node].Output();
        }

        public double[] OutputArray()
        {
            double[] outputs = new double[_outputNodes.Count];
            for (int i = 0; i < _inputNodes.Count; i++)
            {
                outputs[i] = _outputNodes[i].Output();
            }
            return outputs;
        }

        public List<double> OutputList() => OutputArray().ToList();

        public void FeedForward()
        {
            foreach (Node node in _internalNodes)
            {
                if(node.Outgoing.Count != 0)
                {
                    node.CalculateValue();
                }
            }

            foreach (Node node in _outputNodes)
            {
                if (node.Outgoing.Count != 0)
                {
                    node.CalculateValue();
                }
            }
        }   

        public void MakeConnectionByNodeRefRandom(ref Node from, ref Node to, int range = 4)
        {
            if((!_internalNodes.Contains(from) || !_inputNodes.Contains(from)) && 
                (!_internalNodes.Contains(to) || !_outputNodes.Contains(to)))
            {
                throw new Exception("Invalid Connection. The nodes are not a part of the neural network");
            }
            from.Connect(to, true, range);
        }

        public void MakeConnectionByNodeRef(ref Node from, ref Node to, double weight)
        {
            if ((!_internalNodes.Contains(from) || !_inputNodes.Contains(from)) &&
                (!_internalNodes.Contains(to) || !_outputNodes.Contains(to)))
            {
                throw new Exception("Invalid Connection. The nodes are not a part of the neural network");
            }
            from.Connect(to, weight);
        }

        public void MakeConnectionByNodeRef(ref Node from, ref Node to, bool isRandom = true, int range = 4)
        {
            if ((!_internalNodes.Contains(from) || !_inputNodes.Contains(from)) &&
                (!_internalNodes.Contains(to) || !_outputNodes.Contains(to)))
            {
                throw new Exception("Invalid Connection. The nodes are not a part of the neural network");
            }
            from.Connect(to, isRandom, range);
        }

        public void MakeConnection(NodeType sourceType, int sourceIndex, NodeType destType, int destination, double weight)
        {
            if(sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], weight);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], weight);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], weight);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], weight);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnection(NodeType sourceType, int sourceIndex, NodeType destType, int destination, bool isRandom = true, int range = 4)
        {
            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], isRandom, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], isRandom, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], isRandom, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], isRandom, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnectionRandom(NodeType sourceType, int sourceIndex, NodeType destType, int destination, int range)
        {
            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], true, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], true, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], true, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], true, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnectionByNodeData(NodeData node1, NodeData node2, double weight)
        {
            var sourceType = node1.type;
            var destType = node2.type;
            var sourceIndex = node1.index;
            var destination = node2.index;
            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], weight);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], weight);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], weight);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], weight);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnectionByNodeData(NodeData node1, NodeData node2, bool isRandom = true, int range = 4)
        {
            var sourceType = node1.type;
            var destType = node2.type;
            var sourceIndex = node1.index;
            var destination = node2.index;

            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], isRandom, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], isRandom, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], isRandom, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], isRandom, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnectionByNodeData(NodeData node1, NodeData node2, int range)
        {
            var sourceType = node1.type;
            var destType = node2.type;
            var sourceIndex = node1.index;
            var destination = node2.index;

            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], true, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], true, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], true, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], true, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnectionByConnectionData(ConnectionData connectionData)
        {
            var sourceType = connectionData.sourceType;
            var destType = connectionData.destinationType;
            var sourceIndex = connectionData.sourceIndex;
            var destination = connectionData.destinationIndex;
            var weight = connectionData.weight;

            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], weight);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], weight);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], weight);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], weight);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnectionByConnectionData(ConnectionData connectionData, bool isRandom = true, int range = 4)
        {
            var sourceType = connectionData.sourceType;
            var destType = connectionData.destinationType;
            var sourceIndex = connectionData.sourceIndex;
            var destination = connectionData.destinationIndex;
            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], isRandom, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], isRandom, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], isRandom, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], isRandom, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }

        public void MakeConnectionByConnectionDataRandom(ConnectionData connectionData, int range)
        {
            var sourceType = connectionData.sourceType;
            var destType = connectionData.destinationType;
            var sourceIndex = connectionData.sourceIndex;
            var destination = connectionData.destinationIndex;
            if (sourceType == NodeType.InputNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _inputNodes[sourceIndex].Connect(_internalNodes[destination], true, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _inputNodes[sourceIndex].Connect(_outputNodes[destination], true, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else if (sourceType == NodeType.InternalNode)
            {
                if (destType == NodeType.InternalNode)
                {
                    _internalNodes[sourceIndex].Connect(_internalNodes[destination], true, range);
                }
                else if (destType == NodeType.OutputNode)
                {
                    _internalNodes[sourceIndex].Connect(_outputNodes[destination], true, range);
                }
                else
                {
                    throw new Exception("The destination cannot be an input node");
                }
            }
            else
            {
                throw new Exception("The source cannot be an output node");
            }
        }
    }
}
    