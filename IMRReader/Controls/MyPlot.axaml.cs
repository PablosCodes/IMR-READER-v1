using Avalonia;
using Avalonia.Controls;
using IMRReader.Application.Common;
using ScottPlot;
using ScottPlot.Avalonia;
using ScottPlot.Styles;
using System;

namespace IMRReader.Controls;

public partial class MeasurementChartView : UserControl
{
    private static readonly string PLOT_CONTROL_NAME = "chart";

    public static readonly DirectProperty<MeasurementChartView, double[]?> XDataProperty =
        AvaloniaProperty.RegisterDirect<MeasurementChartView, double[]?>(
        nameof(XData), o => o.XData, (o, v) => o.XData = v);

    private double[]? _xData = Array.Empty<double>();
    public double[]? XData
    {
        get { return _xData; }
        set { SetAndRaise(XDataProperty, ref _xData, value); }
    }

    public static readonly DirectProperty<MeasurementChartView, double[]?> YDataProperty =
        AvaloniaProperty.RegisterDirect<MeasurementChartView, double[]?>(
        nameof(YData), o => o.YData, (o, v) => o.YData = v);

    private double[]? _yData = Array.Empty<double>();
    public double[]? YData
    {
        get { return _yData; }
        set { SetAndRaise(XDataProperty, ref _yData, value); }
    }

    public static readonly StyledProperty<PlotStyle> PlotStyleProperty =
    AvaloniaProperty.Register<MeasurementChartView, PlotStyle>(
        nameof(Background),
        defaultValue: PlotStyle.Monospace);

    public PlotStyle PlotStyle
    {
        get { return GetValue(PlotStyleProperty); }
        set { SetValue(PlotStyleProperty, value); }
    }

    public MeasurementChartView()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if(change.Property == PlotStyleProperty)
        {
            UpdatePlotStyle(PlotStyle);
        }

        if(change.Property == XDataProperty || change.Property == YDataProperty)
        {
            RefreshPlot();
        }
    }

    private void UpdatePlotStyle(PlotStyle styleToApply)
    {
        AvaPlot? plot = GetPlotControl();
        if (plot is null)
            return;

        plot.Plot.Style(styleToApply.GetIStyle());
        plot.Refresh();
    }

    // TODO: fix blinking plot when changinh themes
    private void RefreshPlot()
    {
        if (!CanRefreshPlot()) return;

        AvaPlot? plot = GetPlotControl();
        if (plot is null)
            return;

        plot.Plot.AddScatter(XData, YData);
        plot.Refresh();
    }

    private bool CanRefreshPlot()
    {
        return XData is not null && YData is not null && XData.Length > 0 && YData.Length > 0;
    }

    private AvaPlot? GetPlotControl()
    {
        return this.Find<AvaPlot>(PLOT_CONTROL_NAME);
    }
}