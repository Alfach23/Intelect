using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Classes
{
    class CNetwork
    {
        public List<CNeyron> ListInputNeyron;
        public List<CNeyron> ListOutputNeyron;
        public List<CLayer> ListLayers;
        private Random rnd;

        public CNetwork(int CountInpN, int CountOutN, int CountHiddenLayer, int CountNeyronHiddenLay)
        {
            //входные слои (нейроны)
            ListInputNeyron = new List<CNeyron>();
            ListOutputNeyron = new List<CNeyron>();
            for (int i = 0; i < CountInpN; i++)
            {
                CNeyron CNInp = new CNeyron();
                ListInputNeyron.Add(CNInp);
            }
            //выходные слои (нейроны)
            for (int i = 0; i < CountOutN; i++)
            {
                CNeyron CNOut = new CNeyron();
                ListOutputNeyron.Add(CNOut);
            }
            //слои скрытые
            for (int i = 0; i < CountHiddenLayer; i++)
            {
                CLayer CNLayer = new CLayer();
                for (int j = 0; j < CountNeyronHiddenLay; j++)
                {
                    CNeyron CNLay = new CNeyron();
                    CNLayer.NeuronsMass.Add(CNLay);
                }
                ListLayers.Add(CNLayer);
            }

            //связь между скрытыми и входными слоями
            foreach (var X1 in ListLayers.FirstOrDefault().NeuronsMass)
            {
                foreach (var X2 in ListInputNeyron)
                {
                    CSynapse mS = new CSynapse();
                    mS.ConnectionNeyron = X2;
                    mS.WeightN = rnd.NextDouble()*0.1-0.05;
                    X1.SinapsysMass.Add(mS);
                }
            }

            //связь между выходными и скрытыми
            foreach (var X2 in ListOutputNeyron)
            {
                foreach (var X3 in ListLayers.LastOrDefault().NeuronsMass)
                {
                    CSynapse mS = new CSynapse();
                    mS.ConnectionNeyron = X3;
                    mS.WeightN = rnd.NextDouble() * 0.1 - 0.05;
                    X2.SinapsysMass.Add(mS);
                }
            }
            //связи скрытых
            if (ListLayers.Count>1)
            {
                for (int i = 1; i < ListLayers.Count; i++)
                {
                    foreach (var XS1 in ListLayers[i].NeuronsMass)
                    {
                        foreach (var XS2 in ListLayers[i-1].NeuronsMass)
                        {
                            CSynapse ms = new CSynapse();
                            ms.ConnectionNeyron = XS2;
                            ms.WeightN = rnd.NextDouble() * 0.1 - 0.05;
                            XS1.SinapsysMass.Add(ms);
                        }
                    }
                }
            }

        }
        public double CalcNetwork(double[] InptutParamsMas)
        {
            for (int i = 0; i < InptutParamsMas.Count(); i++)
            {
                ListInputNeyron[i].OutNeyron = InptutParamsMas[i];
            }
            for (int i = 0; i < ListLayers.Count(); i++)
            {
                ListLayers[i].NeuronsMass = ListInputNeyron[i].Calculation;
            }

        }
    }
}
//кол-в входов (входных нейронов ListInputNeyron), 
//выходов (ListOutputNeyron), 
//сколько скрытых слоев, 
//кол-во нейронов в скрытых слоях