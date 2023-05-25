using Domain.InterfacesWorker;
using Domain.Models.MainDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLipsum.Core;

namespace Client.Controllers;

[Route("api/v1")]
[ApiController]
[Authorize]
public class DataGeneratorController
{
    public IProductWorker ProductWorker { get; }
    private LipsumGenerator Lipsum { get; } = new();
    private static readonly Random Random = new();

    public DataGeneratorController(IProductWorker productWorker)
    {
        ProductWorker = productWorker;
    }


    [HttpGet]
    [Route("AddData")]
    public async Task<ActionResult> AddDataAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            //Material 
            for (int i = 0; i < 2000; i++)
            {
                MaterialType mt = (MaterialType)Random.Next(0, 4);
                if (mt == MaterialType.Engobe) continue;

                await ProductWorker.MaterialRepository.Add(new()
                {
                    Reference = RandomString(Random.Next(2, 6)),
                    Name = RandomString(Random.Next(7, 22), true),
                    Comment = Lipsum.GenerateLipsum(Random.Next(2, 3), Features.Sentences, string.Empty),
                    Type = mt,
                    Cost = Random.Next(1, 300),
                    Quantity = Random.Next(1, 20),
                    UniteMesure = (MaterialUnite)Random.Next(0, 2),
                    IsHomeMade = false
                }, cancellationToken);
            }

            await ProductWorker.Completed(cancellationToken);


            // for (int i = 0; i < 1; i++)
            // {
            //     await ProductWorker.ProductRepository.Add(new()
            //     {
            //         Reference = RandomString(4),
            //         Name = RandomString(random.Next(7, 22), true),
            //         Description = lipsum.GenerateLipsum(1),
            //         DesignInstruction = lipsum.GenerateLipsum(1),
            //         GlazingInstruction = lipsum.GenerateLipsum(1),
            //         Price = random.Next(1, 999),
            //         Height = random.Next(1, 20),
            //         TopDiameter = random.Next(1, 20),
            //         BottomDiameter = random.Next(1, 20),
            //         Status = (ProductStatus)random.Next(0, 4),
            //         ShrinkingCoeficient = random.Next(1, 20),
            //         IdWorkshop = 6
            //     }, cancellationToken);
            // }


            return new JsonResult(new { sucess = true });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { sucess = false, error = ex.Message, stacktrace = ex.StackTrace });
        }
    }


    public static string RandomString(int length, bool withSpace = false)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz1234567890abcdefghijklmnopqrstuvwxyz1234567890";
        if (withSpace) chars += "                  ";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}