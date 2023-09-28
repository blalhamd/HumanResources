

using Microsoft.AspNetCore.SignalR;

namespace Service.Business.SignalR
{
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendMessageAsync(string message)
        {
            await Clients.All.ReciveMessageAsync($"Reciving Message {message}");
        }

        public async Task ReciveMessageAsync(string message)
        {
            await Clients.All.ReciveMessageAsync(message);
        }

    }
}

/*
   In SignalR, the purpose of having separate SendMessage and ReceiveMessage methods is to establish 
   a clear separation of concerns between the client and the server.

   SendMessage: This method is invoked by the client to send a message to the server.
   It represents the action taken by the client to send a message to the server for further processing 
   or broadcasting.

   ReceiveMessage: This method is invoked on the server when a message is received from a client. 
   It represents the action taken by the server upon receiving a message from a client,
   such as broadcasting the message to other clients, storing it in a database,
   or performing any necessary server-side processing.
 
 */
