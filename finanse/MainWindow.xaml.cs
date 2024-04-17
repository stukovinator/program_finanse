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
using System.Globalization;
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
                string filePath = System.IO.Path.Combine(folderPath, "finanse", "wydatki.txt");

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
                    if (line == null)
                    {
                        Console.WriteLine("pusta linijka");
                        break;
                    }

                    string[] elements = line.Split(';');

                    if (elements.Length == 3)
                    {
                        string name = elements[0];
                        string value = elements[1];
                        //string category = elements[2];

                        //Console.WriteLine($"{name} {value} {category}");

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
                //Console.WriteLine("Wypisano 5 wydatkow");
            }
        }
    }

    public class TransactionsPanelExpenses
    {
        public decimal total_expenses;
        public decimal shopping;
        public decimal services;
        public decimal payments;

        public TransactionsPanelExpenses()
        {
            total_expenses = 0;
        }

        public void writeAllExpenses(StackPanel list)
        {
            string line;

            try
            {
                string workingDirectory = Environment.CurrentDirectory;
                string folderPath = System.IO.Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string filePath = System.IO.Path.Combine(folderPath, "finanse", "wydatki.txt");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Plik nie istnieje");
                    return;
                }

                StreamReader sr = new StreamReader(filePath);
                line = sr.ReadLine();

                while(line != null)
                {
                    string[] elements = line.Split(';');

                    if (elements.Length == 3)
                    {
                        string name = elements[0];
                        string value = elements[1];
                        string type = elements[2];

                        System.Windows.Media.Color bc = System.Windows.Media.Color.FromRgb(36, 36, 36);
                        System.Windows.Media.Color c1 = System.Windows.Media.Color.FromRgb(103, 27, 145);
                        System.Windows.Media.Color c2 = System.Windows.Media.Color.FromRgb(98, 11, 179);

                        Border border = new Border();
                        border.Background = new SolidColorBrush(bc);
                        border.BorderThickness = new Thickness(2);
                        border.CornerRadius = new CornerRadius(3);
                        border.Margin = new Thickness(5, 1, 5, 5);
                        border.Height = 39;
                        border.Width = 235;
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

                        decimal value_numeric;

                        if(decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value_numeric))
                        {
                            switch (type)
                            {
                                case "zakupy":
                                    this.shopping += value_numeric;
                                    break;
                                case "uslugi":
                                    this.services += value_numeric;
                                    break;
                                case "oplata":
                                    this.payments += value_numeric;
                                    break;
                            }
                            this.total_expenses += value_numeric;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy format liczby");
                        }
                        
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
                Console.WriteLine($"zakupy: {this.shopping}  uslugi: {this.services}  oplaty: {this.payments}");
            }
        }
    }

    public class TransactionsPanelEarnings
    {
        public decimal total_earnings;
        public decimal salary;
        public decimal refunds;
        public decimal transfers;
        public decimal deposits;

        public TransactionsPanelEarnings()
        {
            total_earnings = 0;
        }

        public void writeAllEarnings(StackPanel list)
        {
            string line;

            try
            {
                string workingDirectory = Environment.CurrentDirectory;
                string folderPath = System.IO.Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                string filePath = System.IO.Path.Combine(folderPath, "finanse", "zarobki.txt");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Plik nie istnieje");
                    return;
                }

                StreamReader sr = new StreamReader(filePath);
                line = sr.ReadLine();

                while (line != null)
                {
                    string[] elements = line.Split(';');

                    if (elements.Length == 3)
                    {
                        string name = elements[0];
                        string value = elements[1];
                        string type = elements[2];

                        System.Windows.Media.Color bc = System.Windows.Media.Color.FromRgb(36, 36, 36);
                        System.Windows.Media.Color c1 = System.Windows.Media.Color.FromRgb(103, 27, 145);
                        System.Windows.Media.Color c2 = System.Windows.Media.Color.FromRgb(98, 11, 179);

                        Border border = new Border();
                        border.Background = new SolidColorBrush(bc);
                        border.BorderThickness = new Thickness(2);
                        border.CornerRadius = new CornerRadius(3);
                        border.Margin = new Thickness(5, 1, 5, 5);
                        border.Height = 39;
                        border.Width = 235;
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
                        valueLabel.Content = "+" + value;
                        valueLabel.HorizontalContentAlignment = HorizontalAlignment.Right;
                        valueLabel.Margin = new Thickness(0, 0, 10, 0);
                        valueLabel.Foreground = System.Windows.Media.Brushes.LightGreen;
                        valueLabel.FontFamily = new System.Windows.Media.FontFamily(folderPath + "\\finanse\\fonts\\#Roboto Regular");
                        System.Windows.Controls.Grid.SetColumn(valueLabel, 1);

                        grid.Children.Add(nameLabel);
                        grid.Children.Add(valueLabel);

                        sp.Children.Add(grid);

                        border.Child = sp;

                        list.Children.Add(border);

                        decimal value_numeric;

                        if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value_numeric))
                        {
                            switch (type)
                            {
                                case "wyplata":
                                    this.salary += value_numeric;
                                    break;
                                case "zwrot":
                                    this.refunds += value_numeric;
                                    break;
                                case "przelew":
                                    this.transfers += value_numeric;
                                    break;
                                case "wplata":
                                    this.deposits += value_numeric;
                                    break;
                            }
                            this.total_earnings += value_numeric;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy format liczby");
                        }
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
                Console.WriteLine($"wyplata: {this.salary}  zwroty: {this.refunds}  przelewy: {this.transfers}  wplaty: {this.deposits}");
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

    public class TabNavigation
    {
        public StackPanel hometab;
        public StackPanel transactionstab;
        public StackPanel paymentstab;
        public StackPanel statstab;

        public TabNavigation()
        {
            this.hometab = null;
            this.transactionstab = null;
            this.paymentstab = null;
            this.statstab = null;
        }

        public TabNavigation(StackPanel hometab, StackPanel transactionstab, StackPanel paymentstab, StackPanel statstab)
        {
            this.hometab = hometab;
            this.transactionstab = transactionstab;
            this.paymentstab = paymentstab;
            this.statstab = statstab;
        }

        public void homeButton()
        {
            this.hometab.Visibility = Visibility.Visible;
            this.transactionstab.Visibility = Visibility.Collapsed;
            this.paymentstab.Visibility = Visibility.Collapsed;
            this.statstab.Visibility = Visibility.Collapsed;
        }

        public void transactionsButton()
        {
            this.hometab.Visibility = Visibility.Collapsed;
            this.transactionstab.Visibility = Visibility.Visible;
            this.paymentstab.Visibility = Visibility.Collapsed;
            this.statstab.Visibility = Visibility.Collapsed;
        }

        public void paymentsButton()
        {
            this.hometab.Visibility = Visibility.Collapsed;
            this.transactionstab.Visibility = Visibility.Collapsed;
            this.paymentstab.Visibility = Visibility.Visible;
            this.statstab.Visibility = Visibility.Collapsed;
        }

        public void statsButton()
        {
            this.hometab.Visibility = Visibility.Collapsed;
            this.transactionstab.Visibility = Visibility.Collapsed;
            this.paymentstab.Visibility = Visibility.Collapsed;
            this.statstab.Visibility = Visibility.Visible;
        }
    }

    public class Statistics
    {
        public Statistics()
        {

        }

        public void writeExpenses()
        {

        }

        public void writeEarnings()
        {

        }

        public void drawChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {

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

            TransactionsPanelExpenses tpex = new TransactionsPanelExpenses();
            tpex.writeAllExpenses(ALL_EXPENSES_LIST);

            TransactionsPanelEarnings tper = new TransactionsPanelEarnings();
            tper.writeAllEarnings(ALL_EARNINGS_LIST);
        }

        private void navigationButton(object sender, MouseButtonEventArgs e)
        {
            Border btn = sender as Border;

            TabNavigation tabNavigation = new TabNavigation(HOME_TAB, TRANSACTIONS_TAB, PAYMENTS_TAB, STATS_TAB);

            switch (btn.Tag)
            {
                case "HOME_BTN":
                    tabNavigation.homeButton();
                    break;
                case "TRANS_BTN":
                    tabNavigation.transactionsButton();
                    break;
                case "PAYMENTS_BTN":
                    tabNavigation.paymentsButton();
                    break;
                case "STATS_BTN":
                    tabNavigation.statsButton();
                    break;
            }
        }

    }
}