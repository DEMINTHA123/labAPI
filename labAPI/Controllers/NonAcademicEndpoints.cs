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
using labAPI.DTOs.NonAccademic_DTO;

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

            group.MapGet("/{id}", async Task<Results<Ok<NonAcademicOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                return await db.NonAcademic.FindAsync(id)
                    is NonAcademic model
                        ? TypedResults.Ok(_mapper.Map< NonAcademicOutputDTO > (model))
                        : TypedResults.NotFound();
            })
          .WithName("GetANonacademicsById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, NonAcademicInputDTO nonAcademic, LabDBContext db, IMapper _mapper) =>
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

            group.MapPost("/", async (NonAcademicInputDTO nonAcademic, LabDBContext db, IMapper _mapper) =>
            {
                db.NonAcademic.Add(_mapper.Map<NonAcademic>(nonAcademic));
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab", nonAcademic);
            })
            .WithName("CreateNonacademics");

            group.MapDelete("/{id}", async Task<Results<Ok<NonAcademicOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                if (await db.NonAcademic.FindAsync(id) is NonAcademic nonAcademic)
                {
                    db.NonAcademic.Remove(nonAcademic);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(_mapper.Map<NonAcademicOutputDTO>(nonAcademic));
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteNonAcademics");
        }

    }
}
