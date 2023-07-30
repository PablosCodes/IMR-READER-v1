using ScottPlot.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.Common
{
    public static class PlotStyleExtension
    {
        public static IStyle GetIStyle(this PlotStyle style) => style switch {
            PlotStyle.Monospace => ScottPlot.Style.Monospace,
            PlotStyle.Gray1 => ScottPlot.Style.Gray1,
            PlotStyle.Blue1 => ScottPlot.Style.Blue1,
            PlotStyle.Blue2 => ScottPlot.Style.Blue2,
            PlotStyle.Black => ScottPlot.Style.Black,
            PlotStyle.Seaborn => ScottPlot.Style.Seaborn,
            PlotStyle.Light1 => ScottPlot.Style.Light1,
            _ => throw new ArgumentOutOfRangeException(nameof(style))
        };
    }
}
