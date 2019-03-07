using System;
using System.Collections.Generic;
using System.Text;

namespace machine_learning
{
    //距离接口，解耦和
    interface IDistance
    {
        double Between(int[] pixels1, int[] pixels2);
    }
}
