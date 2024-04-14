using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;
using LiveCharts.Wpf.Charts.Base;
//using DataVis = System.Windows.Forms.DataVisualization;

namespace finanse
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 

    public class StartPanelTransactions
    {
        public StartPanelTransactions()
        {

        }

        public void writeLastTransactions(StackPanel list)
        {
            string line;

            try
            {
                string workingDirectory = Environment.CurrentDirectory;
                string folderPath = System.IO.Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string filePath = System.IO.Path.Combine(folderPath, "finanse", "transakcje.txt");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Plik nie istnieje");
                    return;
                }

                StreamReader sr = new StreamReader(filePath);
                line = sr.ReadLine();

                // ZROBIĆ ABY ODCZYTYWAŁO PLIK TRANSAKCJE OD KOŃCA

                for (int i = 0; i < 5; i++)
                {
                    if (line == null) break;

                    string[] elements = line.Split(';');

                    if (elements.Length == 2)
                    {
                        string name = elements[0];
                        string value = elements[1];

                        System.Windows.Media.Color bc = System.Windows.Media.Color.FromRgb(36, 36, 36);
                        System.Windows.Media.Color c1 = System.Windows.Media.Color.FromRgb(103, 27, 145);
                        System.Windows.Media.Color c2 = System.Windows.Media.Color.FromRgb(98, 11, 179);

                        Border border = new Border();
                        border.Background = new SolidColorBrush(bc);
                        border.BorderThickness = new Thickness(2);
                        border.CornerRadius = new CornerRadius(3);
                        border.Margin = new Thickness(5, 5, 5, 0);
                        border.Height = 39;
                        border.BorderBrush = new LinearGradientBrush(c1, c2, new System.Windows.Point(0.5, 0), new System.Windows.Point(0.5, 1));

                        StackPanel sp = new StackPanel();
                        sp.Margin = new Thickness(0);

                        System.Windows.Controls.Grid grid = new System.Windows.Controls.Grid();
                        grid.Margin = new Thickness(0, 5, 0, 5);
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                        Label nameLabel = new Label();
                        nameLabel.Content = name.ToUpper();
                        nameLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
                        nameLabel.Margin = new Thickness(10, 0, 0, 0);
                        nameLabel.Foreground = System.Windows.Media.Brushes.White;
                        nameLabel.FontFamily = new System.Windows.Media.FontFamily(folderPath + "\\finanse\\fonts\\#Roboto Medium");
                        System.Windows.Controls.Grid.SetColumn(nameLabel, 0);

                        Label valueLabel = new Label();
                        valueLabel.Content = "-" + value;
                        valueLabel.HorizontalContentAlignment = HorizontalAlignment.Right;
                        valueLabel.Margin = new Thickness(0, 0, 10, 0);
                        valueLabel.Foreground = System.Windows.Media.Brushes.IndianRed;
                        valueLabel.FontFamily = new System.Windows.Media.FontFamily(folderPath + "\\finanse\\fonts\\#Roboto Regular");
                        System.Windows.Controls.Grid.SetColumn(valueLabel, 1);

                        grid.Children.Add(nameLabel);
                        grid.Children.Add(valueLabel);

                        sp.Children.Add(grid);

                        border.Child = sp;

                        list.Children.Add(border);
                    }

                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            finally
            {
                //Console.WriteLine("Wypisano 5 transakcji");
            }
        }
    }

    public class StarterPanelChart
    {
        public void fillChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            var p1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1, 3721.63)
            {
                AxisLabel = "WYDATKI\n(3721.63)",
                Color = System.Drawing.Color.BlueViolet,
                BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#620BB3"),
                BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
            };

            var p2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2, 5216.52)
            {
                AxisLabel = "ZAROBKI\n(5216.52)",
                Color = System.Drawing.Color.BlueViolet,
                BackSecondaryColor = System.Drawing.ColorTranslator.FromHtml("#620BB3"),
                BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom
            };
            chart.Series[0].Points.Add(p1);
            chart.Series[0].Points.Add(p2);

            chart.ChartAreas[0].AxisX.LineColor = System.Drawing.Color.White;
            chart.ChartAreas[0].AxisY.LineColor = System.Drawing.Color.White;
            chart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            StartPanelTransactions spt = new StartPanelTransactions();
            spt.writeLastTransactions(LAST_TRANSACTIONS_LIST);

            StarterPanelChart spc = new StarterPanelChart();
            spc.fillChart(chart1);
        }
    }
}
