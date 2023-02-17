using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.DTOs;
using labAPI.Entities;
using Azure.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AutoMapper;

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

            group.MapGet("/{id}", async Task<Results<Ok<AcademicOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                return await db.Academics.FindAsync(id)
                    is Academic model
                        ? TypedResults.Ok(_mapper.Map<AcademicOutputDTO> (model))
                        : TypedResults.NotFound();
            })
          .WithName("GetAcademicById");

            group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, AcademicInputDTO academic, LabDBContext db, IMapper _mapper) =>
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

            group.MapPost("/", async (AcademicInputDTO academic, LabDBContext db, IMapper _mapper) =>
            {
                db.Academics.Add(_mapper.Map< Academic > (academic));
                await db.SaveChangesAsync();
                return TypedResults.Created($"/api/Lab/{academic.Id}", academic);
            })
            .WithName("CreateAcademic");

            group.MapDelete("/{id}", async Task<Results<Ok<AcademicOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
            {
                if (await db.Academics.FindAsync(id) is Academic academic)
                {
                    db.Academics.Remove(academic);
                    await db.SaveChangesAsync();
                    return TypedResults.Ok(_mapper.Map< AcademicOutputDTO> (academic));
                }

                return TypedResults.NotFound();
            })
            .WithName("DeleteAcademic");
        }
    }
}
