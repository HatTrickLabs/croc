using Benchmark;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Benchmarks>(DefaultConfig.Instance.WithSummaryStyle(SummaryStyle.Default.WithMaxParameterColumnWidth(100)));
