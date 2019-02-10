using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithm
{
    public static class Search
    {
        /// <summary>
        /// 二分查找(时间复杂度O(logn),空间复杂度O(1))
        /// </summary>
        /// <param name="array"></param>
        /// <param name="target"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static int BinarySearch(this long[] array, long target, IComparer<long> comparer = null)
        {
            if (array.Any())
            {
                var minIndex = 0;
                var maxIndex = array.Length - 1;

                var midIndex = minIndex + (Math.Abs(maxIndex - minIndex) >> 1);

                if (comparer != null)
                {
                    while (minIndex <= maxIndex)
                    {
                        var compareResult = comparer.Compare(array[midIndex], target);
                        if (compareResult == 0)
                        {
                            return midIndex;
                        }
                        else if (compareResult < 0)
                        {
                            minIndex = midIndex + 1;
                        }
                        else
                        {
                            maxIndex = midIndex - 1;
                        }

                        midIndex = minIndex + (Math.Abs(maxIndex - minIndex) >> 1);
                    }
                }
                else
                {
                    while (minIndex <= maxIndex)
                    {
                        if (array[midIndex] == target)
                        {
                            return midIndex;
                        }
                        else if(array[midIndex] < target)
                        {
                            minIndex = midIndex + 1;
                        }
                        else
                        {
                            maxIndex = midIndex - 1;
                        }

                        midIndex = minIndex + (Math.Abs(maxIndex - minIndex) >> 1);
                    }
                }

                return ~midIndex;
            }

            return -1;
        }
    }
}
