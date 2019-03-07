using System;
using System.Collections.Generic;
using System.Text;

namespace machine_learning.Interface
{
    //分类器接口
    interface IClassifier
    {
        void Train(IEnumerable<Observation> trainingSet);

        string Predict(int[] pixels);
    }
}
