using machine_learning.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace machine_learning.Impl
{
    class BasicClassifier : IClassifier
    {
        //训练集
        private IEnumerable<Observation> data;

        //距离函数
        private readonly IDistance distance;

        //创建分类器时需要指定距离函数
        public BasicClassifier(IDistance distance)
        {
            this.distance = distance;
        }

        //给定训练集，（意味着训练？）
        public void Train(IEnumerable<Observation> trainingSet)
        {
            this.data = trainingSet;
        }

        //根据给定图片，返回训练集中最符合条件的一个
        public string Predict(int[] pixels)
        {
            Observation currentBest = null;
            var shortest = Double.MaxValue;

            foreach(Observation obs in this.data)
            {
                var dist = this.distance.Between(obs.Pixels, pixels);
                if(dist < shortest)
                {
                    shortest = dist;
                    currentBest = obs;
                }
            }

            return currentBest.Label;
        }
    }
}
