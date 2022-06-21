namespace TeledocTestTask.Models
{
    public class ClientFounder
    {
        public ClientFounder()
        {

        }

        public ClientFounder(int clientId, int founderId)
        {
            ClientId = clientId;
            FounderId = founderId;
        }

        public int ClientId { get; set; }
        public int FounderId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Founder Founder { get; set; }
    }
}
