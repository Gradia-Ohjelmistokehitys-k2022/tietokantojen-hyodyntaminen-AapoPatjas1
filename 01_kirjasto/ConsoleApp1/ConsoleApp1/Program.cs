using ConsoleApp1.Models;

Console.WriteLine(  "test");
var dataRepository = new DatabaseRepository();
Console.WriteLine( dataRepository.IsDbConnectionEstablished());
//var Books = dataRepository.GetAllBooksLastFiveYears();
//foreach (var book in Books)

//{
//    Console.WriteLine( book );
//}
//dataRepository.GetAverageMemberAge();

//dataRepository.GetMostAvailableBook();
dataRepository.GetMembersWithLoans();