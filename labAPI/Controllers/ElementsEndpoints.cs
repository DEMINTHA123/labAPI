using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;
namespace labAPI.Controllers;

public static class ElementsEndpoints
{
    public static void MapElementsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Elements");
        var mass = routes.MapGroup("/api/Elements/MolarMass");

        group.MapGet("/", async (LabDBContext db) =>
        {
            return await db.Elements.ToListAsync();
        })
        .WithName("GetAllElements");

        group.MapGet("/{id}", async Task<Results<Ok<Elements>, NotFound>> (string id, LabDBContext db) =>
        {
            return await db.Elements.FindAsync(id)
                is Elements model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetElementById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Elements elements, LabDBContext db) =>
        {
            var foundModel = await db.Elements.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            db.Update(elements);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateElement");

        group.MapPost("/", async (Elements elements, LabDBContext db) =>
        {
            db.Elements.Add(elements);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lab/{elements.Id}", elements);
        })
        .WithName("CreateElement");

        group.MapDelete("/{id}", async Task<Results<Ok<Elements>, NotFound>> (string id, LabDBContext db) =>
        {
            if (await db.Elements.FindAsync(id) is Elements elements)
            {
                db.Elements.Remove(elements);
                await db.SaveChangesAsync();
                return TypedResults.Ok(elements);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteElement");
    }
}
