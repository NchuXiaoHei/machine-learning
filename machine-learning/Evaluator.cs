using machine_learning.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace machine_learning
{
    //评估模型质量
    class Evaluator
    {
        /*
         * 计算正确率的函数
         * arg : validationSet 验证数据集
         *       classifier 分类器
         * return:
         *      Linq数据集操作，Select集合中保存的是Score返回的所有值得集合，通过Average计算平均值返回
         */
        public static double Correct(
            IEnumerable<Observation> validationSet,
            IClassifier classifier)
        {
            return validationSet
                .Select(Obs => Score(Obs, classifier))
                .Average();
        }

        //符合返回1，否则返回0
        private static double Score(
            Observation obs,
            IClassifier classifier)
        {
            if (classifier.Predict(obs.Pixels) == obs.Label)
                return 1.0;
            return 0.0;
        }
    }
}
