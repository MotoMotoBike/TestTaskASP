using System;
using System.Collections.Generic;

namespace TeledocTestTask.Models
{
    public class Founder
    {
        public Founder()
        {

        }
        public Founder(int id, string iNN, string name, string middleName, string lastName, DateTime dateCreate, DateTime dateUpdate)
        {
            Id = id;
            INN = iNN;
            Name = name;
            MiddleName = middleName;
            LastName = lastName;
            DateCreate = dateCreate;
            DateUpdate = dateUpdate;
        }

        public int Id { get; set; }
        public string INN { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }

        public virtual ICollection<ClientFounder> ClientFounders { get; set; } = new List<ClientFounder>();
    }
}
