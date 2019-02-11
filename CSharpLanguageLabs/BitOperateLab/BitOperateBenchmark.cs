using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BitOperateLab
{
    public class BitOperateBenchmark
    {
        private int _oddCount = 0;
        private int _evenCount = 0;

        private int[] _intersect;
        private int[] _union;

        [Benchmark]
        [BenchmarkCategory("OddEvenJudge")]
        [ArgumentsSource(nameof(OddEvenSource))]
        public void OddEvenJudge(int[] nums)
        {
            _evenCount = 0;
            _oddCount = 0;

            foreach (var num in nums)
            {
                if (num % 2 == 0)
                {
                    _evenCount++;
                }
                else
                {
                    _oddCount++;
                }
            }
        }

        [Benchmark]
        [BenchmarkCategory("OddEvenJudge")]
        [ArgumentsSource(nameof(OddEvenSource))]
        public void OddEvenJudgeWithBitOperate(int[] nums)
        {
            _evenCount = 0;
            _oddCount = 0;

            foreach (var num in nums)
            {
                //数字与1进行与操作判断奇偶
                if ((num & 1) != 0)
                {
                    _oddCount++;
                }
                else
                {
                    _evenCount++;
                }
            }
        }

        [Benchmark]
        [BenchmarkCategory("Swap")]
        [ArgumentsSource(nameof(SwapSource))]
        public void Swap(int[] objs)
        {
            var temp = objs[1];
            objs[1] = objs[0];
            objs[0] = temp;
        }

        [Benchmark]
        [BenchmarkCategory("Swap")]
        [ArgumentsSource(nameof(SwapSource))]
        public void SwapWithBitOperate(int[] objs)
        {
            //连续异或实现无临时变量交换数据
            objs[1] = objs[0] ^ objs[1];
            objs[0] = objs[0] ^ objs[1];
            objs[1] = objs[0] ^ objs[1];
        }

        [Benchmark]
        [BenchmarkCategory("CollectionJudge")]
        [ArgumentsSource(nameof(CollectionJudgeSource))]
        public void CollectionJudge(int[] nums1, int[] nums2)
        {
            _intersect = nums1.Intersect(nums2).ToArray();
            _union = nums1.Union(nums2).ToArray();
        }

        [Benchmark]
        [BenchmarkCategory("CollectionJudge")]
        [ArgumentsSource(nameof(CollectionJudgeSource))]
        public void CollectionJudgeWithBitOperate(int[] nums1, int[] nums2)
        {
            var bitArray = 0L;
            var bitArray2 = 0L;

            foreach (var i in nums1)
            {
                //包含有0
                bitArray = bitArray | (1L << i);
            }

            foreach (var i in nums2)
            {
                //包含有0
                bitArray2 = bitArray2 | (1L << i);
            }

            //求相交，按位与
            var intersect = bitArray & bitArray2;

            //求相并，按位或
            var union = bitArray | bitArray2;
        }

        public IEnumerable<int[]> OddEvenSource()
        {
            yield return Enumerable.Range(-100000, 100000).ToArray();
        }

        public IEnumerable<int[]> SwapSource()
        {
            yield return new[] {-5, 45};
            yield return new[] {34345, 4345};
        }

        public IEnumerable<object[]> CollectionJudgeSource()
        {
            yield return new object[] {new[] {3, 5, 7, 4, 9, 2}, new[] {3, 6, 1, 9}};
        }
    }
}