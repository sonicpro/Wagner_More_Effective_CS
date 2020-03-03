<Query Kind="Program">
  <NuGetReference>System.Collections.Immutable</NuGetReference>
  <Namespace>System.Collections.Immutable</Namespace>
</Query>

void Main()
{
	string[] phones = new string[10];
	
	PhoneList pl = new PhoneList(phones);

	Console.WriteLine($"Is the object state correct? {(pl.Phones.Skip(4).Take(1).First() == null ? "Yes" : "No")}");

	if (pl.Phones is string[] phonesArray)
	{
		((string[])pl.Phones)[4] = "456-789-9999";
	}
	Console.WriteLine($"Is the object state correct? {(pl.Phones.Skip(4).Take(1).First() == null ? "Yes" : "No")}");
	
	//**********************************************************//
	string[] referenceToNewPhones = new string[10];
	
	FortifiedPhoneList phoneListImmutable = new FortifiedPhoneList(referenceToNewPhones);
	
	Console.WriteLine($"Is the object state correct? {(phoneListImmutable.Phones.Skip(4).Take(1).First() == null ? "Yes" : "No")}");
	
	if (phoneListImmutable.Phones is ImmutableList<string> phonesImmutable)
	{
		phonesImmutable.SetItem(4, "456-789-9999"); // returns a new ImmutableList<string> rather than mutate the original one.
	}
	Console.WriteLine($"Is the object state correct? {(phoneListImmutable.Phones.Skip(4).Take(1).First() == null ? "Yes" : "No")}");
}

// The internal data of the struct can be currupted by accessing the field of a reference type.
public struct PhoneList
{
	private readonly string[] phones;
	
	public PhoneList(string[] ph)
	{
		phones = ph;
	}
	
	public IEnumerable<string> Phones => phones;
}

// Preventing reference type mutations by using System.Collections.Immutable namespace
public struct FortifiedPhoneList
{
	private readonly ImmutableList<string> phones;

	public FortifiedPhoneList(string[] ph)
	{
		phones = ph.ToImmutableList();
	}

	public IEnumerable<string> Phones => phones;
}