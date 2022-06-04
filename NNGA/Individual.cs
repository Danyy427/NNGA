using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNGA
{
    internal class Individual
    {
        public double fitness { get; set; }
        public NeuralNetwork NN { get; set; }
        public List<ConnectionData> Genome { get; set; }
        public int GenomeLength { get; set; }

        public Individual(int length, int inputCount, int internalCount, int outputCount, bool isRandom = false, int weightRange = 4)
        {
            NN = new NeuralNetwork(inputCount, internalCount, outputCount);
            this.GenomeLength = length;
            Genome = new List<ConnectionData>(GenomeLength);
            if(isRandom == true)
            {
                for (int i = 0; i < length; i++)
                {
                    Genome.Add(RandomGene(weightRange));
                }
            }
        }

        public ConnectionData RandomGene(int weightRange = 4)
        {
            ConnectionData gene = new ConnectionData();
            gene.sourceType = Utils.NextInt(0, 2) == 0? NodeType.InputNode : NodeType.InternalNode;
            gene.destinationType = Utils.NextInt(0, 2) == 0 ? NodeType.InternalNode : NodeType.OutputNode;
            if(gene.sourceType == NodeType.InputNode)
            {
                gene.sourceIndex = Utils.NextInt(0, NN.InputNodes.Count);
            }
            else
            {
                gene.sourceIndex = Utils.NextInt(0, NN.InternalNodes.Count);
            }

            if (gene.destinationType == NodeType.InternalNode)
            {
                gene.destinationIndex = Utils.NextInt(0, NN.InternalNodes.Count);
            }
            else
            {
                gene.destinationIndex = Utils.NextInt(0, NN.OutputNodes.Count);
            }

            gene.weight = Utils.NextInt(-weightRange, weightRange);

            return gene;
        }
    }
}
