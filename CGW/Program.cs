using CGW;

Console.WriteLine(
    "Инфо:\n" +
    "- Для определения нечеткого множества используется треугольная функция принадлежности.\n" +
    "- Для деффазификации используется метод центра тяжести.\n");

Console.WriteLine("Основные данные:\n");
var clearNumbers = GetClearNumbers("input.txt");
if (clearNumbers.Count < 7)
{
    Console.WriteLine("В входном файле должно быть минимум 7 пар чисел.");
    Console.ReadLine();
    return;
}

var tree = new BinaryTree(clearNumbers);

Console.WriteLine("\nДерево, до добавления узлов:");
tree.Print();

Console.WriteLine("\nДополнительные данные:\n");
tree.InsertRange(GetClearNumbers("addition.txt"));

Console.WriteLine("\nДерево, после добавления узлов:");
tree.Print();

Console.WriteLine("\nДанные для поиска:\n");
var searchNumbers = GetClearNumbers("search.txt");

Console.WriteLine("Результаты поиска:\n");
foreach (var number in searchNumbers)
{
    Console.WriteLine($"Искомое число: {number}");
    List<int>? res = tree.Search(number);
    if (res == null)
    {
        Console.WriteLine($"Результат поиска: Число {number} не найдено.");
    }
    else
    {
        Console.WriteLine($"Результат поиска: Число {number} найдено.");
        Console.WriteLine($"Путь к числу: {string.Join(" -> ", res)}");
    }
    Console.WriteLine();
}

Console.ReadLine();
return;


List<int> GetClearNumbers(string filename)
{
    // Read data from file
    List<(int, int)> data = new(); // (e; a)

    try
    {
        data = Functions.ReadFromFile(filename);
    }
    catch (ArgumentException e)
    {
        Console.WriteLine(e.Message);
        Console.ReadLine();
        Environment.Exit(1);
    }

    // Calculate list of clear numbers
    List<int> result = new();
    foreach (var (e, a) in data)
    {
        Console.WriteLine($"e = {e}, a = {a};");

        int start = e - a;
        int end = e + a;

        // Calculate fuzzy set [i]
        var fuzzySet = new List<TermMembPair>();
        for (int i = start; i <= end; i++)
        {
            var membership = Functions.TriangularMembershipFunc(i, e, a);
            fuzzySet.Add(new TermMembPair(i, membership));
        }

        // Defuzzification
        var clearNumber = Functions.CenterOfGravityMethod(fuzzySet);
        Console.WriteLine($"Четкое число: {clearNumber}\n");

        result.Add(clearNumber);
    }

    return result;
}