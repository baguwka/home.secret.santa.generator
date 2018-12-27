namespace Secret_Santa_Generator.Model.IdsProvider
{
    public class NoDuplicatesIdsProviderFactory : IIdProviderFactory
    {
        private readonly int _Exp;

        public NoDuplicatesIdsProviderFactory(int exp = 2)
        {
            _Exp = exp;
        }

        public IIdProvider Create() => new NoDuplicatesIdProvider(_Exp);
    }
}