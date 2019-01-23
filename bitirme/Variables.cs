using Accord;
using Accord.IO;
using Accord.MachineLearning;
using Accord.MachineLearning.Bayes;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Distances;
using Accord.Math.Optimization.Losses;
using Accord.Math;
using Accord.Statistics.Analysis;
using Accord.Statistics.Distributions.DensityKernels;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Kernels;
using bitirme;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public static class Variables
    {
        ////Models Variable////

        //SVM
        public static SupportVectorMachine<IKernel> svm;
        public static SequentialMinimalOptimization<IKernel> smo;
        public static IKernel kernel;
        public static double svmComplex=1;
        public static double svmTolerance=0.05;
        public static double svmPos=1;
        public static double svmNeg=1;

        //KNN
        public static KNearestNeighbors knnSimple;
        public static KNearestNeighbors<double[]> knn;
        public static int knnNumber = 2;
        public static bool knnSquare = false;

        //RandomForest
        public static RandomForest randomForest;
        public static int randomForestTreesNumber = 100;
        public static double randomForestRadio = 1 ; 

        //C45
        public static C45Learning teacher;
        public static DecisionTree tree;

        //BinarySplit
        public static BinarySplit binarySplit;
        public static int binarySplitK = 2;
        public static double binarySplitTolerance = 0.05;
        public static int binarySplitMaxIter = 0;


        //Kmeans
        public static KMeans kmeans;
        public static KMeansClusterCollection kMeansCluster;
        public static int kMeansK = 2;
        public static double kMeansTolerance = 0.05;
        public static int kMeansMaxIter = 0;

        //GMM
        public static GaussianClusterCollection gaussianCluster;
        public static int gmmClusterNumber = 2;
        public static double gmmTolerance = 0.05;
        public static int gmmMaxIter = 0;

        //MeanShift
        public static MeanShiftClusterCollection meanShiftCluster;
        public static int meanShiftK = 2;
        public static double meanShiftTolerance = 0.05;
        public static int meanShiftMaxIter = 0;
        public static double meanShiftSigmaRadius = 0.1;


        //NaiveBayes
        public static NaiveBayes<NormalDistribution> bayes; 

        //Project Variable
        public static MainForm mainForm;
        public static string[] columnNames;
        public static HistoryData historyData = new HistoryData();
        public static Image green = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\Resources\\green.png");
        public static Image red = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\Resources\\red.png");
        public static Image yellow = new Bitmap("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\Resources\\yellow.png");
        public static ImageList imgs = new ImageList() { ImageSize = new Size(20, 20) };


        //Datas Variable
        public static int predictedDataCount=0;
        public static int trainDataCount=0;
        public static int testDataCount=0;

        public static DataGridView trainDataGridView;
        public static DataTable trainDataTable;
        public static DataGridView testDataGridView;
        public static DataTable testDataTable = new ExcelReader("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\bin\\Debug\\Resources\\test.xlsx").GetWorksheet("Worksheet");
        public static DataTable testResults = new ExcelReader("C:\\Users\\xyz\\Desktop\\bitirme\\bitirme_projesi\\bitirme\\bitirme\\bin\\Debug\\Resources\\testResult.xlsx").GetWorksheet("Worksheet");

        public static double[][] trainInputs;
        public static int[] trainOutputs;
        public static double[][] testInputs;
        public static int[] testOutputs;
        public static int featuresNumber;

        public static GeneralConfusionMatrix cm;

        ///////////Algorithm methods////////////////

        //KNN
        public static void knnTrain() {
            try {
                knn = new KNearestNeighbors<double[]>(k: knnNumber, distance: new SquareEuclidean());
                knn.Learn(trainInputs, trainOutputs);
                MessageBox.Show("KNearestNeighbors algorithm is trained");
            }
            catch (Exception e)  {
                MessageBox.Show(e.Message);
            }
        }

        //RandomForest
        public static void randomForestTrain()
        {
            try
            {
                var teacher = new RandomForestLearning()
                {
                    NumberOfTrees = randomForestTreesNumber,
                    SampleRatio = randomForestRadio
                };

                randomForest = teacher.Learn(trainInputs, trainOutputs);

                MessageBox.Show("RandomForest algorithm is trained");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //C4.5
        // yontem bulunamadı hatası diyor
        public static void c45Train() {
            try {

                teacher = new C45Learning()
                {
                    Join = 5, // Variables can participate 5 times in a decision path
                    MaxVariables = 0, // No limit on the number of variables to consider
                    MaxHeight = 10 // The tree can have a maximum height of 10
                };
                tree = teacher.Learn(Variables.trainInputs,Variables.trainOutputs);
                MessageBox.Show("C4.5 algorithm is trained");
                
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        //Classification(SVM)
        public static void svmTrain() {
            try {
                smo = new SequentialMinimalOptimization<IKernel>() {
                    Complexity = svmComplex,
                    Tolerance = svmTolerance,
                    PositiveWeight = svmPos,
                    NegativeWeight = svmNeg,
                    Kernel = kernel
                };
                // Run
                svm = smo.Learn(trainInputs, trainOutputs);
                MessageBox.Show("SVM algorithm is trained");
            }
            catch (Exception e){
                MessageBox.Show(e.Message);
            }

        }

        //Kmeans
        public static void kMeansTrain() { 
            try{
                kmeans = new KMeans(kMeansK, new SquareEuclidean())
                {
                    Tolerance = kMeansTolerance,
                    MaxIterations = kMeansMaxIter
                };
                kMeansCluster = kmeans.Learn(trainInputs);
                MessageBox.Show("Kmeans algorithm is trained");
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }

        }

        //GMM
        public static void gmmTrain() {
            try {
                var gmm = new GaussianMixtureModel(gmmClusterNumber)
                {
                    Tolerance = gmmTolerance,
                    MaxIterations = gmmMaxIter
                };

                if (kmeans != null)
                    gmm.Initialize(kmeans);

                gaussianCluster = gmm.Learn(trainInputs);
                MessageBox.Show("GMM algorithm is trained");
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        //MeanShift
        public static void meanShiftTrain() {
            try
            {
                var mean = new MeanShift()
                {
                    Kernel = new EpanechnikovKernel(),
                    Distance = new Euclidean(),
                    Bandwidth = meanShiftSigmaRadius,
                    Tolerance = meanShiftTolerance,
                    MaxIterations = meanShiftMaxIter
                };
                meanShiftCluster = mean.Learn(trainInputs);
                MessageBox.Show("MeanShift algorithm is trained");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Naive Bayes
        public static void naiveBayesTrain() {
            try
            {
                // Create the Bayes classifier and perform classification
                var teacher = new NaiveBayesLearning<NormalDistribution>();
                // Estimate the model using the data
                bayes = teacher.Learn(trainInputs, trainOutputs);
                MessageBox.Show("NaiveBayes algorithm is trained");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //BinarySplit
        public static void binarySplitTrain() {
            try
            {
                // Create the Bayes classifier and perform classification
                binarySplit = new BinarySplit(k: binarySplitK)
                {
                    Distance = new SquareEuclidean(),
                    MaxIterations = binarySplitMaxIter
                };

                // Use it to learn a data model
                kMeansCluster= binarySplit.Learn(trainInputs);
                MessageBox.Show("BinarySplit algorithm is trained");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //Helper methods
        public static void setImages() {
            imgs.Images.Add(green);
            imgs.Images.Add(red);
            //imgs.Images.Add(yellow);

            int b = 0;
            foreach (Image a in imgs.Images)
            {
                if (b == 1)
                    green = a;
                if (b == 0)
                    red = a;
                b++;
            }
        }
    }

    public class HistoryData{
        public string model;
        public string TestType;
        public int pluralNumberData;
        public int numberFeatures;
        public int predicted;
    }
}
