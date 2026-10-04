namespace SonarNet.Services;

public static class StatisticsService
{
    public static double Average(IEnumerable<double> values)
    {
        var list = values.ToList();
        if (list.Count == 0)
            throw new ArgumentException("Колекція не може бути порожньою", nameof(values));
        return list.Sum() / list.Count;
    }

    public static double Median(IEnumerable<double> values)
    {
        var sorted = values.OrderBy(v => v).ToList();
        if (sorted.Count == 0)
            throw new ArgumentException("Колекція не може бути порожньою", nameof(values));
        int mid = sorted.Count / 2;
        return sorted.Count % 2 == 0 ? (sorted[mid - 1] + sorted[mid]) / 2.0 : sorted[mid];
    }
}