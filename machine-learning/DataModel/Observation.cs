using System;
using System.Collections.Generic;
using System.Text;

namespace machine_learning
{
    //模型类
    public class Observation
    {
        public string Label { get; private set; }
        public int[] Pixels { get; private set; }

        public Observation(String label, int[] pixels)
        {
            this.Label = label;
            this.Pixels = pixels;

        }
    }
}
