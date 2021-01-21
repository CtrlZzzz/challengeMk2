namespace ChallengeMk2.Models.ChatModels
{
    public class NewRoomForm
    {
        public NewRoomForm(string name)
        {
            RoomName = name;
        }

        public string RoomName { get; set; }
    }
}
