namespace NNGA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Individual individual = new Individual(5, 2, 2, 2, true);

            int i = 0;
            foreach (var gene in individual.Genome)
            {
                textBox2.Text += $"Connection {i++}:{Environment.NewLine}";
                textBox2.Text += $"source: {gene.sourceIndex}{Environment.NewLine}"; 
                textBox2.Text += $"destination: {gene.destinationIndex}{Environment.NewLine}";
                textBox2.Text += $"source type: {gene.sourceType}{Environment.NewLine}";
                textBox2.Text += $"destination type: {gene.destinationType}{Environment.NewLine}";
                textBox2.Text += $"weight: {gene.weight}{Environment.NewLine}";
            }
            foreach (var node in individual.NN.InputNodes)
            {
                textBox1.Text += $"Node {i++}:{Environment.NewLine}";
                textBox1.Text += $"value: {node.Value}{Environment.NewLine}";
            }
            foreach (var node in individual.NN.InternalNodes)
            {
                textBox1.Text += $"Node {i++}:{Environment.NewLine}";
                textBox1.Text += $"value: {node.Value}{Environment.NewLine}";
            }
            foreach (var node in individual.NN.OutputNodes)
            {
                textBox1.Text += $"Node {i++}:{Environment.NewLine}";
                textBox1.Text += $"value: {node.Value}{Environment.NewLine}";
            }
            individual.NN.Input(0, 0.5);
            individual.NN.Input(1, 0.8);
            individual.NN.FeedForward();
            i = 0;
            foreach (var node in individual.NN.InputNodes)
            {
                textBox1.Text += $"Node {i++}:{Environment.NewLine}";
                textBox1.Text += $"value: {node.Value}{Environment.NewLine}";
            }
            foreach (var node in individual.NN.InternalNodes)
            {
                textBox1.Text += $"Node {i++}:{Environment.NewLine}";
                textBox1.Text += $"value: {node.Value}{Environment.NewLine}";
            }
            foreach (var node in individual.NN.OutputNodes)
            {
                textBox1.Text += $"Node {i++}:{Environment.NewLine}";
                textBox1.Text += $"value: {node.Value}{Environment.NewLine}";
            }

        }
    }
}