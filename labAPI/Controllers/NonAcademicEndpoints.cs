using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;

namespace labAPI.Controllers
{
    public static class NonAcademicEndpoints
    {
        public static void MapNonAcademicEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/NonAcademics");
            group.MapGet("/", async (LabDBContext db) =>
            {
                return await db.NonAcademic.ToListAsync();
            })
            .WithName("GetAllNonAcademics");

            group.MapGet("/{id}", async Task<Results<Ok<NonAcademic>, NotFound>> (string id, LabDBContext db) =>
            {
                return await db.NonAcademic.FindAsync(id)
                    is NonAcademic model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
          .WithName("GetANonacademicsById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, NonAcademic nonAcademic, LabDBContext db) =>
            {
                var foundModel = await db.NonAcademic.FindAsync(id);

                if (foundModel is null)
                {
                    return TypedResults.NotFound();
                }

                db.Update(nonAcademic);
                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            })
          .WithName("UpdateNonAcademics");

            group.MapPost("/", async (NonAcademic nonAcademic, LabDBContext db) =>
            {
                db.NonAcademic.Add(nonAcademic);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab/{nonAcademic.Id}", nonAcademic);
            })
            .WithName("CreateNonacademics");

            group.MapDelete("/{id}", async Task<Results<Ok<NonAcademic>, NotFound>> (string id, LabDBContext db) =>
            {
                if (await db.NonAcademic.FindAsync(id) is NonAcademic nonAcademic)
                {
                    db.NonAcademic.Remove(nonAcademic);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(nonAcademic);
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteNonAcademics");
        }

    }
}
