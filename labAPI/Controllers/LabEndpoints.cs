using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using labAPI;
using labAPI.Entities;
using labAPI.DTOs;
using AutoMapper;
using Azure.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace labAPI.Controllers;

public static class LabEndpoints
{
    public static void MapLabEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Lab");

        group.MapGet("/", async (LabDBContext db) =>
        {
            return await db.Labs.ToListAsync();
        })
        .WithName("GetAllLabs")
        ;

        group.MapGet("/{id}", async Task<Results<Ok<LabOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
        {
            return await db.Labs.FindAsync(id)
               is Lab model
                   ? TypedResults.Ok(_mapper.Map<LabOutputDTO>(model))
                   : TypedResults.NotFound();
        })
        .WithName("GetLabById")
        ;


        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (string id, LabInputDTO lab, LabDBContext db, IMapper _mapper) =>
        {
            var foundModel = await db.Labs.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(lab);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateLab")
        ;


        group.MapPost("/", (LabInputDTO labInput, LabDBContext db, IMapper _mapper) =>
        {
            db.Labs.Add(_mapper.Map<Lab>(labInput));
            db.SaveChanges();
            return TypedResults.Created($"/api/Lab/{labInput.Id}", labInput);
        })
        .WithName("CreateLab")
        ;

        group.MapDelete("/{id}", async Task<Results<Ok<LabOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
        {
            if (await db.Labs.FindAsync(id) is Lab lab)
            {
                db.Labs.Remove(lab);
                await db.SaveChangesAsync();
                return TypedResults.Ok(_mapper.Map<LabOutputDTO>(lab));
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteLab")
        ;

        group.MapPost("/LoginLab", async Task<Results<Ok<bool>, NotFound>> (LoginDTO labInputLogin, LabDBContext db, IMapper _mapper) =>
        {
            var getUser = await db.Labs.Where(x => x.UserName == labInputLogin.UserName).FirstOrDefaultAsync();

            if (getUser == null)
            {
                return TypedResults.NotFound();
            }
            else if (getUser.Password == labInputLogin.Password)
            {
                return TypedResults.Ok(true);
            }
            else
            {
                return TypedResults.Ok(false);
            }
        })
       .WithName("LoginLab")
        ;
    }

}
    
