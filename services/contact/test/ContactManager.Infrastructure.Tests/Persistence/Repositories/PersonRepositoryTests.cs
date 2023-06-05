using ContactManager.Domain.Aggregates.PersonAggregate;
using ContactManager.Infrastructure.Tests.Common;

namespace ContactManager.Infrastructure.Tests.Persistence.Repositories
{
    public class PersonRepositoryTests : BaseTest
    {
        private readonly IPersonRepository _repository;

        public PersonRepositoryTests(HostFixture fixture) : base(fixture)
        {
            _repository = GetService<IPersonRepository>();
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Person()
        {
            var personId = Guid.NewGuid();
            var expectedPerson = GetDefaultPersonBuilder().Build();
                expectedPerson.Id = personId;

            await _repository.AddAsync(expectedPerson);
            var retrievedPerson = await _repository.GetByIdAsync(personId);

            Assert.Equal(expectedPerson.Id, retrievedPerson.Id);
            Assert.Equal(expectedPerson.FirstName, retrievedPerson.FirstName);
            Assert.Equal(expectedPerson.LastName, retrievedPerson.LastName);
            Assert.Equal(expectedPerson.Company, retrievedPerson.Company);
        }

        [Fact]
        public async Task AddAsync_Should_InsertPerson_IntoCollection()
        {
            var person = GetDefaultPersonBuilder().Build();

            await _repository.AddAsync(person);
        }

        [Fact]
        public async Task UpdateAsync_Should_ReplacePerson_InCollection()
        {
            var personId = Guid.NewGuid();
            var expectedPerson = GetDefaultPersonBuilder().Build();
            expectedPerson.Id = personId;

            await _repository.AddAsync(expectedPerson);

            var retrievedPerson = await _repository.GetByIdAsync(personId);
                retrievedPerson?.SetCompany("Rise-Tech");

            await _repository.UpdateAsync(retrievedPerson);
            
                retrievedPerson = await _repository.GetByIdAsync(personId);

            Assert.Equal(expectedPerson.Id, retrievedPerson.Id);
            Assert.Equal(expectedPerson.FirstName, retrievedPerson.FirstName);
            Assert.Equal(expectedPerson.LastName, retrievedPerson.LastName);
            Assert.Equal("Rise-Tech", retrievedPerson.Company);
        }

        [Fact]
        public async Task DeleteAsync_Should_DeletePerson_FromCollection()
        {
            var personId = Guid.NewGuid();
            var expectedPerson = GetDefaultPersonBuilder().Build();
            expectedPerson.Id = personId;

            await _repository.AddAsync(expectedPerson);
            await _repository.DeleteAsync(personId);

            var retrievedPerson = await _repository.GetByIdAsync(personId);
           
            Assert.Null(retrievedPerson);
        }

        private PersonBuilder GetDefaultPersonBuilder()
        {
            return PersonBuilder.Person()
               .WithFirstName("Onur")
               .WithLastName("Can")
               .WithCompany("TEI");
        }
    }
}