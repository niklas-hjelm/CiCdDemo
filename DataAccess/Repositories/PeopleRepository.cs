using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Domain.Common;
using Domain.Common.DTOs;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class PeopleRepository : IRepository<PersonDto>
{
    private readonly IMongoCollection<Person> _people;

    public PeopleRepository(DatabaseSettings dbSettings)
    {
        var client = new MongoClient(dbSettings.ConnectionString);
        var database = client.GetDatabase("DemoCiCd");
        _people = database.GetCollection<Person>("People", new MongoCollectionSettings(){AssignIdOnInsert = true});
    }

    public async Task Add(PersonDto dto)
    {
        await _people.InsertOneAsync(ConvertToModel(dto));
    }

    public async Task Delete(string id)
    {
        var filter = Builders<Person>.Filter.Eq("Id", ObjectId.Parse(id));
        await _people.DeleteOneAsync(filter);
    }

    public async Task<PersonDto> Get(string id)
    {
        var result = await _people.FindAsync(p => p.Id.ToString() == id);
        return ConvertToDto(result.Single());
    }

    public async Task<PersonDto> Update(PersonDto dto, string id)
    {
        var filter = Builders<Person>.Filter.Eq("Id", ObjectId.Parse(id));
        var update = Builders<Person>.Update
            .Set("FirstName", dto.FirstName)
            .Set("LastName", dto.LastName)
            .Set("DateOfBirth", dto.DateOfBirth);
        await _people.FindOneAndUpdateAsync(filter, update);

        return dto;
    }

    public async Task<IEnumerable<PersonDto>> GetAll()
    {
        var all = await _people.FindAsync(_ => true);
        return all.ToEnumerable().Select(ConvertToDto);
    }

    private Person ConvertToModel(PersonDto dto)
    {
        return new Person
        {
            DateOfBirth = dto.DateOfBirth,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
    }

    private PersonDto ConvertToDto(Person model)
    {
        return new PersonDto()
        {
            DateOfBirth = model.DateOfBirth,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
    }
}
