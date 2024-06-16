namespace matrix.lib;

public record struct GraphAdjancenyData(Dictionary<int, List<int>> AdjacencyList, Dictionary<int, string> Mapping);



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
 
    public static Dictionary<int, List<int>> BuildAdjacencyListUsingMapping(Dictionary<string, List<string>> data, 
                                                                            Dictionary<int, string> mapping)
    {
        var reversed = mapping.ToDictionary(x => x.Value, x => x.Key);
        var encodedDictionary = data.ToDictionary(x => reversed[x.Key], x => x.Value.Select(v => reversed[v]).ToList());
        return encodedDictionary;
    }

    public static int[][] ConvertToAdjacencyMatrix(Dictionary<int, List<int>> encodedAdjacencyList, int matrixDimension)
    {
        var matrix = new int[matrixDimension][];
        foreach(var i in Enumerable.Range(1, matrixDimension))
            matrix[i-1] = Enumerable.Range(1, matrixDimension)
                .Select(j => encodedAdjacencyList.ContainsKey(i) && i <= encodedAdjacencyList.Count && encodedAdjacencyList[i].Contains(j) ? 1 : 0)
                .ToArray();
        return matrix;
    }
   
}
