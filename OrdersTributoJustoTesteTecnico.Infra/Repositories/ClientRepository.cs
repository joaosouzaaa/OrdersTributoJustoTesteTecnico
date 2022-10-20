using Microsoft.EntityFrameworkCore;
using OrdersTributoJustoTesteTecnico.Business.Interfaces.Repositories;
using OrdersTributoJustoTesteTecnico.Domain.Entities;
using OrdersTributoJustoTesteTecnico.Infra.Contexts;
using OrdersTributoJustoTesteTecnico.Infra.Repositories.BaseRepositories;

namespace OrdersTributoJustoTesteTecnico.Infra.Repositories
{
    public sealed class ClientRepository : BaseCommandsRepository<Client>, IClientRepository
    {
        public ClientRepository(OrdersTributoJustoTesteTecnicoDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Client> GetClientByIdAsync(int id) => await _dbContextSet.FirstOrDefaultAsync(c => c.Id == id);
    }
}
