namespace MountainArray;

class Solution
{
    public int FindInMountainArray(int target, MountainArray mountainArr)
    {
        var peak = FindPeak(mountainArr);

        var searchLeft = BinarySearch(target, 0, peak, mountainArr);
        if (searchLeft>=0)
        {
            return searchLeft;
        }
        return BinarySearch(target, peak+1, mountainArr.Length()-1, mountainArr, false);
    }

    private static int FindPeak(MountainArray mountainArr)
    {
        var length = mountainArr.Length();
        int left = 0, right = length-1;
        while (left < right)
        {
            var middle = left + (right-left) / 2;
            var curr = mountainArr.Get(middle);
            var next = mountainArr.Get(middle+1);
            if (curr < next)
                left = middle + 1;
            else
                right = middle;
        }

        return left;
    }

    private static int BinarySearch(int target, int left, int right, MountainArray array, bool increasing = true)
    {
        while (left < right)
        {
            var middle = left + (right-left) / 2;
            if (increasing && array.Get(middle) < target || !increasing && array.Get(middle) > target)
                left = middle + 1;
            else
                right = middle;
        }
        if(array.Get(left) == target)
        {
            return left;
        }

        return -1;
    }
    
}