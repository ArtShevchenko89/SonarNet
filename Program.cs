using SonarNet.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine($"{SonarNet.AppInfo.Name} v{SonarNet.AppInfo.Version} — навчальний проєкт для аналізу якості коду");

var repository = new UserRepository();
repository.Add("admin", "Адміністратор системи", "admin@sonarnet.local");
repository.Add("student", "Студент", "student@sonarnet.local");
repository.Add("teacher", "Викладач", "teacher@sonarnet.local");

foreach (var user in repository.GetAll())
    Console.WriteLine(user);

var scores = new double[] { 72, 85, 90, 64, 78 };
Console.WriteLine($"Середнє значення балів: {StatisticsService.Average(scores):F2}");
Console.WriteLine($"Медіана: {StatisticsService.Median(scores):F2}");
Console.WriteLine($"Медіана: {StatisticsService.Median(scores):F2}");
Console.WriteLine($"Максимальний бал: {scores.Max():F2}");

var auth = new UserAuth(repository);
auth.SetPassword("admin", "Adm1n#Secret");
Console.WriteLine($"Вхід admin з правильним паролем: {auth.Authenticate("admin", "Adm1n#Secret")}");
Console.WriteLine($"Вхід admin з неправильним паролем: {auth.Authenticate("admin", "wrong-pass")}");
Console.WriteLine($"Вхід під неіснуючим логіном guest: {auth.Authenticate("guest", "any-pass")}");