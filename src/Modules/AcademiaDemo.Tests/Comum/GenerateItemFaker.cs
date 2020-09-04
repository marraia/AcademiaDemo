using AcademiaDemo.Application.AppItem.Input;
using AcademiaDemo.Domain.Entities;
using Bogus;
using System.Collections.Generic;

namespace AcademiaDemo.Tests.Comum
{
    internal static class GenerateItemFaker
    {
        public static IEnumerable<Item> CreateManyItemObject(int qtd)
        {
            var item = new Faker<Item>("pt_BR")
                .RuleFor(c => c.Description, f => f.Name.FullName())
                .RuleFor(c => c.Price, f => f.Random.Decimal())
                .Generate(qtd);


            return item;
        }

        public static ItemInput CreateItemInputObject()
        {
            var input = new Faker<ItemInput>("pt_BR")
                .RuleFor(c => c.Description, f => f.Name.FullName())
                .RuleFor(c => c.Price, f => f.Random.Decimal())
                .RuleFor(c => c.Ammount, f => f.Random.Int(1, 100))
                .Generate();

            return input;
        }
    }
}
