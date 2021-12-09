namespace JumpGame3;

public class Solution
{
    public bool CanReach(int[] arr, int start)
    {
        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(start);
        while(queue.Count>0)
        {
            var index = queue.Dequeue();
            if(arr[index]==0)
            {
                return true;
            }
            visited.Add(index);
            var jumpPlus = index + arr[index];
            var jumpMinus = index - arr[index];
            if(jumpPlus<arr.Length && !visited.Contains(jumpPlus))
            {
                queue.Enqueue(jumpPlus);
            }
            if(jumpMinus >= 0 && !visited.Contains(jumpMinus))
            {
                queue.Enqueue(jumpMinus);
            }
        }
        return false;
    }
}