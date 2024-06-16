namespace matrix.lib.test;
using FluentAssertions;

public class MatrixTests
{
    [Fact]
    public void Test1()
    {
        var graphLeft = new Dictionary<string, List<string>>
                                            {
                                                { "1", new List<string> { "2", "3", "4" } },
                                                { "2", new List<string> { "1", "3" } },
                                                { "3", new List<string> { "1", "2", "4" } },
                                                { "4", new List<string> { "1", "3" } }
                                            };

        var graphRight = new Dictionary<string, List<string>>
{
                                                { "1", new List<string> { "2", "3", "4" } },
                                                { "2", new List<string> { "1", "3" } },
                                                { "3", new List<string> { "1", "2", "4" } },
                                                { "4", new List<string> { "1", "3"} }
                                            };


        
        var mapping = ConvertUtils.BuildTermsMap(graphLeft, graphRight);                                            
        var encodedAdjacencyListLeft = ConvertUtils.BuildAdjacencySetUsingMapping(graphLeft, mapping);
        var encodedAdjacencyListRight = ConvertUtils.BuildAdjacencySetUsingMapping(graphRight, mapping);

        var jacardIndex = ConvertUtils.CalculateJaccardIndex(ConvertUtils.GetGraphEdges(encodedAdjacencyListLeft), 
                                                              ConvertUtils.GetGraphEdges(encodedAdjacencyListRight));

        var x = ConvertUtils.ConvertToAdjacencyMatrix(encodedAdjacencyListLeft, mapping.Keys.Count);
        var y = ConvertUtils.ConvertToAdjacencyMatrix(encodedAdjacencyListRight, mapping.Keys.Count);
        
        int z = ConvertUtils.CalculateGraphEditDistance(x, y);

    }
}