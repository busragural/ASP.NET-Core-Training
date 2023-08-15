namespace DependencyInjectionDemo.Logic;

public class OtherDemoLogic : IDemoLogic
{
    public int Value1 { get; private set; }
    public int Value2 { get; private set; }

    public OtherDemoLogic()
    {
        Value1 = 10;
        Value2 = 20;
    }
}
