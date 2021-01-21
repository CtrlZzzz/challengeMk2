namespace ChallengeMk2.Models.ChatModels
{
    public class MessageSentForm
    {
        public MessageSentForm(string senderId, string senderName, string content)
        {
            SenderId = senderId;
            SenderName = senderName;
            Content = content;
        }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public string Content { get; set; }
    }
}
