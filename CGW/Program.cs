using CGW;

var filename = "input.txt";
var fuzzySet = Functions.ReadFromFile(filename);

if (fuzzySet == null)
{
    Console.WriteLine($"Некорректные данные в файле {filename}");
    Console.ReadLine();
    return;
}


if (fuzzySet.Count < 7)
{
    Console.WriteLine($"Количество элементов в файле {filename} не может быть меньше 7-ми.");
    Console.ReadLine();
    return;
}

var clearNumber = Functions.CenterOfGravityMethod(fuzzySet);
Console.ReadLine();
