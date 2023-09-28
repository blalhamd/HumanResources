

namespace Service.Business.SignalR
{
    public interface IChatHub
    {
        Task SendMessageAsync(string message);
        Task ReciveMessageAsync(string message);


    }
}
