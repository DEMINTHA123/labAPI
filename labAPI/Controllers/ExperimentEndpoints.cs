using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.DTOs;
using labAPI.Entities;
using Azure.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;
using labAPI.DTOs.ChemicalsDTO;
using labAPI.DTOs.ElementsDTO;
using labAPI.DTOs.EquipmentDTO;
using labAPI.DTOs.ExperimentDTO;

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

            group.MapGet("/{id}", async Task<Results<Ok<ExperimentOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                return await db.Experiment.FindAsync(id)
                    is Experiment model
                        ? TypedResults.Ok(_mapper.Map< ExperimentOutputDTO > (model))
                        : TypedResults.NotFound();
            })
          .WithName("GetAExperimentById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, ExperimentInputDTO experiment, LabDBContext db, IMapper _mapper) =>
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

            group.MapPost("/", async (ExperimentInputDTO experiment, LabDBContext db, IMapper _mapper) =>
            {
                db.Experiment.Add(_mapper.Map<Experiment>(experiment));
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab/{experiment.Id}", experiment);
            })
            .WithName("CreateExperiment");

            group.MapDelete("/{id}", async Task<Results<Ok<ExperimentOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                if (await db.Experiment.FindAsync(id) is  Experiment experiment)
                {
                    db.Experiment.Remove(experiment);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(_mapper.Map<ExperimentOutputDTO>(experiment));
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteExperiment");
        }
    }
}
