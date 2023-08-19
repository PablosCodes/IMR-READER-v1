using System;
using System.Diagnostics;
using System.Resources;
using System.Text;

namespace IMRReader.Common
{
    public class MessageBuilder
    {
        private const string FILE_LOADING_TARGETS_KEY = "FILE_LOADING_TARGETS";
        private const string FILE_LOADED_TARGETS_KEY = "FILE_LOADED_TARGETS";
        private const string FILE_LITE_LOADING_MEASUREMENTS = "FILE_LOADING_MEASUREMENTS";
        private const string FILE_LITE_LOADED_MEASUREMENTS = "FILE_LOADED_MEASUREMENTS";
        private const string FILE_LITE_LOADING_MEASUREMENT_METRICS = "FILE_LOADING_MEASUREMENT_METRICS";
        private const string FILE_LITE_LOADED_MEASUREMENT_METRICS = "FILE_LOADED_MEASUREMENT_METRICS";

        private readonly ResourceManager _resourceManager;

        public MessageBuilder()
        {
            _resourceManager = new ResourceManager("IMRReader.Properties.Resources",
                               typeof(MessageBuilder).Assembly);
        }

        private string? FileLoadingTargetsMsg => GetString(FILE_LOADING_TARGETS_KEY);
        private string? FileLoadedTargetsMsg => GetString(FILE_LOADED_TARGETS_KEY);
        private string? FileLoadingMeasurementsMsg => GetString(FILE_LITE_LOADING_MEASUREMENTS);
        private string? FileLoadedMeasurementsMsg => GetString(FILE_LITE_LOADED_MEASUREMENTS);
        private string? FileLoadingMeasurementMetricsMsg => GetString(FILE_LITE_LOADING_MEASUREMENT_METRICS);
        private string? FileLoadedMeasurementMetricsMsg => GetString(FILE_LITE_LOADED_MEASUREMENT_METRICS);

        private string? GetString(string key)
        {
            try
            {
                return _resourceManager.GetString(key);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return default;
            }
        }

        public string GetLoadingTargetsMsg(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(FileLoadingTargetsMsg);
            stringBuilder.Append(' ');
            stringBuilder.Append(filePath);
            string builtMessage = stringBuilder.ToString();

            return builtMessage;
        }

        public string GetLoadedTargetsMsg(string filePath, int targetsCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(FileLoadedTargetsMsg);
            stringBuilder.Append(' ');
            stringBuilder.Append(filePath);

            if (targetsCount > 0)
            {
                stringBuilder.Append(' ');
                stringBuilder.Append($"({targetsCount})");
            }
            string builtMessage = stringBuilder.ToString();

            return builtMessage;
        }

        public string GetLoadingMeasurementsMsg(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(FileLoadingMeasurementsMsg);
            stringBuilder.Append(' ');
            stringBuilder.Append(filePath);
            string builtMessage = stringBuilder.ToString();

            return builtMessage;
        }

        public string GetLoadedMeasurementsMsg(string filePath, int measurementsCount)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(FileLoadedMeasurementsMsg);
            stringBuilder.Append(' ');
            stringBuilder.Append(filePath);

            if (measurementsCount > 0)
            {
                stringBuilder.Append(' ');
                stringBuilder.Append($"({measurementsCount})");
            }

            string builtMessage = stringBuilder.ToString();

            return builtMessage;
        }

        public string GetLoadingMeasurementMetricsMsg(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(FileLoadingMeasurementMetricsMsg);
            stringBuilder.Append(' ');
            stringBuilder.Append(filePath);
            string builtMessage = stringBuilder.ToString();

            return builtMessage;
        }

        public string GetLoadedMeasurementMetricsMsg(string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(FileLoadedMeasurementMetricsMsg);
            stringBuilder.Append(' ');
            stringBuilder.Append(filePath);
            string builtMessage = stringBuilder.ToString();

            return builtMessage;
        }

    }
}









