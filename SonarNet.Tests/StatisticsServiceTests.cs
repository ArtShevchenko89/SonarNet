using SonarNet.Services;

namespace SonarNet.Tests;

public class StatisticsServiceTests
{
    [Fact]
    public void Average_ReturnsArithmeticMean()
    {
        Assert.Equal(77.8, StatisticsService.Average(new double[] { 72, 85, 90, 64, 78 }), 3);
    }

    [Theory]
    [InlineData(new double[] { 3, 1, 2 }, 2)]
    [InlineData(new double[] { 4, 1, 3, 2 }, 2.5)]
    public void Median_OddAndEvenCount(double[] values, double expected)
    {
        Assert.Equal(expected, StatisticsService.Median(values), 3);
    }

    [Fact]
    public void Average_EmptyCollection_Throws()
    {
        Assert.Throws<ArgumentException>(() => StatisticsService.Average(Array.Empty<double>()));
    }
}