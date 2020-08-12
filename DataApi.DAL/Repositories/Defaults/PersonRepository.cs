using DataApi.DAL.EntityFramework;
using DataApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataApi.DAL.Repositories.Defaults
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DefaultContext _db;
        public PersonRepository(DefaultContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync(Person value)
        {
            if(value != null)
            {
               await  _db.AddAsync(value);
               await _db.SaveChangesAsync();
            }
            return value.Id;
        }

        public async  Task<bool> DeleteAsync(int id)
        {
            var existPerson = await GetByIdAsync(id);

            if (existPerson != null)
            {
                _db.Remove<Person>(existPerson);
                return (await _db.SaveChangesAsync()) > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = await _db.Person
                .Include(p=>p.PersonContact)
                .Include(p=>p.CountryCodeNavigation)
                .Include(p=>p.Greeting)
                .FirstOrDefaultAsync(x=>x.Id==id);

            return person;
        }

        public IQueryable<Person> GetQuery()
        {
            return _db.Person.AsQueryable();
        }

        public async Task<bool> UpdateAsync(int id, Person person)
        {
            var existPerson = await _db.Person
                .Include(p => p.PersonContact)
                .Include(p => p.CountryCodeNavigation)
                .Include(p => p.Greeting)
                .FirstOrDefaultAsync(x => x.Id == id);




           // HashSet<int> k = new HashSet<int>();
            if (existPerson!=null)
            {
                 //  _db.Entry(person).State = EntityState.Modified;
                _db.Entry(existPerson).CurrentValues.SetValues(person);


                //foreach (var existingContacts in existPerson.PersonContact.ToList())
                //{
                //    if (!person.PersonContact.Any(c => c.PersonContactId == existingContacts.PersonContactId))
                //    {
                //        _db.PersonContact.Remove(existingContacts);
                //    }
                //}



                //foreach (var contact in person.PersonContact)
                //{
                //    var existingContact = existPerson.PersonContact
                //        .SingleOrDefault(c => c.PersonContactId == contact.PersonContactId);

                //    if (existingContact != null)
                //    {
                //        existingContact.Txt = contact.Txt;
                //        existingContact.ContactTypeId = contact.ContactTypeId;
                //    }
                //    else
                //    {
                //        // Insert child
                //        var newContact = new PersonContact
                //        {
                //            Txt = contact.Txt,
                //            ContactTypeId = contact.ContactTypeId
                //        };
                //        existPerson.PersonContact.Add(newContact);
                //    }
                //}
                existPerson.PersonContact = person.PersonContact;

                int k = 10;

                
                return (await _db.SaveChangesAsync())>0;
            }
            else
            {
                return false;
            }
        }
    }
}
