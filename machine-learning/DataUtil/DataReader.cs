using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace machine_learning
{
    //读取数据的类
    class DataReader
    {
        private static Observation ObservationFactory(string data)
        {
            var commaSeparaated = data.Split(',');
            var label = commaSeparaated[0];
            var pixels =
                commaSeparaated
                .Skip(1)
                .Select(x => Convert.ToInt32(x))
                .ToArray();

            return new Observation(label, pixels);
        }

        public static Observation[] ReadObservations(string dataPath)
        {
            var data =
                File.ReadAllLines(dataPath)
                .Skip(1)
                .Select(ObservationFactory)
                .ToArray();

            return data;    
        }

    }
}
