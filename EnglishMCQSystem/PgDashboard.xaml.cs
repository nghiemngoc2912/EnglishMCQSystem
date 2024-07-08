using EnglishMCQSystem.Models;
using ScottPlot;
using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace EnglishMCQSystem
{
    /// <summary>
    /// Interaction logic for PgDashboard.xaml
    /// </summary>
    public partial class PgDashboard : Page
    {

        EnglishMcqsystemContext context = new EnglishMcqsystemContext();
        public PgDashboard()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                LoadStatistics();
                LoadNumOfTestPlot();
                LoadAverageScore();
                LoadNumOfTestTaken();
            };
        }

        private void LoadStatistics()
        {
            txtNumOfUser.Text = "Number of Users: "+context.Users.Count().ToString();
            txtNumOfTest.Text = "Number of Tests: " + context.Tests.Count().ToString();
            txtNumOfQuestion.Text = "Number of Questions: " + context.Questions.Count().ToString();
            txtNumOfTestTaken.Text = "Number of Tests Taken: " + context.UserTests.Count().ToString();
        }

        private void LoadNumOfTestTaken()
        {
            var usersTests= context.UserTests.ToList();
            List<Tick> dataX = new List<Tick>();
            List<double> dataY = new List<double>();
            //load num of tests taken by date in a week 
            DateTime curDate = DateTime.Now;
            for (int i = 0; i < 14; i++)
            {
                DateTime date1 = new DateTime(curDate.AddDays(-i).Year, curDate.AddDays(-i).Month, curDate.AddDays(-i).Day).Date;
                var x = usersTests.Count(t => t.TestDate.HasValue && t.TestDate.Value.Date == date1);
                dataX.Add(new Tick(14-(i + 1), date1.ToString("dd/MM/yyyy")));
                dataY.Add(x);
            }
            pltNumOfTestTaken.Plot.Add.ScatterLine(dataX.Select(t => t.Position).ToArray(), dataY.ToArray());
            pltNumOfTestTaken.Plot.Title("Number of tests taken in the last 14 days");
            pltNumOfTestTaken.Plot.Axes.Margins(bottom: 0, left: 0);
            pltNumOfTestTaken.Plot.HideGrid();
            pltNumOfTestTaken.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(dataX.ToArray());
            pltNumOfTestTaken.Plot.Add.Crosshair(0, 0);
            pltNumOfTestTaken.Plot.Axes.Bottom.TickLabelStyle.Rotation = 30;
            pltNumOfTestTaken.Plot.Axes.AutoScaleExpand();
            pltNumOfTestTaken.Refresh();
        }

        private void LoadAverageScore()
        {
            List<Tick> dataX = new List<Tick>();
            var tests = context.Tests.ToList();
            for (int i = 0; i < tests.Count; i++)
            {
                dataX.Add(new Tick(i + 1, tests[i].Name));
                var userTests = context.UserTests.Where(t => t.Test.Name == tests[i].Name).ToList();
                double sum = 0;
                for (int j = 0; j < userTests.Count; j++)
                {
                    sum += (double )userTests[j].Score;
                }
                double avg = sum / userTests.Count;
                pltAverageScore.Plot.Add.Bar(i + 1, avg);
            }
            pltAverageScore.Plot.Title("Average score of each test");
            pltAverageScore.Plot.Axes.Margins(bottom: 0, left: 0);
            pltAverageScore.Plot.HideGrid();
            pltAverageScore.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(dataX.ToArray());
            pltAverageScore.Plot.Add.Crosshair(0, 0);
            pltAverageScore.Plot.Axes.Bottom.TickLabelStyle.Rotation = 30;
            pltAverageScore.Plot.Axes.AutoScaleExpand();
            pltAverageScore.Refresh();
        }

        private void LoadNumOfTestPlot()
        {
            List<Tick> dataX = new List<Tick>();
            var tests = context.Tests.ToList();
            for (int i = 0; i < tests.Count; i++)
            {
                dataX.Add(new Tick(i + 1, tests[i].Name));
                int x = context.UserTests.Count(t => t.Test.Name == tests[i].Name);
                pltNumOfTimesTest.Plot.Add.Bar(i + 1, (double)x);
            }
            pltNumOfTimesTest.Plot.Title("Number of times each test has been taken");
            pltNumOfTimesTest.Plot.Axes.Margins(bottom: 0, left: 0);
            pltNumOfTimesTest.Plot.HideGrid();
            pltNumOfTimesTest.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(dataX.ToArray());
            pltNumOfTimesTest.Plot.Add.Crosshair(0, 0);
            pltNumOfTimesTest.Plot.Axes.Bottom.TickLabelStyle.Rotation = 30;
            pltNumOfTimesTest.Plot.Axes.AutoScaleExpand();
            pltNumOfTimesTest.Refresh();
        }
    }
}
