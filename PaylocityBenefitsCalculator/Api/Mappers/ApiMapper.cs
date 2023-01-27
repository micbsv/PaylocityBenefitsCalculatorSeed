using Mapster;

namespace Api.Mappers
{
    public static class ApiMapper
    {
        internal static IQueryable<TDest> Map<TSrc, TDest>(IQueryable<TSrc> source)
        {
            var dest = source.ProjectToType<TDest>();
            return dest;
        }

        internal static TOut Map<TIn, TOut>(TIn source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var dest = source.Adapt<TOut>();
            return dest;
        }
    }
}
