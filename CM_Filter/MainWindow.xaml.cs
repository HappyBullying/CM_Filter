using System;
using SkiaSharp;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace CM_Filter
{
    public partial class MainWindow : Window
    {
        private LinkedList<Point> points = new LinkedList<Point>();


        public MainWindow()
        {
            InitializeComponent();
            Calculate();
            surface.InvalidateVisual();
        }

        private void Calculate()
        {   
            double[] filter = new double[] { 6, 0, -4, -3, 5, 6, -6, -13, 7, 44, 64, 44, 7, -13, -6, 6, 5, -3, -4, 0, 6 };
            double Dpi = Math.PI * 2.0d;

            for (double ext = 0.0001; ext < 22.0d; ext += 0.01d)
            {
                double x = 0;
                double y = 0;

                for (int inn = 0; inn < filter.Length; inn++)
                {
                    double tmp = inn / ext;
                    x += filter[inn] * Math.Cos(Dpi * tmp);
                    y += filter[inn] * Math.Sin(Dpi * tmp);
                }

                double abscissa = Dpi / ext;

                double sqr = Math.Pow(x, 2.0d) + Math.Pow(y, 2.0d);
                double ordinate = 20.0d * Math.Log10(Math.Sqrt(sqr));

                points.AddLast(new Point(abscissa, ordinate));
            }
        }

        private void OnPaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();



            SKPaint paint = new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 4,
                IsAntialias = true
            };


            foreach(Point pt in points)
            {
                canvas.DrawPoint((float)(300 * pt.X), (float)(-10 * pt.Y) + 500, paint);
            }

        }
    }
}
