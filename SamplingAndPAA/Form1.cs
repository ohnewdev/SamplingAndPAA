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
        public Form1()
        {
            InitializeComponent();
            Series s1 = chart1.Series[0];
            Series s2 = chart1.Series[1];

            for (int i = 0; i < rawData.Length; i++)
            {
                s1.Points.AddXY(i + 1, rawData[i]);
            }
            double[] sampleData = sampling(rawData, 10);
            //for (int i = 0; i < sampleData.Length; i++)
            //{

            //    s2.Points.AddXY(i + 1, sampleData[i]);
            //}

            double[] paaData = PAA(rawData, 4);
            for (int i = 0; i < paaData.Length; i++)
            {

                s2.Points.AddXY(i + 1, paaData[i]);
            }
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

    }

}
