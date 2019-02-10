using System;
using System.Collections.Generic;

namespace Algorithm
{
    public static class Sort
    {
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
        /// 归并排序内部递归操作
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