
using Framework.comman.Enums;

namespace Framework.core.comman
{
    public class RepositoryRequest
    {
        public Pagination? pagination { get; set; }
        public string Sorting { get; set; }
        public IList<string> includes { get; set; } = null!;
        public Order? order { get; set; } 

        public RepositoryRequest (RepositoryRequest repositoryRequest)
        {
            if(repositoryRequest != null)
            {
                pagination = repositoryRequest.pagination;
                includes = repositoryRequest.includes;
                Sorting = repositoryRequest.Sorting;
                order= repositoryRequest.order;
            }

            else
            {
                includes = new List<string>();
            }
        }

        public RepositoryRequest()
        {
            includes= new List<string>();
        }





    }
}
