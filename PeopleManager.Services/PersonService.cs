using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class PersonService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PersonService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<Person>> FindAsync()
        {
            return await _dbContext.People
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();
        }

        //Get by id
        public async Task<Person?> GetAsync(int id)
        {
            return await _dbContext.People.FindAsync(id);
        }

        //Create
        public async Task<Person?> CreateAsync(Person person)
        {
            _dbContext.Add(person);
            await _dbContext.SaveChangesAsync();

            return person;
        }

        //Update
        public async Task<Person?> UpdateAsync(int id, Person person)
        {
            var dbPerson = await _dbContext.People.FindAsync(id);
            if (dbPerson is null)
            {
                return null;
            }

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.Description = person.Description;

            await _dbContext.SaveChangesAsync();

            return dbPerson;
        }

        //Delete
        public async Task DeleteAsync(int id)
        {
            var person = new Person
            {
                Id = id,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty
            };
            _dbContext.People.Attach(person);

            _dbContext.People.Remove(person);

            await _dbContext.SaveChangesAsync();
        }
    }
}
