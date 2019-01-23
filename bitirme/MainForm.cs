using Accord.IO;
using Accord.Math;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project;

namespace bitirme
{
    public partial class MainForm : Form
    {
        public String selectedAlgo;
        public MainForm()
        {
            InitializeComponent();
            Variables.mainForm = this;
            openFileDialog1.InitialDirectory = Path.Combine(Application.StartupPath, "Resources");
            //Console.WriteLine(Path.Combine(Application.StartupPath, "Resources"));
        }

        public void fillTrainTable(object sender, EventArgs e) {

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
                        project.Variables.trainDataTable = db.GetWorksheet(t.Selection);
                        this.trainDataGridView.DataSource = Variables.trainDataTable;
                        Variables.trainDataGridView = this.trainDataGridView;
                        readyTrainDatas();
                        //trainData.Columns.Add(new DataColumn(new Button()));
                    }
                }
            }
        }
        
        public void trainAlgorithm(object sender, EventArgs e)
        {
            if (trainDataGridView.DataSource == null)
            {
                MessageBox.Show("Please load some data first.");
                return;
            }
            else if (modelComboBox.SelectedItem == "KNN")
            {
                Variables.knnTrain();
                this.selectedAlgo = "KNN";
            }
            else if (modelComboBox.SelectedItem == "C4.5")
            {
                Variables.c45Train();
                this.selectedAlgo = "C4.5";
            }
            else if (modelComboBox.SelectedItem == "SVM")
            {
                Variables.svmTrain();
                this.selectedAlgo = "SVM";
            }
            else if (modelComboBox.SelectedItem == "Kmeans")
            {
                Variables.kMeansTrain();
                this.selectedAlgo = "Kmeans";
            }
            else if (modelComboBox.SelectedItem == "GMM")
            {
                Variables.gmmTrain();
                this.selectedAlgo = "GMM";
            }
            else if (modelComboBox.SelectedItem == "MeanShift")
            {
                Variables.meanShiftTrain();
                this.selectedAlgo = "MeanShift";
            }
            else if (modelComboBox.SelectedItem == "NaiveBayes")
            {
                Variables.naiveBayesTrain();
                this.selectedAlgo = "NaiveBayes";
            }
            else if (modelComboBox.SelectedItem == "BinarySplit")
            {
                Variables.binarySplitTrain();
                this.selectedAlgo = "BinarySplit";
            }
            else if (modelComboBox.SelectedItem == "RandomForest")
            {
                Variables.randomForestTrain();
                this.selectedAlgo = "RandomForest";
            }
            else {
                MessageBox.Show("Please select algorithm ");
                return;
            }
        }

        private void readyTrainDatas() {

           // Variables.allDataCount=Variables.trainDataGridView.ColumnCount;

            // Creates a matrix from the entire source data table
            double[][] data = (Variables.trainDataGridView.DataSource as DataTable).ToJagged(out Variables.columnNames);
            Variables.featuresNumber = data[0].Length;
            // Get only the input vector values (first column)0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28
            int[] a = new int[Variables.trainDataTable.Columns.Count];
            for(int i=0;i<a.Length;i++)
            {
                a[i] = i;
            }
            Variables.trainInputs = data.GetColumns(a);

            // Get only the outputs (last column)
            double[] outputs = data.GetColumn(29);

            Variables.trainOutputs = new int[outputs.Length];
            for (int i = 0; i < outputs.Length; i++)
                Variables.trainOutputs[i] = (int)outputs[i];


        }

        private void testDataStart_Click(object sender, EventArgs e){

            if (this.trainDataGridView.DataSource != null)
            {
                if (kontrol())
                {
                    if (testTypeComboBox.SelectedItem == "Individual" && modelComboBox.SelectedItem != null)
                    {

                        if (modelComboBox.SelectedItem == "KNN" || modelComboBox.SelectedItem == "NaiveBayes" || modelComboBox.SelectedItem == "SVM")
                        {
                            new IndividualTest(trainDataGridView, this, modelComboBox.SelectedItem.ToString()).ShowDialog(this);
                        }
                        else
                            MessageBox.Show("Please select classification algorithm.");

                    }
                    else if (testTypeComboBox.SelectedItem == "Plural" && modelComboBox.SelectedItem != null)
                        new PluralTest(trainDataGridView, this, modelComboBox.SelectedItem.ToString()).ShowDialog(this);
                    else
                        MessageBox.Show("Please select test type or select model");
                }
                else
                    MessageBox.Show("Please train selected algorithm");
            }
            else {
                MessageBox.Show("Please get train data file");
            }
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            if (modelComboBox.SelectedItem == "KNN") {
                new Settings("KNN").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "C4.5")
            {
                new Settings("C4.5").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "SVM")
            {
                new Settings("SVM").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "Kmeans")
            {
                new Settings("Kmeans").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "GMM")
            {
                new Settings("GMM").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "MeanShift")
            {
                new Settings("MeanShift").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "NaiveBayes")
            {
                new Settings("NaiveBayes").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "BinarySplit")
            {
                new Settings("BinarySplit").ShowDialog(this);
            }
            else if (modelComboBox.SelectedItem == "RandomForest")
            {
                new Settings("RandomForest").ShowDialog(this);
            }
            else {
                MessageBox.Show("Please select model");
            }
            
        }

        private bool kontrol() {
            
            if (modelComboBox.SelectedItem == "KNN")
            {
                if (Variables.knn != null)
                    return true;
                else return false;
            }
            else if (modelComboBox.SelectedItem == "SVM")
            {
                if (Variables.svm != null)
                    return true;
                else return false;
            }
            else if (modelComboBox.SelectedItem == "Kmeans")
            {
                if (Variables.kmeans != null)
                    return true;
                else return false;
            }
            
            else if (modelComboBox.SelectedItem == "MeanShift")
            {
                if (Variables.meanShiftCluster != null)
                    return true;
                else return false;
            }
            else if (modelComboBox.SelectedItem == "NaiveBayes")
            {
                if (Variables.bayes != null)
                    return true;
                else return false;
            }
            else if (modelComboBox.SelectedItem == "BinarySplit")
            {
                if (Variables.kMeansCluster != null)
                    return true;
                else return false;
            }
            
            else
            {
                MessageBox.Show("Please train selected algorithm ");
                return false;
            }
        }
    }
}
