<Query Kind="Program" />

// #nullable enable - Uncomment if you target .Net Core 3.

void Main()
{
	var myPartner = new Person("William", "Gates");
	var me = new Person("Volodymyr", "Tsurko");
	Console.WriteLine(me.HyphenateForPartner(myPartner));	
}

// Define other methods and classes here

public class Point
{
	double x;
	double y;
	double? distance;
	
	public double X
	{
		get => x;
		private set => x = value;
	}
	
	public double Y
	{
		get => y;
		private set => y = value;
	}
	
	public Point(double x, double y) =>
		(this.x, this.y, distance) = (x, y, default);
	
	public static bool operator ==(Point left, Point right) =>
		(left.X, left.Y) == (right.X, right.Y);
		
	public static bool operator !=(Point left, Point right) =>
		(left.X, left.Y) != (right.X, right.Y);
		
	// CS8610 tells you that Object.Equals() parameter is now Object? rather than Object.
	public override bool Equals(object other) =>
		(other is Point otherPoint) ?
			this == otherPoint :
			false;

	public override int GetHashCode()
	{
		return this.X.GetHashCode() ^ this.Y.GetHashCode();
	}
}

public readonly struct ImmutablePoint
{
	public double X
	{
		get;
	}
	
	public double Y
	{
		get;
	}
	
	public double Distance
	{
		get;
	}
	
	public ImmutablePoint(double x, double y) =>
		(this.X, this.Y, Distance) = (x, y, Math.Sqrt(x * x + y * y));
}

class Person
{
	private string _firstName;
	
	private string _lastName;
	
	public string FirstName
	{
		get => _firstName;
		set => _firstName = value ??
			throw new ArgumentNullException(nameof(value), "Cannot set name to null");
	}
	
	public string LastName
	{
		get => _lastName;
		set => _lastName = value ??
			throw new ArgumentNullException(nameof(value), "Cannot set name to null");
	}
	
	public Person(string firstName, string lastName)
	{
		_firstName = firstName ??
			throw new ArgumentNullException(nameof(firstName), "Cannot set name to null");
		
		_lastName = lastName ??
			throw new ArgumentNullException(nameof(lastName), "Cannot set name to null");
	}
	
	public string HyphenateForPartner(Person partner)
	{
		_ = partner ??
			throw new ArgumentNullException(nameof(partner), "Partner should not be null");
			
		return $"{partner.LastName} - {this.LastName}";
	}
}