namespace MountainArray;

public class MountainArray
{
    private readonly int[] _numbers;
    public int QueryCount { get; private set; }
    public MountainArray(int[] numbers)
    {
        _numbers = numbers;
    }

    public int Get(int index)
    {
        QueryCount++;
        return _numbers[index];
    }

    public int Length()
    {
        return _numbers.Length;
    }
}