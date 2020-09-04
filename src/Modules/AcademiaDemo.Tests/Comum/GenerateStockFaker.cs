using AcademiaDemo.Domain.Entities;
using Bogus;
using System;
using System.Collections.Generic;

namespace AcademiaDemo.Tests.Comum
{
    internal static class GenerateStockFaker
    {
        public static Stock CreateOneStockObject(Guid id)
        {
            var item = new Faker<Stock>("pt_BR")
                .RuleFor(c => c.Id, id)
                .RuleFor(c => c.ItemId, f => f.Random.Guid())
                .RuleFor(c => c.Ammount, f => f.Random.Int(1, 10))
                .Generate();


            return item;
        }
    }
}
