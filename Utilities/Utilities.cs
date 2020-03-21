using System;
using System.Collections.Generic;
using System.Linq;

namespace MoreEffectiveCS
{
    public static class Utilities
    {
        public static IEnumerable<TResult> Generator<TResult> (int number, Func<TResult> generator)
        {
            for (int i = 0; i < number; i++)
            {
                yield return generator();
            }
        }

        public static IEnumerable<TOutput> Merge<T1, T2, TOutput> (IEnumerable<T1> left, IEnumerable<T2> right, Func<T1, T2, TOutput> generator)
        {
            //var pairs = left.Zip(right, (l, r) => new { Left = l, Right = r });
            //foreach (var p in pairs)
            //{
            //    yield return generator(p.Left, p.Right);
            //}
            IEnumerator<T1> leftSequence = left.GetEnumerator();
            IEnumerator<T2> rightSequence = right.GetEnumerator();

            while(leftSequence.MoveNext() && rightSequence.MoveNext())
            {
                yield return generator(leftSequence.Current, rightSequence.Current);
            }
        }
    }
}
