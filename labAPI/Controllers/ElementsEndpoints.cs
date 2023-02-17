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

        group.MapGet("/{id}", async Task<Results<Ok<ElementsOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
        {
            return await db.Elements.FindAsync(id)
                is Elements model
                    ? TypedResults.Ok(_mapper.Map< ElementsOutputDTO > (model))
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

        group.MapPost("/", async (ElementsInputDTO elements, LabDBContext db,IMapper _mapper) =>
        {
            db.Elements.Add(_mapper.Map<Elements>(elements));
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lab/{elements.Id}", elements);
        })
        .WithName("CreateElement");

        group.MapDelete("/{id}", async Task<Results<Ok<ElementsOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
        {
            if (await db.Elements.FindAsync(id) is Elements elements)
            {
                db.Elements.Remove(elements);
                await db.SaveChangesAsync();
                return TypedResults.Ok(_mapper.Map<ElementsOutputDTO>(elements));
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteElement");
    }
}
