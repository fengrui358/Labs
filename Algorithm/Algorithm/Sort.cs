﻿namespace Algorithm
{
    public static class Sort
    {
        /// <summary>
        /// 冒泡排序(时间复杂度On2,稳定排序,原地排序)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static void BubbleSort(this long[] array, bool desc = false)
        {
            var index = 0;

            if (!desc)
            {
                //升序
                while (index <= array.Length - 1)
                {
                    //标记位，确认是否还需要继续
                    var hasSwap = false;
                    for (var i = index; i < array.Length - 1; i++)
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

                    index++;
                }
            }
            else
            {
                //降序
                while (index <= array.Length - 1)
                {
                    //标记位，确认是否还需要继续
                    var hasSwap = false;
                    for (var i = index; i < array.Length - 1; i++)
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

                    index++;
                }
            }
        }
    }
}
