using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Accord.MachineLearning;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math.Optimization.Losses;
using System.Drawing;
using Accord.MachineLearning.DecisionTrees;
using Accord.Statistics.Kernels;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Statistics.Distributions.Univariate;
using System.Data;
using Accord.IO;
using Accord.Controls;
using Accord.MachineLearning.Bayes;
using Accord.Statistics.Filters;
using Accord.Math.Distances;
using Accord.MachineLearning.VectorMachines;
using System.Text;
using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Statistics;
using project;

namespace bitirme
{
    public partial class temp : Form
    {
        double[][] inputsForLearn;
        int[] outputsForLearn;
        double[][] testInputs;
        String testDataFile = String.Empty;
        int numberOfFeatures=0;
        int numberOfTrue=0;

        DataTable trainTable = new ExcelReader("C:\\Users\\xyz\\Desktop\\bitirme\\newDatas\\train\\trainData.xlsx").GetWorksheet("Worksheet");
        DataTable trainTableRes = new ExcelReader("C:\\Users\\xyz\\Desktop\\bitirme\\newDatas\\train\\trainResult.xlsx").GetWorksheet("Worksheet");
        DataTable testTable = new ExcelReader("C:\\Users\\xyz\\Desktop\\bitirme\\newDatas\\train\\trainData.xlsx").GetWorksheet("Worksheet");

        ImageList imgs = new ImageList() { ImageSize= new Size(10,10)};
        Image green = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\Resources\\green.png");
        Image red = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\Resources\\red.png");
        Image yellow = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\Resources\\yellow.png");

        String line;
        StreamReader file;
        
        KNearestNeighbors<double[]> knn;
        Boolean knnTrained = false;

        C45Learning teacher;
        DecisionTree tree;

        MulticlassSupportVectorMachine<Linear> svm;

        public temp(DataTable trainData)
        {
            InitializeComponent();
        }

        private void readyTestFile() {
            Normalization normalizationTest = new Normalization(testTable);
            DataTable result2 = normalizationTest.Apply(testTable);
            double[][] Testinputs = testTable.ToJagged<double>();
            testInputs = Testinputs;
        }

        private void readyDatas() {

            //Normalization normalizationOutput = new Normalization(trainTableRes);
            //DataTable resultOutput = normalizationOutput.Apply(trainTableRes);

            int[] outputs = trainTableRes.Columns["Scores"].ToArray<int>();
           // trainTableRes.Columns.
            //outputsForLearn = new int[outputs.Length];
            int i = 0;
            foreach (int index in outputs) {
                outputsForLearn[i] = scoreToLevel(index);
                i++;
            }

            //Normalization normalization = new Normalization(trainTable);

            numberOfTrue = 0;
            
            //double mean = normalization["Score"].Mean;
            //double sdev = normalization["Score"].StandardDeviation;

            //DataTable result = normalization.Apply(trainTable);
            numberOfFeatures = trainTable.Columns.Count;
            double[][] inputs = trainTable.ToJagged<double>();
            //inputs = Accord.Statistics.Tools.Standardize(inputs);
            inputsForLearn = inputs;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Results.Columns.Add("Results",250);
            Inputs.Columns.Add("FileContent",400);
            listView2.Columns.Add("History", 250);
        }

        private int scoreToLevel(int score) {
            if (score < 300)
                return 0;
            if (score >= 300 && score < 700)
                return 1;
            if (score >= 700)
                return 2;
            else
                return -1;
        }


