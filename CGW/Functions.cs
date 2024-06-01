namespace CGW;

public static class Functions
{
    public static List<(int, int)> ReadFromFile(string path)
    {
        using var sr = new StreamReader(path);
        var file = sr.ReadToEnd().Trim('\n').Split('\n');
        var fileName = Path.GetFileName(path);
        
        var data = new List<(int, int)>(); // (e; a)
        for (int i = 0; i < file.Length; i++)
        {
            var line = file[i].Split(' ');
            
            if (line.Length != 2)
                throw new ArgumentException($"{fileName} - Строка {i+1}: Количество элементов в строке не равно 2.");
            
            if (!int.TryParse(line[0], out var e))
                throw new ArgumentException($"{fileName} - Строка {i+1}: Не удалось преобразовать первый элемент (e) в число.");

            if (!int.TryParse(line[1], out var a))
                throw new ArgumentException($"{fileName} - Строка {i+1}: Не удалось преобразовать второй элемент (a) в число.");
            
            if (a < 0)
                throw new ArgumentException($"{fileName} - Строка {i+1}: Второй элемент (a) должен быть больше или равен 0.");

            if (e - a < 0)
                throw new ArgumentException($"{fileName} - Строка {i+1}: e - a должно быть больше или равно 0.");
            
            data.Add((e, a));
        }

        return data;
    }

    public static int CenterOfGravityMethod(List<ProbTermPair> fuzzySet)
    {
        float result = 0;

        var denominator = fuzzySet.Select(x => x.Membership).Sum();
        foreach (var pair in fuzzySet)
        {
            result += pair.Term * pair.Membership / denominator;
        }

        return (int)Math.Round(result);
    }
    
    public static float TriangularMembershipFunc(int x, int e, int a)
    {
        var w = e - a <= x && x < e + a ? 1 : 0;
        return w * (a - Math.Abs(x - e)) / (float)a;
    }
}