using System;
using System.Collections.Generic;
using System.Text;

namespace machine_learning.Impl
{
    //距离函数的实现之一，曼哈顿距离
    class ManhattanDistance : IDistance
    {
        public double Between(int[] pixels1, int[] pixels2)
        {
            if(pixels1.Length != pixels2.Length)
            {
                throw new ArgumentException("不合法的图片！");
            }

            var length = pixels1.Length;

            var distance = 0;

            for(int i = 0; i < length; i++)
            {
                distance += Math.Abs(pixels1[i] - pixels2[i]);
            }

            return distance;
        }
    }
}
