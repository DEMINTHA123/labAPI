using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;
namespace labAPI.Controllers;

public static class LabEndpoints
{
    public static void MapLabEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Lab");

        group.MapGet("/", async (LabDBContext db) =>
        {
            return await db.Labs.ToListAsync();
        })
        .WithName("GetAllLabs");

        group.MapGet("/{id}", async Task<Results<Ok<Lab>, NotFound>> (string id, LabDBContext db) =>
        {
            return await db.Labs.FindAsync(id)
                is Lab model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetLabById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Lab lab, LabDBContext db) =>
        {
            var foundModel = await db.Labs.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(lab);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateLab");

        group.MapPost("/", async (Lab lab, LabDBContext db) =>
        {
            db.Labs.Add(lab);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lab/{lab.Id}",lab);
        })
        .WithName("CreateLab");

        group.MapDelete("/{id}", async Task<Results<Ok<Lab>, NotFound>> (string id, LabDBContext db) =>
        {
            if (await db.Labs.FindAsync(id) is Lab lab)
            {
                db.Labs.Remove(lab);
                await db.SaveChangesAsync();
                return TypedResults.Ok(lab);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteLab");
    }
}
