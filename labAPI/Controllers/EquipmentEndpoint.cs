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
using Microsoft.AspNetCore.Mvc;

namespace labAPI.Controllers;

public static class EquipmentEndpoints
{
    public static void MapEquipmentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/minimalapi/Equipment");

        group.MapGet("/", async (LabDBContext db) =>
        {
            return await db.Equipment.ToListAsync();
        })
        .WithName("GetAllEquipment");

        group.MapGet("/{id}", async Task<Results<Ok<EquipmentOutputDTO>, NotFound>> (string id, LabDBContext db, IMapper _mapper) =>
        {
            return await db.Equipment.FindAsync(id)
                is Equipment model
                    ? TypedResults.Ok(_mapper.Map< EquipmentOutputDTO > (model))
                    : TypedResults.NotFound();
        })
        .WithName("GetEquipmentById");

        group.MapGet("/GetEquipmentNameById/{id}", async Task<Results<Ok<string>, NotFound>> (string id, LabDBContext db) =>
        {
            return await db.Equipment.FindAsync(id)
                is Equipment model
                    ? TypedResults.Ok(model.Name)
                    : TypedResults.NotFound();
        })

       .WithName("GetEquipmentNameById");

        //group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Equipment equipment, LabDBContext db) =>
        //{
        //    var foundModel = await db.Equipment.FindAsync(id);

        //    if (foundModel is null)
        //    {
        //        return TypedResults.NotFound();
        //    }
        //    foundModel.Name = equipment.Name;
        //    foundModel.Qty = equipment.Qty;
        //    foundModel.Description = equipment.Description;
        //    foundModel.Photo = equipment.Photo;
        //    db.Update(foundModel);

        //    db.Update(foundModel);
        //    await db.SaveChangesAsync();

        //    return TypedResults.NoContent();
        //})
        //.WithName("UpdateEquipment");

        group.MapPost("/", async (EquipmentInputDTO equipment, LabDBContext db, IMapper _mapper) =>
        {
            db.Equipment.Add(_mapper.Map<Equipment>(equipment));
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Lab/", equipment);
        })
        .WithName("CreateEquipment");

        group.MapDelete("/{id}", async Task<Results<Ok<Equipment>, NotFound>> (int id, LabDBContext db) =>
        {
            if (await db.Equipment.FindAsync(id) is Equipment equipment)
            {
                db.Equipment.Remove(equipment);
                await db.SaveChangesAsync();
                return TypedResults.Ok(equipment);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteEquipment");
    }
}