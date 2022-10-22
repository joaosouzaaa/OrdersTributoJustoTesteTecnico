using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using OrdersTributoJustoTesteTecnico.Business.Settings.PaginationSettings;
using System.Net.Mime;
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

        public static IFormFile BuildIFormFile(string imageExtension)
        {
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");

            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(imageExtension, out contentType);

            return new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", $"image{imageExtension}")
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType,
                ContentDisposition = "form-data; name=\"Image\"; filename=\"image.jpg\""
            };
        }

        public static Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> BuildIncluadableMock<TEntity>() 
            where TEntity : class 
            =>
            It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();

        public static string GenerateRandomCpf()
        {
            var firsMultiplier = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var secondMultiplier = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpf = GenerateRandomNumberStringFrom100000000To999999999();
            cpf = BuildCpfString(cpf, firsMultiplier);
            cpf = BuildCpfString(cpf, secondMultiplier);

            return cpf;
        }

        private static string BuildCpfString(string cpf, int[] multiplier)
        {
            var sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * multiplier[i];

            var remainder = sum % 11;

            if (remainder < 2)
                remainder = 0;
            else
                remainder = 11 - remainder;

            cpf += remainder;

            return cpf;
        }

        private static string GenerateRandomNumberStringFrom100000000To999999999()
        {
            var random = new Random();
            
            return random.Next(100000000, 999999999).ToString();
        }
    }
}
