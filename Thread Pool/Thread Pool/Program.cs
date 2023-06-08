using Thread_Pool;




object obj = new object();

CallCenter CS = new CallCenter(24);
CS.stopWatch.Start();
for (int i = 0; i <26; i++)
{
    CS.Call();
}
Console.ReadLine();
CS.stopWatch.Restart();
//CS.CallAlt(240);

Console.ReadLine();
CS.stopWatch.Stop();
Console.WriteLine(CS.ToString());
Console.ReadLine();