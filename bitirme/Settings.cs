using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project;
using Accord.Statistics.Kernels;

namespace bitirme
{
    public partial class Settings : Form
    {
        String model;
        public Settings(String algo){
            this.model = algo;
            InitializeComponent();
            selectAlgo();
        }

        private void knnOK(object sender, EventArgs e){

            Variables.knnNumber = (int)this.knnNumber.Value;
            this.Close();
        }

        private void svmOK(object sender, EventArgs e)
        {

            Variables.svmComplex = (double)numC.Value;
            Variables.svmTolerance = (double)numT.Value;
            Variables.svmPos = (double)numPositiveWeight.Value;
            Variables.svmNeg = (double)numNegativeWeight.Value;

            if (svmComboBox.SelectedItem == "Gaussian")
            {
                Variables.kernel = new Gaussian((double)numSigma.Value);
            }
            else if (svmComboBox.SelectedItem == "Polynomial")
            {
                Variables.kernel = new Polynomial((int)numDegree.Value, (double)numPolyConstant.Value);
            }
            else if (svmComboBox.SelectedItem == "Laplacian")
            {
                Variables.kernel = new Laplacian((double)numSigma.Value);
            }
            else if (svmComboBox.SelectedItem == "Sigmoid")
            {
                Variables.kernel = new Sigmoid((double)numSigAlpha.Value, (double)numPolyConstant.Value);
            }
            else {
                MessageBox.Show("Please select kernel");
                return;
            }
            this.Close();
        }

        private void kmeansOK(object sender, EventArgs e) {
            Variables.kMeansK = (int)numKMeans.Value;
            Variables.kMeansTolerance = (double)kMeansTolerance.Value;
            Variables.kMeansMaxIter = (int)kMeansMaxIter.Value;
            this.Close();
        }

        private void randomForestOK(object sender, EventArgs e)
        {
            Variables.randomForestRadio = (double)randomForestRatio.Value;
            Variables.randomForestTreesNumber = (int)randomForestNumberTree.Value;
            this.Close();
        }

        private void gmmOK(object sender, EventArgs e)
        {
            Variables.gmmClusterNumber = (int)gmmNumber.Value;
            Variables.gmmTolerance = (double)gmmTolerans.Value;
            Variables.gmmMaxIter = (int)gmmMaxIter.Value;
            this.Close();
        }

        private void meanShiftOK(object sender, EventArgs e) {
            Variables.meanShiftK = (int)meanShiftCluster.Value;
            Variables.meanShiftTolerance = (double)meanShiftTolerance.Value;
            Variables.meanShiftMaxIter = (int)meanShiftMaxIter.Value;
            Variables.meanShiftSigmaRadius = (double)meanShiftRadius.Value;
            this.Close();
        }

        private void binarySplitOK(object sender, EventArgs e)
        {
            Variables.binarySplitK = (int)binarySplitK.Value;
            Variables.binarySplitTolerance = (double)binarySplitTolerance.Value;
           // Variables.binarySplitMaxIter = (int)binarySplitMaxIter.Value;
            this.Close();
        }

        private void selectAlgo() {
            if (model == "KNN")
            {
                setPanels(false,true,false,false,false,false,false,false,false, false);
            } else if (model == "C4.5")
            {
                setPanels(false, false, true, false, false, false, false, false, false, false);
            }
            else if (model == "SVM")
            {
                setPanels(false, false, false, true, false, false, false, false, false, false);
            }
            else if (model == "Kmeans")
            {
                setPanels(false, false, false, false, true, false, false, false, false, false);
            }
            else if (model == "GMM")
            {
                setPanels(false, false, false, false, false, true, false, false, false, false);
            }
            else if (model == "MeanShift")
            {
                setPanels(false, false, false, false, false, false, true, false, false, false);
            }
            else if (model == "NaiveBayes")
            {
                setPanels(false, false, false, false, false, false, false, true, false, false);
            }
            else if (model == "BinarySplit")
            {
                setPanels(false, false, false, false, false, false, false, false, true, false);
            }
            else if (model == "RandomForest")
            {
                setPanels(false, false, false, false, false, false, false, false, false, true);
            }
            else {
                setPanels(true, false, false, false, false, false, false, false, false, false);
            }
        }

        private void setPanels(bool no,bool knn,bool c45,bool svm,bool kmeans,bool gmm,bool meanshift,bool naiveBayes,bool binary,bool rand) {
            noPanel.Visible = no;
            knnPanel.Visible = knn;
            c45Panel.Visible = c45;
            svmPanel.Visible = svm;
            kMeansPanel.Visible = kmeans;
            gmmPanel.Visible = gmm;
            meanShiftPanel.Visible = meanshift;
            naiveBayesPanel.Visible = naiveBayes;
            binarySplitPanel.Visible = binary;
            randomForestPanel.Visible = rand;
        }

        private void svmPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
