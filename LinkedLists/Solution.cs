public class Solution
{
    public ListNode OddEvenList(ListNode head)
    {
        var oddDummie = new ListNode();
        var evenDummie = new ListNode();
        var even = evenDummie;
        var odd = oddDummie;
        var i = 1;
        for (var node = head; node != null; node = node.next)
        {
            if (i % 2 == 0)
            {
                even.next = node;
                even = even.next;
            }
            else
            {
                odd.next = node;
                odd = odd.next;
            }

            i++;
        }

        if (odd != null)
        {
            odd.next = evenDummie.next;
            even.next = null;
        }

        return oddDummie.next;
    }

    public ListNode? RotateRight(ListNode? head, int k)
    {
        if (head == null)
        {
            return null;
        }

        var last = head;
        int length = 1;
        while (last != null && last.next != null)
        {
            last = last.next;
            length++;
        }

        k %= length;
        if (k == 0)
        {
            return head;
        }

        var dummy = new ListNode(0, head);
        var prev = dummy;
        var split = head;
        for (var i = 0; i < length - k; i++)
        {
            prev = prev.next;
            split = split.next;
        }

        last.next = head;
        head = split;
        prev.next = null;
        return head;
    }

    public ListNode ReverseBetween(ListNode head, int left, int right)
    {
        if (left == right)
        {
            return head;
        }

        var dummy = new ListNode(0, head);
        var prevLeft = dummy;
        var leftNode = dummy;
        var prev = dummy;
        var current = head;
        for (var i = 1; i < right; i++)
        {
            if (i == left)
            {
                leftNode = current;
                prevLeft = prev;
            }

            prev = current;
            current = current.next;
        }

        var next = current.next;
        var stop = next;
        for (var node = leftNode; node != stop;)
        {
            var nextIter = node.next;
            node.next = next;
            next = node;
            node = nextIter;
        }

        prevLeft.next = current;

        return left > 1
            ? head
            : current;
    }

    public ListNode? Partition(ListNode? head, int x)
    {
        if (head == null)
        {
            return null;
        }

        var dummy = new ListNode(0, head);
        var lessHead = dummy;
        var lessCurrent = dummy;
        var prev = dummy;
        var node = head;
        while (node != null)
        {
            if (node.val < x)
            {
                if (lessHead == dummy)
                {
                    lessHead = node;
                    lessCurrent = node;
                }
                else
                {
                    lessCurrent.next = node;
                    lessCurrent = node;
                }

                prev.next = node.next;
            }
            else
            {
                prev = prev.next;
            }

            node = node.next;
        }

        lessCurrent.next = dummy.next;
        return lessHead == dummy
            ? dummy.next
            : lessHead;
    }

    public ListNode? SwapPairs(ListNode? head)
    {
        if (head == null)
        {
            return null;
        }

        var dummy = new ListNode(0, head);
        var prev = dummy;
        var current = head;
        while (current != null && current.next != null)
        {
            var tmp = current.next.next;
            prev.next = current.next;
            current.next.next = current;
            prev = current;
            prev.next = tmp;
            current = tmp;
        }

        return dummy.next;
    }

    public ListNode? DetectCycle(ListNode? head)
    {
        var slow = head;
        var fast = head;
        while (fast!=null)
        {
            fast = fast.next?.next;
            slow = slow?.next;
            if (slow == fast)
            {
                break;
            }
        }

        if (fast == null)
        {
            return null;
        }

        var node = head;
        while (node!=fast)
        {
            node = node?.next;
            fast = fast?.next;
        }

        return fast;
    }
    
    public bool HasCycle(ListNode? head)
    {
        var slow = head;
        var fast = head;
        while (fast!=null)
        {
            fast = fast.next?.next;
            slow = slow?.next;
            if (slow == fast)
            {
                break;
            }
        }

        return fast != null;
    }
}