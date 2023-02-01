using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;

namespace labAPI.Controllers
{
    public static class AcademicEndpoints
    {
        public static void MapAcademicEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Academic");
            group.MapGet("/", async (LabDBContext db) =>
            {
                return await db.Academics.ToListAsync();
            })
            .WithName("GetAllAcademic");

            group.MapGet("/{id}", async Task<Results<Ok<Academic>, NotFound>> (string id, LabDBContext db) =>
            {
                return await db.Academics.FindAsync(id)
                    is Academic model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
          .WithName("GetAcademicById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Academic academic, LabDBContext db) =>
            {
                var foundModel = await db.Academics.FindAsync(id);

                if (foundModel is null)
                {
                    return TypedResults.NotFound();
                }

                db.Update(academic);
                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            })
          .WithName("UpdateAcademic");

            group.MapPost("/", async (Academic academic, LabDBContext db) =>
            {
                db.Academics.Add(academic);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab/{academic.Id}", academic);
            })
            .WithName("CreateAcademic");

            group.MapDelete("/{id}", async Task<Results<Ok<Academic>, NotFound>> (string id, LabDBContext db) =>
            {
                if (await db.Academics.FindAsync(id) is Academic academic)
                {
                    db.Academics.Remove(academic);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(academic);
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteAcademic");
        }
    }
}
