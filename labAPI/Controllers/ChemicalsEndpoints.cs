using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.DTOs;
using labAPI.Entities;
using Azure.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;
using labAPI.DTOs.ChemicalsDTO;

namespace labAPI.Controllers;
public static class ChemicalsEndpoints
{
    public static void MapChemicalsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Chemicals");

        group.MapGet("/", async (LabDBContext db) =>
        {
            return await db.Chemicals.ToListAsync();
        })
        .WithName("GetAllChemicals");

        group.MapGet("/{id}", async Task<Results<Ok<ChemicalsOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
        {
            return await db.Chemicals.FindAsync(id)
                is Chemicals model
                    ? TypedResults.Ok(_mapper.Map< ChemicalsOutputDTO > (model))
                    : TypedResults.NotFound();
        })
        .WithName("GetChemicalById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, ChemicalsInputDTO chemicals, LabDBContext db) =>
        {
            var foundModel = await db.Chemicals.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            foundModel.Name = chemicals.Name;
            foundModel.Symbol = chemicals.Symbol;
            foundModel.MatterState =  chemicals.MatterState;
            foundModel.Construction = chemicals.Construction;
            db.Update(foundModel);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateChemical");

        group.MapPost("/", async (ChemicalsInputDTO chemicals, LabDBContext db, IMapper _mapper) =>
        {
            db.Chemicals.Add(_mapper.Map<Chemicals>(chemicals));
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lab/", chemicals);
        })
        .WithName("CreateChemical");

        group.MapDelete("/{id}", async Task<Results<Ok<ChemicalsOutputDTO>, NotFound>> (int id, LabDBContext db, IMapper _mapper) =>
        {
            if (await db.Chemicals.FindAsync(id) is Chemicals chemicals)
            {
                db.Chemicals.Remove(chemicals);
                await db.SaveChangesAsync();
                return TypedResults.Ok(_mapper.Map< ChemicalsOutputDTO > (chemicals));
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteChemical");
    }
}
