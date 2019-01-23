using Accord.Controls;
using Accord.MachineLearning;
using Accord.Math;
using Accord.Statistics.Distributions.Multivariate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using project;
using Accord.Statistics.Analysis;

namespace bitirme
{
    public partial class Visualization : Form
    {
        int numberOfClass;
        int[] data;
        Dictionary<int, int> set;
        String algo;

        public Visualization(int k,int[] data,Dictionary<int,int> set,String algo)
        {
            this.set = set;
            this.numberOfClass = k;
            this.data = data;
            this.algo = algo;
            //dgvPerformance.DataSource = new[] { new ConfusionMatrix };
            InitializeComponent();
            setDetails();
            setDatas();
        }
    
        private void setDatas(){
            chart1.Titles.Add("Result");
            chart1.Series["Series3"].IsValueShownAsLabel = true;
            
            if (algo=="KNN" || algo == "C4.5" || algo == "SVM" || algo == "NaiveBayes" || algo == "NaiveBayes") {
                chart1.Series["Series3"].Points.AddXY("OK", data[0]);
                chart1.Series["Series3"].Points.AddXY("NO", data[1]);
            }
            else {
                foreach (int key in set.Keys) {
                    chart1.Series["Series3"].Points.AddXY(key, set[key]);
                }
            }
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
