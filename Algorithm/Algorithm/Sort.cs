using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithm
{
    public static class Sort
    {
        #region 冒泡排序

        /// <summary>
        /// 冒泡排序(时间复杂度O(n2),稳定排序,原地排序)
        /// 两两交换数据的原地排序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static void BubbleSort(this long[] array, bool desc = false)
        {
            var sortCount = 1;

            if (!desc)
            {
                //升序
                while (sortCount <= array.Length - 1)
                {
                    //标记位，确认是否还需要继续
                    var hasSwap = false;
                    if (array.Length > 1)
                    {
                        //判断第一个数
                        if (array[0] > array[1])
                        {
                            var temp = array[0];
                            array[0] = array[1];
                            array[1] = temp;
                        }
                    }

                    for (var i = 1; i < array.Length - sortCount; i++)
                    {
                        var next = i + 1;
                        if (array[i] > array[next])
                        {
                            var temp = array[i];
                            array[i] = array[next];
                            array[next] = temp;
                            hasSwap = true;
                        }
                    }

                    if (!hasSwap)
                    {
                        break;
                    }

                    sortCount++;
                }
            }
            else
            {
                //降序
                while (sortCount <= array.Length - 1)
                {
                    //标记位，确认是否还需要继续
                    var hasSwap = false;
                    if (array.Length > 1)
                    {
                        //判断第一个数
                        if (array[0] < array[1])
                        {
                            var temp = array[0];
                            array[0] = array[1];
                            array[1] = temp;
                        }
                    }

                    for (var i = 0; i < array.Length - sortCount; i++)
                    {
                        var next = i + 1;
                        if (array[i] < array[next])
                        {
                            var temp = array[i];
                            array[i] = array[next];
                            array[next] = temp;
                            hasSwap = true;
                        }
                    }

                    if (!hasSwap)
                    {
                        break;
                    }

                    sortCount++;
                }
            }
        }

        #endregion

        #region 选择排序

        /// <summary>
        /// 选择排序(时间复杂度O(n2),非稳定排序,原地排序)
        /// 将待排序数据分为已排序和未排序两部分，从未排序部分找最小值和已排序后一位交换，涉及位置交换，所以是不稳定排序算法
        /// </summary>
        /// <param name="array"></param>
        /// <param name="desc"></param>
        public static void SelectedSort(this long[] array, bool desc = false)
        {
            //未排序部分起始索引
            var noSortIndex = 0;

            if (!desc)
            {
                //从小到大
                //遍历未排序部分
                while (noSortIndex < array.Length)
                {
                    var minValue = array[noSortIndex];
                    var minValueIndexArray = new List<int> {noSortIndex};

                    for (var i = noSortIndex + 1; i < array.Length; i++)
                    {
                        if (array[i] < minValue)
                        {
                            minValue = array[i];
                            minValueIndexArray = new List<int> {i};
                        }
                        else if (array[i] == minValue)
                        {
                            //相同最小数
                            minValueIndexArray.Add(i);
                        }
                    }

                    for (var i = 0; i < minValueIndexArray.Count; i++)
                    {
                        var toBeInsertIndex = minValueIndexArray[i];
                        var toBeInsert = array[toBeInsertIndex];
                        var temp = array[noSortIndex];
                        array[noSortIndex] = toBeInsert;
                        array[toBeInsertIndex] = temp;
                        noSortIndex++;
                    }
                }
            }
            else
            {
                //从小到大
                //遍历未排序部分
                while (noSortIndex < array.Length)
                {
                    var minValue = array[noSortIndex];
                    var minValueIndexArray = new List<int> {noSortIndex};

                    for (var i = noSortIndex + 1; i < array.Length; i++)
                    {
                        if (array[i] > minValue)
                        {
                            minValue = array[i];
                            minValueIndexArray = new List<int> {i};
                        }
                        else if (array[i] == minValue)
                        {
                            //相同最大数
                            minValueIndexArray.Add(i);
                        }
                    }

                    for (var i = 0; i < minValueIndexArray.Count; i++)
                    {
                        var toBeInsertIndex = minValueIndexArray[i];
                        var toBeInsert = array[toBeInsertIndex];
                        var temp = array[noSortIndex];
                        array[noSortIndex] = toBeInsert;
                        array[toBeInsertIndex] = temp;
                        noSortIndex++;
                    }
                }
            }
        }

        #endregion

        #region 插入排序

        /// <summary>
        /// 插入排序(时间复杂度O(n2),稳定排序,原地排序)
        /// 将待排序数据分为已排序和未排序两部分，将未排序部分逐一插入已排序部分，比冒泡节省交换数据的两步操作
        /// </summary>
        /// <param name="array"></param>
        /// <param name="desc"></param>
        public static void InsertSort(this long[] array, bool desc = false)
        {
            //未排序部分起始索引
            var noSortIndex = 1;

            if (!desc)
            {
                //从小到大
                //遍历未排序部分
                for (var i = noSortIndex; i < array.Length; i++)
                {
                    //遍历已排序部分，比较
                    var toBeSort = array[i];
                    int findIndex;
                    if (toBeSort >= array[i - 1])
                    {
                        //比已排序部分最大数都大
                        findIndex = noSortIndex - 1;
                    }
                    else if (toBeSort < array[0])
                    {
                        //比已排序部分最小数都小
                        findIndex = -1;
                    }
                    else
                    {
                        findIndex = Array.BinarySearch(array, 0, noSortIndex - 0, toBeSort);
                    }

                    if (findIndex >= 0)
                    {
                        //找到数据
                        //向后遍历找到真正索引
                        while (findIndex + 1 < noSortIndex)
                        {
                            if (array[findIndex] == array[findIndex + 1])
                            {
                                findIndex++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        findIndex++;
                    }
                    else
                    {
                        findIndex = ~findIndex;
                    }

                    //开始搬移数据
                    for (int j = noSortIndex; j > findIndex; j--)
                    {
                        array[j] = array[j - 1];
                    }

                    array[findIndex] = toBeSort;

                    noSortIndex++;
                }
            }
            else
            {
                //从大到小
                //遍历未排序部分
                for (var i = noSortIndex; i < array.Length; i++)
                {
                    //遍历已排序部分，比较
                    var toBeSort = array[i];
                    int findIndex;
                    if (toBeSort <= array[i - 1])
                    {
                        //比已排序部分最小数都小
                        findIndex = noSortIndex - 1;
                    }
                    else if (toBeSort > array[0])
                    {
                        //比已排序部分最大数都大
                        findIndex = -1;
                    }
                    else
                    {
                        findIndex =
                            Array.BinarySearch(array, 0, noSortIndex - 0, toBeSort, new ReverseComparer<long>());
                    }

                    if (findIndex >= 0)
                    {
                        //找到数据
                        //向后遍历找到真正索引
                        while (findIndex + 1 < noSortIndex)
                        {
                            if (array[findIndex] == array[findIndex + 1])
                            {
                                findIndex++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        findIndex++;
                    }
                    else
                    {
                        findIndex = ~findIndex;
                    }

                    //开始搬移数据
                    for (int j = noSortIndex; j > findIndex; j--)
                    {
                        array[j] = array[j - 1];
                    }

                    array[findIndex] = toBeSort;

                    noSortIndex++;
                }
            }
        }

        #endregion

        #region 归并排序

        /// <summary>
        /// 归并排序(时间复杂度O(n),稳定排序,非原地排序)
        /// 将数据规模除2，分步排序，最后合并，合并时需要额外存储空间O(n)，非原地排序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="desc"></param>
        public static void MergeSort(this long[] array, bool desc = false)
        {
            MergeSortInner(array, 0, array.Length - 1, desc);
        }

        /// <summary>
        /// 归并排序内部递归操作(时间复杂度O(nlogn),稳定排序,非原地排序)
        /// 将数据规模除2，分步排序，最后合并，合并时需要额外存储空间O(n)，非原地排序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="desc"></param>
        private static void MergeSortInner(this long[] array, int startIndex, int endIndex, bool desc = false)
        {
            if (startIndex >= endIndex)
            {
                return;
            }

            //均分数组，递归排序(取半操作，防止溢出，移位比除法更快)
            var midIndex = startIndex + ((endIndex - startIndex) >> 1);
            MergeSortInner(array, startIndex, midIndex, desc);
            MergeSortInner(array, midIndex + 1, endIndex, desc);

            //合并
            var mergeTemp = new long[endIndex - startIndex + 1];

            if (!desc)
            {
                var copyIndex = 0;

                //从小到大排列
                int rightStartIndex = midIndex + 1;
                var rightMin = array[rightStartIndex];

                int leftStartIndex = startIndex;
                for (var i = midIndex; i >= startIndex; i--)
                {
                    //左边的最大数小于等于右边的最小数，不用再继续比较了
                    if (array[i] <= rightMin)
                    {
                        //找到
                        leftStartIndex = i + 1;
                        break;
                    }
                }

                //拷贝数组
                for (int i = startIndex; i < leftStartIndex; i++)
                {
                    mergeTemp[copyIndex] = array[i];
                    copyIndex++;
                }

                //比较并拷贝剩余数组
                for (int i = leftStartIndex; i <= midIndex && rightStartIndex <= endIndex; i++)
                {
                    for (int j = rightStartIndex; j <= endIndex; j++)
                    {
                        if (array[i] > array[j])
                        {
                            mergeTemp[copyIndex] = array[j];
                            copyIndex++;
                            rightStartIndex++;
                        }
                        else
                        {
                            mergeTemp[copyIndex] = array[i];
                            copyIndex++;
                            leftStartIndex++;
                            break;
                        }
                    }
                }

                //拷贝剩余数组
                for (int i = leftStartIndex; i <= midIndex; i++)
                {
                    mergeTemp[copyIndex] = array[i];
                    copyIndex++;
                }

                for (int i = rightStartIndex; i <= endIndex; i++)
                {
                    mergeTemp[copyIndex] = array[i];
                    copyIndex++;
                }

                mergeTemp.CopyTo(array, startIndex);
            }
            else
            {
                var copyIndex = 0;

                //从大到小排列
                int rightStartIndex = midIndex + 1;
                var rightMax = array[rightStartIndex];

                int leftStartIndex = startIndex;
                for (var i = midIndex; i >= startIndex; i--)
                {
                    //左边的最小数大于等于右边的最大数，不用再继续比较了
                    if (array[i] >= rightMax)
                    {
                        //找到
                        leftStartIndex = i + 1;
                        break;
                    }
                }

                //拷贝数组
                for (int i = startIndex; i < leftStartIndex; i++)
                {
                    mergeTemp[copyIndex] = array[i];
                    copyIndex++;
                }

                //比较并拷贝剩余数组
                for (int i = leftStartIndex; i <= midIndex && rightStartIndex <= endIndex; i++)
                {
                    for (int j = rightStartIndex; j <= endIndex; j++)
                    {
                        if (array[i] < array[j])
                        {
                            mergeTemp[copyIndex] = array[j];
                            copyIndex++;
                            rightStartIndex++;
                        }
                        else
                        {
                            mergeTemp[copyIndex] = array[i];
                            copyIndex++;
                            leftStartIndex++;
                            break;
                        }
                    }
                }

                //拷贝剩余数组
                for (int i = leftStartIndex; i <= midIndex; i++)
                {
                    mergeTemp[copyIndex] = array[i];
                    copyIndex++;
                }

                for (int i = rightStartIndex; i <= endIndex; i++)
                {
                    mergeTemp[copyIndex] = array[i];
                    copyIndex++;
                }

                mergeTemp.CopyTo(array, startIndex);
            }
        }

        #endregion

        #region 快速排序

        /// <summary>
        /// 快速排序(时间复杂度O(nlogn),非稳定排序,原地排序)
        /// 选择任意pivot分区点，不停的分区迭代，涉及数据交换，非稳定排序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="desc"></param>
        public static void QuickSort(this long[] array, bool desc = false)
        {
            QuickSortInner(array, 0, array.Length - 1, desc);
        }

        /// <summary>
        /// 快速排序(时间复杂度O(nlogn),非稳定排序,原地排序)
        /// 选择任意pivot分区点，不停的分区迭代，涉及数据交换，非稳定排序
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="desc"></param>
        private static void QuickSortInner(this long[] array, int startIndex, int endIndex, bool desc = false)
        {
            if (startIndex >= endIndex)
            {
                return;
            }

            //使用三点法选择pivot
            long pivotValue;
            int pivotIndex;

            #region 选择pivot

            var start = array[startIndex];
            var midIndex = startIndex + (endIndex - startIndex) >> 1;
            var mid = array[midIndex];
            var end = array[endIndex];
            if (start > mid)
            {
                if (mid > end)
                {
                    //选mid
                    pivotValue = mid;
                    pivotIndex = midIndex;
                }
                else
                {
                    if (start > end)
                    {
                        //选end
                        pivotValue = end;
                        pivotIndex = endIndex;
                    }
                    else
                    {
                        //选Start
                        pivotValue = start;
                        pivotIndex = startIndex;
                    }
                }
            }
            else
            {
                if (start > end)
                {
                    //选start
                    pivotValue = start;
                    pivotIndex = startIndex;
                }
                else
                {
                    if (end > mid)
                    {
                        //选mid
                        pivotValue = mid;
                        pivotIndex = midIndex;
                    }
                    else
                    {
                        //选end
                        pivotValue = end;
                        pivotIndex = endIndex;
                    }
                }
            }

            //将pivot放到最后
            array[pivotIndex] = end;
            array[endIndex] = pivotValue;

            #endregion

            //已处理数据索引
            var handedIndex = startIndex;

            //未处理数据索引
            var unHandedIndex = startIndex;

            if (!desc)
            {
                for (int i = unHandedIndex; i < endIndex; i++)
                {
                    if (array[unHandedIndex] <= pivotValue)
                    {
                        //进行数据交换
                        var temp = array[i];
                        array[i] = array[handedIndex];
                        array[handedIndex] = temp;

                        handedIndex++;
                        unHandedIndex++;
                    }
                    else
                    {
                        unHandedIndex++;
                    }
                }

                //直接交换元素
                array[endIndex] = array[handedIndex];
                array[handedIndex] = pivotValue;
                pivotIndex = handedIndex;
            }
            else
            {
                for (int i = unHandedIndex; i < endIndex; i++)
                {
                    if (array[unHandedIndex] >= pivotValue)
                    {
                        //进行数据交换
                        var temp = array[i];
                        array[i] = array[handedIndex];
                        array[handedIndex] = temp;

                        handedIndex++;
                        unHandedIndex++;
                    }
                    else
                    {
                        unHandedIndex++;
                    }
                }

                //直接交换元素
                array[endIndex] = array[handedIndex];
                array[handedIndex] = pivotValue;
                pivotIndex = handedIndex;
            }

            //处理完毕，递归
            QuickSortInner(array, startIndex, pivotIndex - 1, desc);
            QuickSortInner(array, pivotIndex + 1, endIndex, desc);
        }

        #endregion

        #region 桶排序

        /// <summary>
        /// 桶排序(时间复杂度O(logn),稳定排序,非原地排序)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="desc"></param>
        public static void BucketSort(this long[] array, bool desc = false)
        {
            //必须要知道数据大小，已知数据范围：0~10000
            //分成10个桶
            var buckets = new List<long>[10];
            var rules = new Tuple<long, long, int>[10];
            for (var i = 0; i < rules.Length; i++)
            {
                rules[i] = new Tuple<long, long, int>(10000L / 10 * i, 10000L / 10 * (i + 1), i);
            }

            for (var i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<long>();
            }

            for (var i = 0; i < array.Length; i++)
            {
                for (var j = 0; j < rules.Length; j++)
                {
                    if (array[i] >= rules[j].Item1 && array[i] < rules[j].Item2)
                    {
                        buckets[rules[j].Item3].Add(array[i]);
                        break;
                    }
                }
            }

            if (!desc)
            {
                Parallel.ForEach(buckets, list =>
                {
                    list.Sort();
                });

                var index = 0;
                foreach (var bucket in buckets)
                {
                    bucket.CopyTo(array, index);
                    index = index + bucket.Count;
                }
            }
            else
            {
                Parallel.ForEach(buckets, list =>
                {
                    list.Sort(new ReverseComparer<long>());
                });

                var index = 0;
                foreach (var bucket in buckets.Reverse())
                {
                    bucket.CopyTo(array, index);
                    index = index + bucket.Count;
                }
            }
        }

        #endregion

        /// <summary>
        /// 从大到小比较
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ReverseComparer<T> : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                return Comparer<T>.Default.Compare(y, x);
            }
        }
    }
}