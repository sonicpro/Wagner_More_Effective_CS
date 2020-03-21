<Query Kind="Statements">
  <Reference Relative="Utilities\bin\Debug\netstandard2.0\Utilities.dll">D:\SourcesCS\Wagner_More_Effective_CS\Utilities\bin\Debug\netstandard2.0\Utilities.dll</Reference>
  <Namespace>MoreEffectiveCS</Namespace>
</Query>

int i = 5;
int j = 5;

if (!object.ReferenceEquals(i, j))
{
	Console.WriteLine("Boxed to object, the value types are never reference equals");
}

if (!object.ReferenceEquals(i, i))
{
	Console.WriteLine("Boxed to object, the value type instance is never equal to itsef");
}
