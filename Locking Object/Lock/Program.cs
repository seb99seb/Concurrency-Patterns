using Lock;

Console.WriteLine("No lock test:\n");

#region Dice test without using lock
Die LocklessDiceTest = new Die();

for (int i = 0; i < 3; i++)
{
    new Thread(() =>
    {
        LocklessDiceTest.LocklessRoll();
    }).Start();
}

Thread.Sleep(500);
Console.WriteLine(LocklessDiceTest);
#endregion

Console.WriteLine("\nLock test:\n");

#region Dice test using lock
Die LockDiceTest = new Die();

for (int i = 0; i < 3; i++)
{
    new Thread(() =>
    {
        LockDiceTest.Roll();
    }).Start();
}

Thread.Sleep(500);
Console.WriteLine(LockDiceTest);
#endregion

Console.WriteLine("\nDeadlock test:\n");

#region Deadlock test
AccumulatedValue value1 = new AccumulatedValue(0);
AccumulatedValue value2 = new AccumulatedValue(0);
DieTransfer DeadlockTest = new DieTransfer();

for (int i = 0; i < 3; i++)
{
    new Thread(() =>
    {
        DeadlockTest.Roll(value1);
    }).Start();

}

Thread.Sleep(500);
Console.WriteLine(value1);
Console.WriteLine();

for (int i = 0; i < 3; i++)
{
    new Thread(() =>
    {
        DeadlockTest.Roll(value2);
    }).Start();

}

Thread.Sleep(500);
Console.WriteLine(value2);

Console.WriteLine("");

new Thread(() =>
{
    DeadlockTest.Transfer(value1, value2);

}).Start();
//Thread.Sleep(100);
new Thread(() =>
{
    DeadlockTest.Transfer(value2, value1);

}).Start();

Thread.Sleep(1000);
Console.WriteLine(value1);
Console.WriteLine(value2);
//now transfer stuff
#endregion