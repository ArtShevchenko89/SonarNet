using SonarNet.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("SonarNet — навчальний проєкт для аналізу якості коду");

var repository = new UserRepository();
repository.Add("admin", "Адміністратор системи", "admin@sonarnet.local");
repository.Add("student", "Студент", "student@sonarnet.local");

foreach (var user in repository.GetAll())
    Console.WriteLine(user);

var scores = new double[] { 72, 85, 90, 64, 78 };
Console.WriteLine($"Середній бал: {StatisticsService.Average(scores):F2}");
Console.WriteLine($"Медіана: {StatisticsService.Median(scores):F2}");