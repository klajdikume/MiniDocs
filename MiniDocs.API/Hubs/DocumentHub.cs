using Microsoft.AspNetCore.SignalR;

namespace MiniDocs.API.Hubs
{
    public class DocumentHub : Hub
    {
        internal static readonly Dictionary<Guid, (string Content, DateTime LastSaved)> _autosaveBuffer = new();

        public async Task EditDocument(Guid documentId, string updatedContent)
        {
            await Clients.OthersInGroup(documentId.ToString())
                .SendAsync("ReceiveEdit", updatedContent);

            _autosaveBuffer[documentId] = (updatedContent, DateTime.UtcNow);
        }

        public async Task JoinDocument(Guid documentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, documentId.ToString());
        }

        public async Task LeaveDocument(Guid documentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, documentId.ToString());
        }

        public async Task SendComment(string documentId, string userId, string commentText, int? startOffset, int? endOffset)
        {
            await Clients.OthersInGroup(documentId).SendAsync("ReceiveComment", userId, commentText, startOffset, endOffset);
        }
    }
}
