using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;

namespace labAPI.Controllers
{
    public static class ExperimentEndpoints
    {
        public static void MapExperimentEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Experiment");
            group.MapGet("/", async (LabDBContext db) =>
            {
                return await db.Experiment.ToListAsync();
            })
            .WithName("GetAllExperiments");

            group.MapGet("/{id}", async Task<Results<Ok<Experiment>, NotFound>> (string id, LabDBContext db) =>
            {
                return await db.Experiment.FindAsync(id)
                    is Experiment model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
          .WithName("GetAExperimentById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Experiment experiment, LabDBContext db) =>
            {
                var foundModel = await db.Experiment.FindAsync(id);

                if (foundModel is null)
                {
                    return TypedResults.NotFound();
                }

                db.Update(experiment);
                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            })
          .WithName("UpdateExperiment");

            group.MapPost("/", async (Experiment experiment, LabDBContext db) =>
            {
                db.Experiment.Add(experiment);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab/{experiment.Id}", experiment);
            })
            .WithName("CreateExperiment");

            group.MapDelete("/{id}", async Task<Results<Ok<Experiment>, NotFound>> (string id, LabDBContext db) =>
            {
                if (await db.Experiment.FindAsync(id) is  Experiment experiment)
                {
                    db.Experiment.Remove(experiment);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(experiment);
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteExperiment");
        }
    }
}
