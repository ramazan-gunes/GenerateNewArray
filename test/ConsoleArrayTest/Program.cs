using RamCore;

var classicArray = new[] { 1, 2, 3, 4, 5, 6, 7 };

RamArray<int> ramArray = classicArray; // implicit operator


Console.WriteLine("-- New Case NextItem --");

while (ramArray.NextItem())
{
    Console.WriteLine($"Next Item {ramArray.Current}");
}

Console.WriteLine("-- New Case PreviousItem --");

while (ramArray.PreviousItem())
{
    Console.WriteLine($"Previous Item {ramArray.Current}");
}

Console.WriteLine("-- New Case Foreach --");

foreach (var item in ramArray)
{
    Console.WriteLine($"Foreach {item}");
}

Console.WriteLine("-- New Case Add --");

ramArray.Add(100);
ramArray.Add(101);

foreach (var item in ramArray)
{
    Console.WriteLine(ramArray.Current + " Add ");
}

Console.WriteLine("-- New Case AddRange --");

ramArray.AddRange(new[] { 1000, 1001 });


foreach (var item in ramArray)
{
    Console.WriteLine(ramArray.Current + " Add ");
}

Console.WriteLine("-- New Case RemoveAt --");

ramArray.RemoveAt(0);

foreach (var item in ramArray)
{
    Console.WriteLine(ramArray.Current + " Add ");
}



Console.ReadLine();
