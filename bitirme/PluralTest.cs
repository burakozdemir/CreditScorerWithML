using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;
using System;
using Accord.Math;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project;
using Accord.MachineLearning;
using Accord.Math.Distances;
using System.Windows.Forms.DataVisualization.Charting;
using Accord.IO;
using System.IO;
using Accord.Math.Optimization.Losses;
using Accord.Statistics;
using Accord.Controls;
using Accord.Statistics.Analysis;

namespace bitirme
{
    public partial class PluralTest : Form
    {
        MainForm mainForm;
        DataGridView datas;
        String algo;
        int numberClass = 2;
        int[] visuData;
        Dictionary<int, int> set = new Dictionary<int, int>();

        string[] columns;

        public PluralTest(DataGridView data, MainForm main, String algo)
        {
            this.datas = data;
            this.mainForm = main;
            this.algo = algo;
            this.visuData = new int[numberClass];
            InitializeComponent();
            Variables.setImages();
            setColumns();
            openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "Resources");
            readyTestDatas();
        }

        public void readyTest(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                string extension = Path.GetExtension(filename);
                if (extension == ".xls" || extension == ".xlsx")
                {
                    ExcelReader db = new ExcelReader(filename, true, false);
                    TableSelectDialog t = new TableSelectDialog(db.GetWorksheetList());

                    if (t.ShowDialog(this) == DialogResult.OK)
                    {
                        project.Variables.testDataTable = db.GetWorksheet(t.Selection);
                        //project.Variables.testDataGridView.DataSource = Variables.trainDataTable;
                        //trainData.Columns.Add(new DataColumn(new Button()));
                        //readyTestDatas();
                    }
                }
            }
        }

        private void readyTestDatas()
        {
            // Variables.allDataCount = Variables.trainDataGridView.ColumnCount;
            Variables.testInputs = Variables.testDataTable.ToJagged<double>();
            Variables.testOutputs = Variables.testResults.Columns["NihaiKarar"].ToArray<int>();
            Variables.testDataCount = Variables.testOutputs.Length;


        }

        private void visualization_Click(object sender, EventArgs e)
        {
            if (testResult.Rows.Count != 0)
            {
                new Visualization(numberClass, visuData, set, algo).ShowDialog(this);
            }
            else {
                MessageBox.Show("Please click to Start Button");
            }
        }

        private void startTest(object sender, EventArgs e) {
            if (this.algo == "KNN")
            {
                knnTest();
            }
            else if (this.algo == "C4.5")
            {
                c45Test();
            }
            else if (this.algo == "SVM")
            {
                svmTest();
            }
            else if (this.algo == "Kmeans")
            {
                kMeansTest();
            }
            else if (this.algo == "GMM")
            {
                gmmTest();
            }
            else if (this.algo == "MeanShift")
            {
                meanShiftTest();
            }
            else if (this.algo == "NaiveBayes")
            {
                naiveBayesTest();
            }
            else if (this.algo == "BinarySplit")
            {
                binarySplitTest();
            }
            else if (this.algo == "RandomForest")
            {
                randomForestTest();
            }
            else
            {
                MessageBox.Show("Please select algorithm ");
                return;
            }
        }

        private String getData(int indis) {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Variables.testInputs[indis].Length; i++) {
                result.Append(Variables.testInputs[indis][i]);
                result.Append(";");
            }

            return result.ToString();
        }

        private void setColumns() {
            if (algo == "Kmeans" || algo == "GMM" || algo == "MeanShift" || algo == "BinarySplit")
            {
                testResult.Columns["Predicted"].Visible = false;
                testResult.Columns["Clusters"].Visible = true;
                testResult.Columns["Result"].Visible = false;
            }
            else {
                //testResult.Columns["Predicted"].Visible = true;
                testResult.Columns["Clusters"].Visible = false;
            }

        }

        //KNN test
        private void knnTest()
        {
            try
            {
                int index = 0;
                Variables.predictedDataCount = 0;
                testResult.Rows.Clear();

                //bool[][] expected = Classes.Decide(Variables.testInputs);
                //bool[]output = Variables.knn.Decide()

                foreach (double[] newP in Variables.testInputs)
                {

                    int answer = Variables.knn.Decide(newP);

                    if (answer == Variables.testOutputs[index]) Variables.predictedDataCount++;

                    Image resultImage;
                    if (Variables.testOutputs[index] == 1)
                        resultImage = Variables.green;
                    else
                        resultImage = Variables.red;

                    if (answer == 0)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.red, answer);
                    if (answer == 1)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.green, answer);

                    index++;
                }
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.knn, Variables.trainInputs, Variables.trainOutputs);
                visuData[0] = Variables.predictedDataCount;
                visuData[1] = Variables.testDataCount - Variables.predictedDataCount;
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} Predicted:{Variables.predictedDataCount}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        //RandomForest
        private void randomForestTest()
        {
            try
            {
                int index = 0;
                Variables.predictedDataCount = 0;
                testResult.Rows.Clear();

                foreach (double[] newP in Variables.testInputs)
                {

                    int answer = Variables.randomForest.Decide(newP);

                    if (answer == Variables.testOutputs[index]) Variables.predictedDataCount++;

                    Image resultImage;
                    if (Variables.testOutputs[index] == 1)
                        resultImage = Variables.green;
                    else
                        resultImage = Variables.red;

                    if (answer == 0)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.red, answer);
                    if (answer == 1)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.green, answer);

                    index++;
                }
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.randomForest, Variables.trainInputs, Variables.trainOutputs);
                visuData[0] = Variables.predictedDataCount;
                visuData[1] = Variables.testDataCount - Variables.predictedDataCount;
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} Predicted:{Variables.predictedDataCount}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //C45 test(Decision Tree)
        private void c45Test() {
            if (Variables.tree == null) {
                MessageBox.Show("Please create a machine first.");
                return;
            }
            int index = 0;
            Variables.predictedDataCount = 0;

            // To get the estimated class labels, we can use
            int[] predicted = Variables.tree.Decide(Variables.testInputs);
            testResult.Rows.Clear();

            // The classification error (0.0266) can be computed as 
            double error = new ZeroOneLoss(Variables.trainOutputs).Loss(predicted);

            try
            {
                foreach (int answer in predicted)
                {

                    if (answer == Variables.testOutputs[index]) Variables.predictedDataCount++;
                    Image resultImage;

                    if (Variables.testOutputs[index] == 1)
                        resultImage = Variables.green;
                    else
                        resultImage = Variables.red;

                    if (answer == 0) {
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.red, answer);
                    }
                    else if (answer == 1) {
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.green, answer);
                    }
                    else {
                        testResult.Rows.Add(index, "hesaplanamadi");
                    }
                    index++;
                }
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.tree, Variables.trainInputs, Variables.trainOutputs);
                visuData[0] = Variables.predictedDataCount;
                visuData[1] = Variables.testDataCount - Variables.predictedDataCount;
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} Predicted:{Variables.predictedDataCount}");
                mainForm.listView1.Items.Add("=========================================");

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        //Classification SVM
        private void svmTest() {
            if (Variables.svm == null)
            {
                MessageBox.Show("Please create a machine first.");
                return;
            }
            try {

                int index = 0;
                Variables.predictedDataCount = 0;
                testResult.Rows.Clear();

                bool[] output = Variables.svm.Decide(Variables.testInputs);
                int[] answers = output.ToZeroOne();


                foreach (int res in answers)
                {
                    if (res == Variables.testOutputs[index]) Variables.predictedDataCount++;
                    Image resultImage;

                    if (Variables.testOutputs[index] == 1)
                        resultImage = Variables.green;
                    else
                        resultImage = Variables.red;

                    if (res == 0)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.red, res);
                    else if (res == 1)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.green, res);
                    index++;
                }
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.svm, Variables.trainInputs, Variables.trainOutputs);
                visuData[0] = Variables.predictedDataCount;
                visuData[1] = Variables.testDataCount - Variables.predictedDataCount;
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} Predicted:{Variables.predictedDataCount}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        //NaiveBayes
        private void naiveBayesTest()
        {
            if (Variables.bayes == null)
            {
                MessageBox.Show("Please train naivebayes algorithm");
                return;
            }
            try {

                int index = 0;
                Variables.predictedDataCount = 0;

                int[] predicted = Variables.bayes.Decide(Variables.testInputs);
                foreach (int res in predicted)
                {
                    if (res == Variables.testOutputs[index]) Variables.predictedDataCount++;

                    Image resultImage;

                    if (Variables.testOutputs[index] == 1)
                        resultImage = Variables.green;
                    else
                        resultImage = Variables.red;

                    if (res == 0)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.red);
                    if (res == 1)
                        testResult.Rows.Add(index, getData(index), resultImage, Variables.green);

                    index++;
                }
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.bayes, Variables.trainInputs, Variables.trainOutputs);
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} Predicted:{Variables.predictedDataCount}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        //KmeansTest
        private void kMeansTest()
        {
            if (Variables.kmeans == null)
            {
                MessageBox.Show("Please create a machine first.");
                return;
            }
            try
            {

                int index = 0;
                Variables.predictedDataCount = 0;
                this.numberClass = Variables.kMeansK;
                this.visuData = new int[Variables.kMeansK];
                this.set.Clear();

                int[] classification = Variables.kMeansCluster.Decide(Variables.testInputs);
                //int[] classification = Variables.kmeans.Compute(Variables.testInputs);

                Dictionary<int, int> set = new Dictionary<int, int>();


                foreach (int res in classification)
                {
                    if (!set.ContainsKey(res))
                    {
                        set.Add(res, 1);
                    }
                    else
                    {
                        set[res] += 1;
                    }

                    Image resultImage = Variables.green;

                    testResult.Rows.Add(index, getData(index), resultImage, resultImage, res);

                    index++;
                }
                this.set = set;
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.kMeansCluster, Variables.trainInputs, Variables.trainOutputs);
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} ClusterNum:{Variables.kMeansK}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        //gmm
        private void gmmTest()
        {
            if (Variables.gaussianCluster == null)
            {
                MessageBox.Show("Please create a machine first.");
                return;
            }
            try
            {
                readyTestDatas();//burda kaldınnnn
                int index = 0;
                Variables.predictedDataCount = 0;
                this.numberClass = Variables.gmmClusterNumber;
                this.visuData = new int[Variables.gmmClusterNumber];
                this.set.Clear();

                int[] classification = Variables.gaussianCluster.Decide(Variables.testInputs);

                Dictionary<int, int> set = new Dictionary<int, int>();

                foreach (int res in classification)
                {
                    if (!set.ContainsKey(res))
                    {
                        set.Add(res, 1);
                    }
                    else
                    {
                        set[res] += 1;
                    }

                    Image resultImage = Variables.green;

                    testResult.Rows.Add(index, getData(index), resultImage, resultImage, res);

                    index++;
                }
                this.set = set;
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.gaussianCluster, Variables.trainInputs, Variables.trainOutputs);
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} ClusterNum:{Variables.gmmClusterNumber}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //MeanShift
        private void meanShiftTest()
        {
            if (Variables.meanShiftCluster == null)
            {
                MessageBox.Show("Please create a machine first.");
                return;
            }
            try
            {

                int index = 0;
                Variables.predictedDataCount = 0;
                this.numberClass = Variables.meanShiftK;
                this.visuData = new int[Variables.meanShiftK];
                this.set.Clear();

                int[] predicted = Variables.meanShiftCluster.Decide(Variables.testInputs);
                Dictionary<int, int> set = new Dictionary<int, int>();

                foreach (int res in predicted)
                {
                    if (!set.ContainsKey(res))
                    {
                        set.Add(res, 1);
                    }
                    else
                    {
                        set[res] += 1;
                    }
                    Image resultImage = Variables.green;

                    testResult.Rows.Add(index, getData(index), resultImage, resultImage, res);

                    index++;
                }
                this.set = set;
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.meanShiftCluster, Variables.trainInputs, Variables.trainOutputs);
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} ClusterNum:{Variables.meanShiftK}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        //NaiveBayes
        private void binarySplitTest()
        {
            try
            {

                int index = 0;
                Variables.predictedDataCount = 0;
                this.numberClass = Variables.binarySplitK;
                this.visuData = new int[Variables.binarySplitK];
                this.set.Clear();

                int[] predicted = Variables.kMeansCluster.Decide(Variables.testInputs);
                Dictionary<int, int> set = new Dictionary<int, int>();

                foreach (int res in predicted)
                {
                    if (!set.ContainsKey(res))
                    {
                        set.Add(res, 1);
                    }
                    else
                    {
                        set[res] += 1;
                    }
                    Image resultImage = Variables.green;

                    testResult.Rows.Add(index, getData(index), resultImage, resultImage, res);

                    index++;
                }
                this.set = set;
                Variables.cm = GeneralConfusionMatrix.Estimate(Variables.kMeansCluster, Variables.trainInputs, Variables.trainOutputs);
                mainForm.listView1.Items.Add("=========================================");
                mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem} TestType:{mainForm.testTypeComboBox.SelectedItem}");
                mainForm.listView1.Items.Add($"DataNumber:{Variables.testDataCount}");
                mainForm.listView1.Items.Add($"NumOfFea:{datas.ColumnCount - 1} ClusterNum:{Variables.binarySplitK}");
                mainForm.listView1.Items.Add("=========================================");
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }

        }
    }
}
