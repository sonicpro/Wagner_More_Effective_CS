<Query Kind="Program" />

void Main()
{
	// Standard Constructor.
	Address a0 = new Address();
	Console.WriteLine($"Line1: '{a0.Line1}', City: '{a0.City}', ZipCode: '{a0.ZipCode}'");
	
	
	// Immutable struct is created.
	Address a1 = new Address("Ill S. Main",
		"", "Anytown", "IL", 61111);
	
	// Reassigning the stack variable
	a1 = new Address(a1.Line1,
		a1.Line2, "Ann Arbor", "MI", 48103);
}

// Immutable struct
public struct Address
{
	public string Line1 { get; }
	public string Line2 { get; }
	public string City { get; }
	public string State { get; }
	public int ZipCode { get; }
	
	public Address(string line1,
		string line2,
		string city,
		string state,
		int zipCode) : this() // The requirements of CS up to 4.5, otherwise cause CS0843 error "backing field must be fully initialized.
	{
		Line1 = line1;
		Line2 = line1;
		City = city;
		State = state;
		ZipCode = zipCode;
	}
}
