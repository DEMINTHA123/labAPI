using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;
namespace labAPI.Controllers;

public static class EquipmentEndpoints
{
    public static void MapEquipmentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Equipment");

        group.MapGet("/", async (LabDBContext db) =>
        {
            return await db.Equipment.ToListAsync();
        })
        .WithName("GetAllEquipment");

        group.MapGet("/{id}", async Task<Results<Ok<Equipment>, NotFound>> (string id, LabDBContext db) =>
        {
            return await db.Equipment.FindAsync(id)
                is Equipment model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEquipmentById");

        group.MapGet("/GetEquipmentNameById/{id}", async Task<Results<Ok<string>, NotFound>> (string id, LabDBContext db) =>
        {
            return await db.Equipment.FindAsync(id)
                is Equipment model
                    ? TypedResults.Ok(model.Name)
                    : TypedResults.NotFound();
        })

       .WithName("GetEquipmentNameById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Equipment equipment, LabDBContext db) =>
        {
            var foundModel = await db.Equipment.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            db.Update(equipment);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateEquipment");

        group.MapPost("/", async (Equipment equipment, LabDBContext db) =>
        {
            db.Equipment.Add(equipment);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lab/{equipment.Id}", equipment);
        })
        .WithName("CreateEquipment");

        group.MapDelete("/{id}", async Task<Results<Ok<Equipment>, NotFound>> (string id, LabDBContext db) =>
        {
            if (await db.Equipment.FindAsync(id) is Equipment elements)
            {
                db.Equipment.Remove(elements);
                await db.SaveChangesAsync();
                return TypedResults.Ok(elements);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteElement");
    }
}