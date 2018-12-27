using System.Collections.Generic;
using System.Windows.Input;

namespace Secret_Santa_Generator.Model.Combination
{
    public abstract class CombinationHandler
    {
        protected abstract IEnumerable<Key> EnumerateCorrectCombination();

        private CollectionEnumeratorDecorator _Enumerator;
        private CollectionEnumeratorDecorator Enumerator => _Enumerator ?? (_Enumerator = new CollectionEnumeratorDecorator(EnumerateCorrectCombination()));

        public CombinationResult ProvideNextInput(Key input)
        {
            var moveResult = Enumerator.MoveNext();
            if (moveResult == false)
            {
                Enumerator.Reset();
                return CombinationResult.NotCompleted();
            }

            var expected = Enumerator.Current;
            if (input != expected)
            {
                //failed. Resetting enumerator
                Enumerator.Reset();
                return CombinationResult.NotCompleted();
            }

            if (Enumerator.IsAtLastPosition)
            {
                //completed. Resetting and returns "Completed"
                Enumerator.Reset();
                return CombinationResult.Completed();
            }

            //do not reset. Continue listening because input was correct and it's not over yet.
            return CombinationResult.NotCompleted();
        }
    }
}