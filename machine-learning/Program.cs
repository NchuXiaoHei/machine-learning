using System;
using machine_learning.Impl;

namespace machine_learning
{
    class Program
    {
        static void Main(string[] args)
        {
            var distance = new ManhattanDistance();
            var classifier = new BasicClassifier(distance);

            //@:写路径字符串可以让字符串不需要转义，否则需要写两个\\
            //读取训练集
            var trainingPath = @"D:\SojS\machine-learning\machine-learning-projects-for-dot-net-developers\chapter-1\DigitsRecognizer\Data\trainingsample.csv";
            var training = DataReader.ReadObservations(trainingPath);

            //训练基本分类器
            classifier.Train(training);

            //读取验证集
            var validationPath = @"D:\SojS\machine-learning\machine-learning-projects-for-dot-net-developers\chapter-1\DigitsRecognizer\Data\validationsample.csv";
            var validation = DataReader.ReadObservations(validationPath);

            //验证分类器
            var correct = Evaluator.Correct(validation, classifier);
            Console.WriteLine("{0:P2}", correct);

            Console.ReadLine();
        }
    }
}
