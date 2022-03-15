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
        public async Task<IEnumerable<Pessoa>> GetAll(int skip, int take)
        {
            return await _context.Pessoas
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();
        }
        public async Task<Pessoa?> GetById(int id){
            return await _context.Pessoas.AsNoTracking().Where(p=> p.Id == id).FirstAsync();

        }
        public async Task<bool> Update(Pessoa pessoa){
            _context.Update(pessoa);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(Pessoa pessoa){
            _context.Remove(pessoa);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}