using Thread_Pool;




object obj = new object();

CallCenter CS = new CallCenter(24);
for (int i = 0; i <26; i++)
{
    CS.Call();
}

Console.ReadLine();