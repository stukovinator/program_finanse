using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataVis = System.Windows.Forms.DataVisualization;

namespace finanse
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var p1 = new DataPoint(1, 3721.63) 
            { 
                AxisLabel = "WYDATKI", 
                Color = System.Drawing.Color.BlueViolet, 
                BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#620BB3"), 
                BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom 
            };

            var p2 = new DataPoint(2, 5216.52)
            {
                AxisLabel = "ZAROBKI",
                Color = System.Drawing.Color.BlueViolet,
                BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#620BB3"),
                BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
            };
            chart1.Series[0].Points.Add(p1);
            chart1.Series[0].Points.Add(p2);

            chart1.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
            chart1.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;

        }
    }
}
