using Microsoft.AspNetCore.Http;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using System.Text;

namespace TestsBuilders.Helpers
{
    public static class BuildersUtils
    {
        public static PageList<TEntity> BuildPageList<TEntity>(List<TEntity> entityList)
            where TEntity : class
        {
            return new PageList<TEntity>
            {
                CurrentPage = 1,
                PageSize = 10,
                Result = entityList,
                TotalCount = entityList.Count,
                TotalPages = 1
            };
        }

        public static IFormFile BuildIFormFile()
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");

            return new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "image.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg",
                ContentDisposition = "form-data; name=\"Image\"; filename=\"image.jpg\""
            };
        }
    }
}
