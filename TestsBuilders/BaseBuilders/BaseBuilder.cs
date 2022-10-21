using Bogus;

namespace TestsBuilders.BaseBuilders
{
    public abstract class BaseBuilder
    {
        protected int _id = GenerateRandomNumber();

        protected static int GenerateRandomNumber() => new Random().Next();
        
        protected static string GenerateRandomWord() => new Faker().Random.Word();

        protected static decimal GenerateRandomDecimal() => new Faker().Random.Decimal();
    }
}
