class Solution
{
    public int FindInMountainArray(int target, MountainArray.MountainArray mountainArr)
    {
        var peak = FindPeak(mountainArr);

        var searchLeft = BinarySearch(target, 0, peak, mountainArr);
        if (searchLeft>=0)
        {
            return searchLeft;
        }
        return BinarySearch(target, peak+1, mountainArr.Length()-1, mountainArr, false);
    }

    private int FindPeak(MountainArray.MountainArray mountainArr)
    {
        var length = mountainArr.Length();
        int l = 0, r = length-1;
        while (l < r)
        {
            var mid = l + (r-l) / 2;
            var curr = mountainArr.Get(mid);
            var next = mountainArr.Get(mid+1);
            if (curr < next)
                l = mid + 1;
            else
                r = mid;
        }

        return l - (length % 2 == 0 ? 0 : 1);
    }

    private int BinarySearch(int target, int l, int r, MountainArray.MountainArray array, bool increasing = true)
    {
        while (l < r)
        {
            var mid = l + (r-l) / 2;
            if (increasing && array.Get(mid) < target || !increasing && array.Get(mid) > target)
                l = mid + 1;
            else
                r = mid;
        }
        if(array.Get(l) == target)
        {
            return l;
        }

        return -1;
    }
    
}