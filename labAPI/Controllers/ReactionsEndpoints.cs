using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;
namespace labAPI.Controllers
{
    public static class ReactionsEndpoints
    {
        public static void MapReactionsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Reactions");
            group.MapGet("/", async (LabDBContext db) =>
            {
                return await db.Reactions.ToListAsync();
            })
            .WithName("GetAllReactions");

            group.MapGet("/{id}", async Task<Results<Ok<Reactions>, NotFound>> (string id, LabDBContext db) =>
            {
                return await db.Reactions.FindAsync(id)
                    is Reactions model
                        ? TypedResults.Ok(model)
                        : TypedResults.NotFound();
            })
          .WithName("GetAReactionById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, Reactions reactions, LabDBContext db) =>
            {
                var foundModel = await db.Reactions.FindAsync(id);

                if (foundModel is null)
                {
                    return TypedResults.NotFound();
                }

                db.Update(reactions);
                await db.SaveChangesAsync();

                return TypedResults.NoContent();
            })
          .WithName("UpdateReaction");

            group.MapPost("/", async (Reactions reactions, LabDBContext db) =>
            {
                db.Reactions.Add(reactions);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab/{reactions.Id}", reactions);
            })
            .WithName("CreateReaction");

            group.MapDelete("/{id}", async Task<Results<Ok<Reactions>, NotFound>> (string id, LabDBContext db) =>
            {
                if (await db.Reactions.FindAsync(id) is Reactions reactions)
                {
                    db.Reactions.Remove(reactions);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(reactions);
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteReactions");
        }
    }
}
