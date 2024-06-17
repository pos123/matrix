namespace matrix.lib;

public record struct Edge(int From, int To);

public class ConvertUtils
{
    public static Dictionary<int, string> BuildTermsMap(Dictionary<string, List<string>> left, Dictionary<string, List<string>> right)
    {
        var terms = left.Keys.Union(left.Values.SelectMany(x => x))
            .Union(right.Keys.Union(right.Values.SelectMany(x => x)))
            .Distinct()
            .OrderBy(x => x)
            .ToList();
        return terms.Select((x, i) => new { Key = i + 1, Value = x }).ToDictionary(x => x.Key, x => x.Value);
    }
 
    public static Dictionary<int, HashSet<int>> BuildAdjacencySetUsingMapping(Dictionary<string, List<string>> data, 
                                                                            Dictionary<int, string> mapping)
    {
        var reversed = mapping.ToDictionary(x => x.Value, x => x.Key);
        var encodedDictionary = data.ToDictionary(x => reversed[x.Key], x => x.Value.Select(v => reversed[v]).ToHashSet());
        return encodedDictionary;
    }

    public static int[][] ConvertToAdjacencyMatrix(Dictionary<int, HashSet<int>> encodedAdjacencyList, int matrixDimension)
    {
        var matrix = new int[matrixDimension][];
        foreach(var i in Enumerable.Range(1, matrixDimension))
            matrix[i-1] = Enumerable.Range(1, matrixDimension)
                .Select(j => encodedAdjacencyList.ContainsKey(i) && 
                encodedAdjacencyList[i].Contains(j) ? 1 : 0)
                .ToArray();
        return matrix;
    }

    public static int CalculateGraphEditDistance(int[][] leftMatrix, int[][] rightMatrix)
    {
        int totalDifference = 0;
        for (int i = 0; i < leftMatrix.Length; i++)
            for (int j = 0; j < leftMatrix[i].Length; j++)
               totalDifference += Math.Abs(leftMatrix[i][j] - rightMatrix[i][j]);
        return totalDifference;
    }
    
    public static List<Edge> GetGraphEdges(Dictionary<int, HashSet<int>> encodedAdjacencyList) =>
        encodedAdjacencyList.SelectMany(x => x.Value.Select(y => new Edge(x.Key, y))).ToList();

    public static double CalculateJaccardIndex(IEnumerable<Edge> edges1, IEnumerable<Edge> edges2)
    {
        var set1 = new HashSet<Edge>(edges1);
        var set2 = new HashSet<Edge>(edges2);

        var intersection = new HashSet<Edge>(set1);
        intersection.IntersectWith(set2);
        var union = new HashSet<Edge>(set1);
        union.UnionWith(set2);

        return (union.Count == 0) ? 1 : (double)intersection.Count / union.Count;
    }    
}
