using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Statistics.Dtos
{
    public class StatisticDto 
    {
        public string Name { get; set; }
        public ChartDto Data { get; set; } 
    }
    public class ChartDto
    {
        public List<string> Labels { get; set; }
        public List<Dataset> Datasets { get; set; }
    }

    public class Dataset 
    {
        public string Label { get; set; }
        public string BackgroundColor { get; set; }
        public List<int> Data { get; set; }
    }
}
