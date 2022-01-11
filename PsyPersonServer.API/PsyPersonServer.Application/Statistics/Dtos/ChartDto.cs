using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Statistics.Dtos
{
    public class StatisticDto 
    {
        public string Name { get; set; }
        public string NameCode { get; set; }
        public ChartDto Data { get; set; } 
    }
    public class ChartDto
    {
        public List<string> Labels { get; set; }
        public List<DatasetDto> Datasets { get; set; }
    }

    public class DatasetDto 
    {
        public string Label { get; set; }
        public bool Fill { get; set; }
        public string BorderColor { get; set; }
        public string YAxisID { get; set; }
        public double Tension { get; set; }
        public List<double> Data { get; set; }
    }
}
