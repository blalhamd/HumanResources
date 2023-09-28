
namespace Framework.core.IRepositories
{
    public interface IunitOfWorkAsync 
    {
        Task<int> commitAsync();
    }
}
