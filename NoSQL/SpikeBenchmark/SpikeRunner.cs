﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using SpikeLib;
using System;
using System.IO;

namespace SpikeBenchmark
{
    [SimpleJob(RuntimeMoniker.Net472)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter]
    public class SpikeRunner
    {
        private SpikeInMemery _spikeImMemery;
        private SpikeInRedis _spikeInRedis;

        private static int index = 1;
        public SpikeRunner()
        {
            _spikeImMemery = new SpikeInMemery();
            _spikeImMemery.InitStock(100000).Wait();

            _spikeInRedis = new SpikeInRedis();
            _spikeInRedis.InitStock(100000).Wait();

            File.AppendAllText(@"G:\Users\Administrator\Desktop\test123.txt", "构造" + index);
        }

        [Benchmark]

        public void RunSpikeInMemery()
        {
            var r = new Random();
            var userId = r.Next(1, 80000000);
            _spikeImMemery.Decrease(userId).Wait();
        }

        [Benchmark]

        public void RunSpikeInRedis()
        {
            var r = new Random();
            var userId = r.Next(1, 80000000);
            _spikeInRedis.Decrease(userId).Wait();
        }
    }
}
