using Microsoft.EntityFrameworkCore;

namespace dotnetApiCode.Infra
{
    public class PessoaRepository
    {
        private readonly AppDbContext _context;

        public PessoaRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Pessoa pessoa)
        {
            _context.Add(pessoa);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Pessoa>> Get()
        {
            return await _context.Pessoas
            .AsNoTracking()
            .ToListAsync();
        }
    }
}