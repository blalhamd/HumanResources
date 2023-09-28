

using System.Linq.Expressions;

namespace Framework.core.comman
{
    public class RepositoryRequestFilter<TEntity,TKey> : RepositoryRequest
    {
        public Expression<Func<TEntity,bool>> condition { get; set; }
        public RepositoryRequestFilter(RepositoryRequest repositoryRequest) : base(repositoryRequest) 
        { 
        }

        public RepositoryRequestFilter() { }



    }
}
