using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Secret_Santa_Generator.Model
{
    public class CollectionEnumeratorDecorator : IEnumerator<Key>
    {
        private readonly ICollection<Key> _Keys;
        private IEnumerator<Key> _CombinationEnumerator;

        public int Count => _Keys.Count;
        public int MovesCount { get; private set; }

        public bool IsAtLastPosition => MovesCount >= Count;

        private IEnumerator<Key> InternalEnumerator {
            get {
                if (_CombinationEnumerator == null)
                {
                    return _CombinationEnumerator = _Keys.GetEnumerator();
                }

                return _CombinationEnumerator;
            }
        }

        public CollectionEnumeratorDecorator(IEnumerable<Key> decorated)
        {
            _Keys = decorated.ToList();
        }

        public void Dispose()
        {
            InternalEnumerator.Dispose();
        }

        public bool MoveNext()
        {
            MovesCount++;
            return InternalEnumerator.MoveNext();
        }

        public void Reset()
        {
            MovesCount = 0;
            InternalEnumerator.Reset();
        }

        public Key Current => InternalEnumerator.Current;

        object IEnumerator.Current => Current;
    }
}