using System.Collections.Generic;
using System.Windows.Input;

namespace Secret_Santa_Generator.Model.Combination
{
    public class KonamiCodeCombinationHandler : CombinationHandler
    {
        protected override IEnumerable<Key> EnumerateCorrectCombination()
        {
            yield return Key.Up;
            yield return Key.Up;
            yield return Key.Down;
            yield return Key.Down;
            yield return Key.Left;
            yield return Key.Right;
            yield return Key.Left;
            yield return Key.Right;
            yield return Key.B;
            yield return Key.A;
        }
    }
}