using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;
namespace labAPI.Controllers;

public static class ChemicalsEndpoints
{
    public static void MapChemicalsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Chemicals");

        group.MapGet("/", async (LabDBContext db) =>
        {
            return await db.Labs.ToListAsync();
        })
        .WithName("GetAllChemicals");

        group.MapGet("/{id}", async Task<Results<Ok<Chemicals>, NotFound>> (string id, LabDBContext db) =>
        {
            return await db.Chemicals.FindAsync(id)
                is Chemicals model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetChemicalById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Chemicals chemicals, LabDBContext db) =>
        {
            var foundModel = await db.Academics.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            db.Update(chemicals);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateChemical");

        group.MapPost("/", async (Chemicals chemicals, LabDBContext db) =>
        {
            db.Chemicals.Add(chemicals);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lab/{chemicals.Id}", chemicals);
        })
        .WithName("CreateChemical");

        group.MapDelete("/{id}", async Task<Results<Ok<Chemicals>, NotFound>> (string id, LabDBContext db) =>
        {
            if (await db.Chemicals.FindAsync(id) is Chemicals chemicals)
            {
                db.Chemicals.Remove(chemicals);
                await db.SaveChangesAsync();
                return TypedResults.Ok(chemicals);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteChemical");
    }
}
