using System;
using System.Collections.Generic;

namespace TeledocTestTask.Models
{
    public class Client
    {
        public Client()
        {

        }

        public int Id { get; set; }
        public string INN { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public ClientType clientType { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public virtual ICollection<ClientFounder> ClientFounders { get; set; } = new List<ClientFounder>();

        public Client(int id, string inn, string name, string middleName, string lastName,
            ClientType clientType, DateTime dateCreate, DateTime dateUpdate)
        {
            Id = id;
            INN = inn;
            Name = name;
            MiddleName = middleName;
            LastName = lastName;
            this.clientType = clientType;
            DateCreate = dateCreate;
            DateUpdate = dateUpdate;
        }

        public Client(string inn, string name, string middleName, string lastName,
            ClientType clientType, DateTime dateCreate, DateTime dateUpdate)
        {
            INN = inn;
            Name = name;
            MiddleName = middleName;
            LastName = lastName;
            this.clientType = clientType;
            DateCreate = dateCreate;
            DateUpdate = dateUpdate;
        }
    }

    public enum ClientType : byte
    {
        IndividualEntrepreneur = 1,
        LegalEntity = 2
    }
}
