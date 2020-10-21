using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SamplingAndPAA
{
    public partial class Form1 : Form
    {
        double[] rawData = { 15, 29, 21, 45, 23, 63, 54, 12, 54, 61, 44, 56, 23, 56, 21, 44, 32, 46, 23, 38 };

        private Series s1;
        private Series s2;
        private Series s3;


        public Form1()
        {
            InitializeComponent();


            s1 = chart1.Series[0];
            s2 = chart1.Series[1];
            s3 = chart1.Series[2];


            axKHOpenAPI1.CommConnect();



            //for (int i = 0; i < rawData.Length; i++)
            //{
            //    s1.Points.AddXY(i + 1, rawData[i]);
            //}
            //double[] sampleData = sampling(rawData, 10);
            ////for (int i = 0; i < sampleData.Length; i++)
            ////{

            ////    s2.Points.AddXY(i + 1, sampleData[i]);
            ////}

            //double[] paaData = PAA(rawData, 4);
            //for (int i = 0; i < paaData.Length; i++)
            //{

            //    s2.Points.AddXY(i + 1, paaData[i]);
            //}
        }
        public static double[] sampling(double[] rawData, int n)
        {
            double[] samplingData = new double[n];
            int interval = rawData.Length / n;

            for (int i = 0; i < n; i++)
            {
                samplingData[i] = rawData[i * interval];

            }

            return samplingData;
        }

        public static double[] PAA(double[] rawData, int n)
        {
            double[] paaData = new double[n];
            int interval = rawData.Length / n;
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                int count = 0;
                for (int j = interval * i; j < interval * (i + 1); j++)
                {
                    sum += rawData[j];
                    count++;

                }

                if (count != 0)
                {
                    double avg = sum / count;
                    paaData[i] = avg;

                }
            }

            return paaData;
        }

        private void axKHOpenAPI1_OnEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                axKHOpenAPI1.SetInputValue("종목코드", "005930");
                axKHOpenAPI1.SetInputValue("기준일자", DateTime.Now.ToString("yyyyMMdd"));
                axKHOpenAPI1.SetInputValue("수정주가구분", "0");
                axKHOpenAPI1.CommRqData("일봉차트", "opt10081", 0, "1001");

            }
        }

        private void axKHOpenAPI1_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
            double[] rawData = new double[count];
            for (int i = 0; i < count; i++)
            {
                rawData[i] = double.Parse(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가"));

            }
            for (int i = 0; i < rawData.Length; i++)
            {

                s1.Points.AddXY(i + 1, rawData[i]);
            }

            double[] sampleData = sampling(rawData, 15);
            for (int i = 0; i < sampleData.Length; i++)
            {

                s2.Points.AddXY(i + 1, sampleData[i]);
            }

            double[] paaData = PAA(rawData, 15);
            for (int i = 0; i < paaData.Length; i++)
            {

                s3.Points.AddXY(i + 1, paaData[i]);
            }


        }
    }

}
