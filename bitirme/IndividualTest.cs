using project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Math;
using Accord.Statistics.Analysis;

namespace bitirme
{
    public partial class IndividualTest : Form
    {
        MainForm mainForm;
        DataGridView datas;
        String algo;

        double[] input;
        double result;
        int answer;
        public GeneralConfusionMatrix cm;

        //Ornek input son ozellik olması gereken cıktı
        //33;1747;575;0;999;1;0;0;17001;5;17001;1499;0;1;5000;5000;37;1;1;5;1;96;2;4;34600;0;0;12;781;1

        public IndividualTest(DataGridView data, MainForm main, String algo)
        {
            this.datas = data;
            this.mainForm = main;
            this.algo = algo;
            readyTestDatas();
            InitializeComponent();
            input = new double[Variables.trainInputs[0].Length];
            pictureBoxGreen.Visible = false;
            pictureBoxRed.Visible = false;
        }
        private void readyTestDatas()
        {
            // Variables.allDataCount = Variables.trainDataGridView.ColumnCount;
            Variables.testInputs = Variables.testDataTable.ToJagged<double>();
            Variables.testOutputs = Variables.testResults.Columns["NihaiKarar"].ToArray<int>();
            Variables.testDataCount = Variables.testOutputs.Length;


        }

        private void startTest(object sender, EventArgs e)
        {
            if (control(inputTextBox.Text))
            {
                toData(inputTextBox.Text);
                if (this.algo == "KNN")
                {
                    //MessageBox.Show("okkw");
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
                else if (this.algo == "NaiveBayes")
                {
                    naiveBayesTest();
                }
                else if (this.algo == "RandomForest")
                {
                    RandomForestTest();
                }
                else
                {
                    MessageBox.Show("Please select algorithm ");
                    return;
                }
            }
            else
                MessageBox.Show("Wrong input Format");
        }
      
        private void toData(String input) {
            String[] splitted = input.Split(';');
            int index = 0;
            foreach (String s in splitted) {
                if(index==29)
                    this.result= Convert.ToDouble(s);
                else
                    this.input[index] = Convert.ToDouble(s);

                index++;
            }
        }

        private Boolean control(String input) {
            String[] splitted = input.Split(';');
            if (splitted.Length == Variables.featuresNumber && dogruInput(splitted)) {
                return true;
            }
            MessageBox.Show("Check the features number or features content");
            return false;
        }

        private Boolean dogruInput(String [] input)
        {
            int n;
            foreach (String s in input) {
                bool isNumeric = int.TryParse(s, out n);
                if (!isNumeric) return false;
            }
            return true;
        }

        private void naiveBayesTest()
        {
            answer = Variables.bayes.Decide(input);
            if (answer == result)
            {
                pictureBoxGreen.Visible = true;
                pictureBoxRed.Visible = false;
            }
            else
            {
                pictureBoxGreen.Visible = false;
                pictureBoxRed.Visible = true;
            }
            cm = Variables.cm = GeneralConfusionMatrix.Estimate(Variables.bayes, Variables.trainInputs, Variables.trainOutputs);
            setDetails();
            history();
        }

        private void svmTest()
        {
            var a = Variables.svm.Decide(input);
            if (a == true) answer = 1; else answer = 0;
            if (answer == result)
            {
                pictureBoxGreen.Visible = true;
                pictureBoxRed.Visible = false;
            }
            else
            {
                pictureBoxGreen.Visible = false;
                pictureBoxRed.Visible = true;
            }
            cm = Variables.cm = GeneralConfusionMatrix.Estimate(Variables.svm, Variables.trainInputs, Variables.trainOutputs);
            setDetails();
            history();
        }

        private void c45Test()
        {
            throw new NotImplementedException();
        }

        private void knnTest()
        {
            answer = Variables.knn.Decide(input);

            if (answer == result)
            {
                pictureBoxGreen.Visible = true;
                pictureBoxRed.Visible = false;
            }
            else
            {
                pictureBoxGreen.Visible = false;
                pictureBoxRed.Visible = true;
            }
            cm = Variables.cm = GeneralConfusionMatrix.Estimate(Variables.knn, Variables.trainInputs, Variables.trainOutputs);
            setDetails();
            history();
        }

        private void RandomForestTest()
        {
            answer = Variables.randomForest.Decide(input);

            if (answer == result)
            {
                pictureBoxGreen.Visible = true;
                pictureBoxRed.Visible = false;
            }
            else
            {
                pictureBoxGreen.Visible = false;
                pictureBoxRed.Visible = true;
            }
            cm = Variables.cm = GeneralConfusionMatrix.Estimate(Variables.randomForest, Variables.trainInputs, Variables.trainOutputs);
            setDetails();
            history();
        }

        private void history()
        {
            mainForm.listView1.Items.Add("=========================================");
            mainForm.listView1.Items.Add($"Model:{mainForm.modelComboBox.SelectedItem}");
            mainForm.listView1.Items.Add($"TestType:{mainForm.testTypeComboBox.SelectedItem}");
            mainForm.listView1.Items.Add($"NumOfFeatures:{datas.ColumnCount - 1}");
            mainForm.listView1.Items.Add("=========================================");

        }

        private void setDetails()
        {
            textBox1.Text = algo;
            textBox2.Text = Variables.cm.Error.ToString();
            textBox3.Text = Variables.cm.Accuracy.ToString();
            textBox4.Text = Variables.cm.Kappa.ToString();
            textBox5.Text = Variables.cm.Pearson.ToString();
            textBox6.Text = Variables.cm.ChiSquare.ToString();
            textBox7.Text = Variables.cm.Phi.ToString();
            textBox8.Text = Variables.cm.Tau.ToString();

        }
    }
}
