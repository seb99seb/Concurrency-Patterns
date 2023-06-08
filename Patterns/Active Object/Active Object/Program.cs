using Active_Object;

#region parallelism
RandomStringsParallel parallelString = new RandomStringsParallel();
parallelString.CatFactAsync();
parallelString.BoredAsync();
Thread.Sleep(1000);
Console.WriteLine(parallelString);
#endregion

Console.WriteLine("\n\n");

#region queue test
RandomStrings queueTest = new RandomStrings();
queueTest.CatFactAsync();
Thread.Sleep(700);
Console.WriteLine(queueTest);
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
queueTest.CatFactAsync();
Thread.Sleep(3000);
Console.WriteLine(queueTest);
#endregion

Console.WriteLine("\n\n");

#region Loop w/ 'menu'
RandomStrings mainstring = new RandomStrings();
ConsoleKeyInfo level;
while(true)
{
    Console.WriteLine(mainstring);
    Console.WriteLine();
    Console.WriteLine("enter '1' to get a cat fact\n" +
        "enter '2' to get something to do\n" +
        "enter '3' to get your IP\n" +
        "enter '4' to refresh string");
    level = Console.ReadKey();
    Console.WriteLine();
    switch (level.KeyChar)
    {
        case '1':
            mainstring.CatFactAsync();
            break;
        case '2':
            mainstring.BoredAsync();
            break;
        case '3':
            mainstring.IpAsync();
            break;
        case '4':
            break;
        default: 
            Console.WriteLine("Invalid string entered");
            break;
    }
}
#endregion