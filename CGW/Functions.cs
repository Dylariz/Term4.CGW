namespace CGW;

public static class Functions
{
    public static List<ProbTermPair>? ReadFromFile(string path, int step = 1)
    {
        var sr = new StreamReader(path);
        var file = sr.ReadToEnd().Split('\n');
        
        if (file.Length != 2)
            return null;

        var probabilities = file[0].Split(' ');
        var terms = file[1].Split(' ');
        
        if (probabilities.Length != terms.Length)
            return null;

        var fuzzySet = new List<ProbTermPair>(); // (probability; term)
        for (int i = 0; i < terms.Length; i++)
        {
            if (!float.TryParse(probabilities[i], out var prob))
                return null;

            if (prob is > 1 or < 0)
                return null;

            if (!int.TryParse(terms[i], out var term))
                return null;
            
            fuzzySet.Add(new ProbTermPair(prob, term));

            if (i != 0 && fuzzySet[i].Term != fuzzySet[i-1].Term + step)
                return null;
        }

        return fuzzySet;
    }

    public static int CenterOfGravityMethod(List<ProbTermPair> fuzzySet)
    {
        float result = 0;

        var denominator = fuzzySet.Select(x => x.Probability).Sum();
        foreach (var pair in fuzzySet)
        {
            result += pair.Term * pair.Probability / denominator;
        }

        return (int)Math.Round(result);
    }
}