        /*
         * KNN 
         * */
        private void kNearestNeighborsTrain(){
            readyDatas();
            try {
                knn = new KNearestNeighbors<double[]>(k: 3, distance: new SquareEuclidean());
                knn.Learn(inputsForLearn, outputsForLearn);
                knnTrained = true;
                MessageBox.Show("KNearestNeighbors algorithm is trained");
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }
        private void kNearestNeighborsResult() {
           // double[][] scores = knn.Scores(testInputs);
            try
            {
                int index = 0;
                foreach (double[] newPoint in testInputs)
                {
                    
                    int answer = knn.Decide(newPoint);

                    if (answer == outputsForLearn[index]) numberOfTrue++;

                    if (answer == 0)
                    {
                        imgs.Images.Add(red);
                    }
                    if (answer == 1)
                    {
                        imgs.Images.Add(yellow);
                    }
                    if (answer == 2)
                    {
                        imgs.Images.Add(green);
                    }
                    Results.Items.Add($"asnwer :{answer}", index);
                    index++;
                }
                Results.SmallImageList = imgs;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void kNearestNeighborsTestSingle() {

        }
        /**
         * C45
         */
        private void c45Train() {
            readyDatas();
      
            try
            {
                teacher = new C45Learning();

            

                //tree = teacher.Learn(inputsForLearn, outputsForLearn);
                MessageBox.Show("C4.5 algorithm is trained");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + e.StackTrace);
            }

        }
        private void c45Result() {
            // To get the estimated class labels, we can use
            int[] predicted = tree.Decide(testInputs);

            // The classification error (0.0266) can be computed as 
            double error = new ZeroOneLoss(outputsForLearn).Loss(predicted);
            int index = 0;
            try
            {
                foreach (int answer in predicted)
                {

                    if (answer == outputsForLearn[index]) numberOfTrue++;

                    if (answer == 0)
                    {
                        imgs.Images.Add(red);
                    }
                    if (answer == 1)
                    {
                        imgs.Images.Add(yellow);
                    }
                    if (answer == 2)
                    {
                        imgs.Images.Add(green);
                    }
                    Results.Items.Add($"asnwer :{answer}", index);
                    index++;
                }

                Results.SmallImageList = imgs;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void c45TestSingle() {

        }
        /*
         * VectorMachine
         * **/
        private void vectorMachineTrain(){
            readyDatas();
            var teacher = new MulticlassSupportVectorLearning<Linear>()
            {
                Learner = (p) => new SequentialMinimalOptimization<Linear>()
                {
                    Complexity = 10.0 // Create a hard SVM
                }
            };

            svm = teacher.Learn(inputsForLearn, outputsForLearn);

            MessageBox.Show("SupportVectorMachine algorithm is trained");
          
        }
        private void vectorMachineResult(){
            // Obtain class predictions for each sample
            int[] predicted = svm.Decide(testInputs);
            int index = 0;
            try
            {
                foreach (int answer in predicted)
                {
                    if (answer == outputsForLearn[index]) numberOfTrue++;

                    if (answer == 0)
                    {
                        imgs.Images.Add(red);
                    }
                    if (answer == 1)
                    {
                        imgs.Images.Add(yellow);
                    }
                    if (answer == 2)
                    {
                        imgs.Images.Add(green);
                    }
                    Results.Items.Add($"asnwer :{answer}", index);
                    index++;
                }

                Results.SmallImageList = imgs;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void vectorMachineSingle(){

        }

        private void pictureBoxLogo_Click(object sender, EventArgs e)
        {

        }
        private void printToOpenFile(String filename) {
            line = String.Empty;
            file = new StreamReader(filename);
            while ((line = file.ReadLine()) != null)
            {
                Inputs.Items.Add(line);
            }

        }
        private void openFileForTest_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog(this);
            /*
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                testDataFile = ofd.FileName;
                printToOpenFile(ofd.FileName);
            }*/
        }
        private void TestDataSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void Result_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void openFileForTrain(object sender, EventArgs e)
        {
            try {
                if (Algos.SelectedItem == "KNN")
                {
                    kNearestNeighborsTrain();
                }
                else if (Algos.SelectedItem == "C4.5")
                {
                    c45Train();
                }       
                else if (Algos.SelectedItem == "SVM")
                {
                    vectorMachineTrain();
                }
                else {
                    MessageBox.Show("No choosen algorithm");
                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }
        private void testDataStart_Click(object sender, EventArgs e)
        {
            if (Algos.SelectedItem == "KNN")
            {
                if (knnTrained.Equals(true)) {
                    readyTestFile();
                    kNearestNeighborsResult();
                    saveDatas("KNN");
                }
            }
            else if (Algos.SelectedItem == "C4.5")
            {
                readyTestFile();
                c45Result();
                saveDatas("C4.5");
            }
            else if (Algos.SelectedItem == "SVM")
            {
                readyTestFile();
                vectorMachineResult();
                saveDatas("SVM");
            }
            else
                MessageBox.Show("Please choose algorithm for start");
        }
        private void history() {
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(10, 10);
            
            Image green = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\resources\\green.png");
            Image red = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\resources\\red.png");
            Image yellow = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\resources\\yellow.png");
           /* 
            try
            {
                foreach (String path in p) {

                    imgs.Images.Add(Image.FromFile(path));
                }
            } catch (Exception e){
                MessageBox.Show(e.Message);
            }

            listView4.SmallImageList = imgs;
            listView4.Items.Add("adada",5);
             */  

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){}

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Comboboxta seçildiğinde visiblity özelliği çalışmıyor.
            if (Algos.SelectedItem == "KNN")
            {
                panel4.Visible = true;
                panel2.Visible = false;
                svmPanel.Visible = false;
            }
            else if (Algos.SelectedItem == "C4.5")
            {
                panel4.Visible = false;
                panel2.Visible = true;
                svmPanel.Visible = false;
            }
            else if (Algos.SelectedItem == "SVM")
            {
                panel4.Visible = false;
                panel2.Visible = false;
                svmPanel.Visible = true;
            }
        }

        private void label1_Click(object sender, EventArgs e) {
        }
        private void panel1_Paint(object sender, PaintEventArgs e){
        }
        private void CDortBes_Paint(object sender, PaintEventArgs e)  {
     }
        private void saveDatas(String model) {
            StringBuilder result = new StringBuilder(250);
            if (model == "KNN") {
                listView2.Items.Add("=========================================");
                listView2.Items.Add($"Model:{model} Type:DataSet DataNumber:{500}");
                listView2.Items.Add($"NumberFeatures:{numberOfFeatures} Predicted:{numberOfTrue}");
                listView2.Items.Add("=========================================");
                }
            else if (model == "C4.5"){
                listView2.Items.Add("=========================================");
                listView2.Items.Add($"Model:{model} Type:DataSet DataNumber:{500}");
                listView2.Items.Add($"NumberFeatures:{numberOfFeatures} Predicted:{numberOfTrue}");
                listView2.Items.Add("=========================================");
            }
            else if (model == "SVM"){
                listView2.Items.Add("=========================================");
                listView2.Items.Add($"Model:{model} Type:DataSet DataNumber:{500}");
                listView2.Items.Add($"NumberFeatures:{numberOfFeatures} Predicted:{numberOfTrue}");
                listView2.Items.Add("=========================================");
            }
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)   {
            
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)   {
           
        }
        private void listView4_SelectedIndexChanged(object sender, EventArgs e)    {
            Results.Items.Clear();
        }

        private void KNN_Paint(object sender, PaintEventArgs e)
        {

        }

        private void singleTest(object sender, EventArgs e) {
            
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void CDortBes_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
