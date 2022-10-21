using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using TestsBuilders.BaseBuilders;

namespace TestsBuilders
{
    public class PageParamsBuilders : BaseBuilder
    {
        public static PageParamsBuilders NewObject() => new PageParamsBuilders();

        public PageParams DomainBuild() =>
            new PageParams()
            {
                pageNumber = GenerateRandomNumber(),
                pageSize = GenerateRandomNumber()
            };
    }
}
