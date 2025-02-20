using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Avalonia;

namespace App
{

    public partial class MainWindow : Window
    {
        
        public ObservableCollection<ISeries> ChartSeries { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            EpsilonBox = this.FindControl<TextBox>("EpsilonBox");
            BetaBox = this.FindControl<TextBox>("BetaBox");
            AlphaBox = this.FindControl<TextBox>("AlphaBox");
            DeltaBox = this.FindControl<TextBox>("DeltaBox");
            X0Box = this.FindControl<TextBox>("X0Box");
            Y0Box = this.FindControl<TextBox>("Y0Box");
         

            // Устанавливаем привязку для графика
            ChartSeries = new ObservableCollection<ISeries>();
            DataContext = this;

            
          
        }
        

        private void CalculateButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            try
            {
                double epsilon = double.Parse(EpsilonBox.Text);
                double beta = double.Parse(BetaBox.Text);
                double alpha = double.Parse(AlphaBox.Text);
                double delta = double.Parse(DeltaBox.Text);
                double x = double.Parse(X0Box.Text);
                double y = double.Parse(Y0Box.Text);

                List<double> prey = new List<double>();
                List<double> predator = new List<double>();

                int generations = 450;
                for (int t = 0; t < generations; t++)
                {
                    double newX = x + (epsilon * x - alpha * x * y);
                    double newY = y + (delta * x * y - beta * y);

                    prey.Add(newX);
                    predator.Add(newY);

                    x = newX;
                    y = newY;
                }

                // Обновляем данные графика
                ChartSeries.Clear();
                ChartSeries.Add(new LineSeries<double> { Values = prey, Name = "Жертвы" });
                ChartSeries.Add(new LineSeries<double> { Values = predator, Name = "Хищники" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}


