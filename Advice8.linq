<Query Kind="Statements">
  <Reference Relative="Utilities\bin\Debug\netstandard2.0\Utilities.dll">D:\SourcesCS\Wagner_More_Effective_CS\Utilities\bin\Debug\netstandard2.0\Utilities.dll</Reference>
  <Namespace>MoreEffectiveCS</Namespace>
</Query>

var rand = new Random();
// generate 100 "ponts" with coordinates from 0.000 to 99.999.
var sequence = (from x in Utilities.Generator(100, () => rand.NextDouble() * 100)
				let y = rand.NextDouble() * 100
				select new { x, y }).TakeWhile(point => point.x < 75);
var scaled = sequence.Select(p => new { x = p.x * 5, y = p.y * 5 });
var truncated = scaled.Select(p => new { x = p.x - 20, y = p.y - 20 });
var distances = from p in truncated
				let distance = Math.Sqrt(p.x * p.x + p.y * p.y)
				where distance < 500.0
				select new { p.x, p.y, distance };
				
distances.Dump();