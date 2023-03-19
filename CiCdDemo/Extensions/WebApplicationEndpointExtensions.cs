using DataAccess.Repositories.Interfaces;
using Domain.Common.DTOs;

namespace CiCdDemo.Extensions;

public static class WebApplicationEndpointExtensions
{
    public static WebApplication MapPersonEndpoints(this WebApplication app)
    {
        app.MapPost("/people", AddPersonHandler).WithOpenApi();
        app.MapGet("/people/", GetPeopleHandler).WithOpenApi();
        app.MapGet("/people/{id}", GetPersonHandler).WithOpenApi();       
        app.MapPut("/people/{id}", UpdatePersonHandler).WithOpenApi();
        app.MapDelete("/people/{id}", RemovePersonHandler).WithOpenApi();
        
        return app;
    }

    private static async Task<IResult> AddPersonHandler(IRepository<PersonDto> repo, PersonDto dto)
    {
        await repo.Add(dto);
        return Results.Ok("Person added");
    }

    private static async Task<IResult> UpdatePersonHandler(IRepository<PersonDto> repo, string id, PersonDto dto)
    {
        return Results.Ok(await repo.Update(dto, id));
    }

    private static async Task<IResult> RemovePersonHandler(IRepository<PersonDto> repo, string id)
    {
        await repo.Delete(id);
        return Results.Ok("Person removed");
    }

    private static async Task<IResult> GetPeopleHandler(IRepository<PersonDto> repo)
    {
        return Results.Ok(await repo.GetAll());
    }

    private static async Task<IResult> GetPersonHandler(IRepository<PersonDto> repo, string id)
    {
        return Results.Ok(await repo.Get(id));
    }
}