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
                                                { "1", new List<string> { "2" } },
                                                { "2", new List<string> { } },
                                            };


        var mapping = ConvertUtils.BuildTermsMap(graphLeft, graphRight);                                            
        var encodedAdjacencyListLeft = ConvertUtils.BuildAdjacencyListUsingMapping(graphLeft, mapping);
        var encodedAdjacencyListRight = ConvertUtils.BuildAdjacencyListUsingMapping(graphRight, mapping);

        var t = ConvertUtils.ConvertToAdjacencyMatrix(encodedAdjacencyListRight, mapping.Keys.Count);

        ConvertUtils.ConvertToAdjacencyMatrix(encodedAdjacencyListLeft, mapping.Keys.Count).Should().BeEquivalentTo(new int[][]
        {
            [0, 1, 1, 1],
            [1, 0, 1, 0],
            [1, 1, 0, 1],
            [1, 0, 1, 0]
        });
    }
}