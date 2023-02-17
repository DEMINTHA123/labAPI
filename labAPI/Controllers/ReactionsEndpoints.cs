using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.DTOs;
using labAPI.Entities;
using Azure.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;
using labAPI.DTOs.ReactionsDTO;

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

            group.MapGet("/{id}", async Task<Results<Ok<ReactionsOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                return await db.Reactions.FindAsync(id)
                    is Reactions model
                        ? TypedResults.Ok(_mapper.Map< ReactionsOutputDTO > (model))
                        : TypedResults.NotFound();
            })
          .WithName("GetAReactionById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, ReactionsInputDTO reactions, LabDBContext db,IMapper _mapper) =>
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

            group.MapPost("/", async (ReactionsInputDTO reactions, LabDBContext db,IMapper _mapper) =>
            {
                db.Reactions.Add(_mapper.Map<Reactions>(reactions));
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab/{reactions.Id}", reactions);
            })
            .WithName("CreateReaction");

            group.MapDelete("/{id}", async Task<Results<Ok<ReactionsOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                if (await db.Reactions.FindAsync(id) is Reactions reactions)
                {
                    db.Reactions.Remove(reactions);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(_mapper.Map<ReactionsOutputDTO>(reactions));
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteReactions");
        }
    }
}
