using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNGA
{
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
            from.Connect(to, true, range);
        }

        public void MakeConnectionByNodeRef(ref Node from, ref Node to, double weight)
        {
            from.Connect(to, weight);
        }

        public void MakeConnectionByNodeRef(ref Node from, ref Node to, bool isRandom = true, int range = 4)
        {
            from.Connect(to, isRandom, range);
        }

    }
}
    