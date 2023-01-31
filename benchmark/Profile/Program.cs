using HatTrick.CrockfordBase32;
using JetBrains.Profiler.Api;
using Profile;

var iterations = 1000;

//"prime" to ensure static elements are initialized
CrockfordBase32.GetString(0L);

//var target = new EncodeToStringProfileTarget();
//var target = new EncodeToCharArrayProfileTarget();
//var target = new EncodeToReadOnlyMemorProfileTarget();
var target = new DecodeToLongProfileTarget();

const string value = "7ZZZZZZZZZZZZ5";

MemoryProfiler.CollectAllocations(true);
MeasureProfiler.StartCollectingData();
MemoryProfiler.GetSnapshot();

for (var i = 0; i < iterations; i++)
    target.Execute(value, true);

MemoryProfiler.GetSnapshot();
MeasureProfiler.StopCollectingData();
