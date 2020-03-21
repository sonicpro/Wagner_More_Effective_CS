<Query Kind="Program">
  <Reference Relative="Utilities\bin\Debug\netstandard2.0\Utilities.dll">D:\SourcesCS\Wagner_More_Effective_CS\Utilities\bin\Debug\netstandard2.0\Utilities.dll</Reference>
  <Namespace>MoreEffectiveCS</Namespace>
</Query>

void Main()
{
	// Incorrect Object.Equals(object) implementation for reference types.
	
	B baseObject = new B();
	D derivedObject = new D();
	
	// Returns "The instances are equal" when calling from base class instance.
	Console.WriteLine(baseObject.Equals(derivedObject) ? "The instances are equal" : "The instances are not equal");
	
	// The result must be the same, but it differs.
	Console.WriteLine(derivedObject.Equals(baseObject) ? "The instances are equal" : "The instances are not equal");
}

// Define other methods and classes here
public class B // : IEquatable<B>
{
	private string Value { get; }
	
	public override bool Equals(object right)
	{
		if (object.ReferenceEquals(right, null))
			return false;
			
		if (object.ReferenceEquals(this, right))
			return true;
			
		// Incorrect, the method can return true comparing with the derived type.
		B rightAsB = right as B;
		if (rightAsB == null)
			return false;
			
		return Value == rightAsB.Value;
//		return this.Equals(rightAsB);
	}
	
//	public bool Equals(B other)
//	{
//		return Value == other.Value;
//	}
	
	public override int GetHashCode()
	{
		return Value.GetHashCode();
	}
}

public class D : B, IEquatable<D>
{
	private string Value { get; }

	public override bool Equals(object right)
	{
		if (object.ReferenceEquals(right, null))
			return false;

		if (object.ReferenceEquals(this, right))
			return true;
			
		D rightAsD = right as D;
		if (rightAsD == null)
			return false;

		return this.Equals(rightAsD);
	}

	public bool Equals(D other)
	{
		return Value == other.Value;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}