using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Components;
using Models;
using System.Data;

namespace Services
{
    public class PersonService
    {
        PersonComponent data = new PersonComponent();

        public DataTable ListPerson()
        {
            return data.PersonList();
        }

        public void InsertPerson(PersonModel emp)
        {
            data.PersonInsert(emp);
        }

        public void DeletePerson(int personId)
        {
            data.PersonDelete(personId);
        }

        public void UpdatePerson(PersonModel emp)
        {
            data.PersonUpdate(emp);
        }

        public DataTable SearchPerson (String search)
        {
            return data.PersonSearch(search);
        }
    }
}
