using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    class CNeyron
    {
        public List<CSynapse> SinapsysMass { get; set; }
        public double OutNeyron { get; set; }
        public void Calculation()
        {
            double res = 0;
            foreach (var item in SinapsysMass)
            {
                res += item.WeightN * item.ConnectionNeyron.OutNeyron;
            }
            OutNeyron = res;
        }
    }
}
