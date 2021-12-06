public class ListNode
{
    public int val;
    public ListNode? next;
    public ListNode(int val=0, ListNode? next=null)
    {
        this.val = val;
        this.next = next;
    }

    public static ListNode? Create(int[]? values)
    {
        if (values == null)
        {
            return null;
        }
        var dummy = new ListNode();
        var current = dummy;
        foreach (var value in values)
        {
            var node = new ListNode(value);
            current.next = node;
            current = current.next;
        }

        return dummy.next;
    }

    public static int[] ToArray(ListNode? head)
    {
        var result = new List<int>();
        for (var node = head; node != null; node = node.next)
        {
            result.Add(node.val);
        }

        return result.ToArray();
    }
}