using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.EntityFrameworkCore;

namespace Index_Bislat_Back.Repository
{
    public class ChoisetableRepository : IChoisetable
    {
        private indexbislatContext _context;
        public ChoisetableRepository(indexbislatContext context)
        {
            _context = context;
        }
        public async Task<bool> AddChoise(Choisetable choise)
        {
            try
            { 
             await _context.Choisetables.AddAsync(choise);
            return await Save();
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }

        public async Task<ICollection<Choisetable>> GetAllChoise(int sortid)
        {
            return await _context.Choisetables.Where(p => p.Sortid == sortid).ToListAsync();
        }

        public async Task<Choisetable> GetChoise(string id, int sortid)
        {
            return await _context.Choisetables.FirstOrDefaultAsync(p => p.Id.Contains(id)&& p.Sortid == sortid);
        }

        public async Task<int> GetSortId(string name)
        {

            var sort = await _context.SortCycles.FirstOrDefaultAsync(p => p.Name.Contains(name));
            if (sort == null) return -1;
                  return sort.Sortid;
        }

        public async Task<bool> Isexsit(string id, int sortid)
        {
            try
            {
                return await _context.Choisetables.AnyAsync(p => p.Id.Contains(id)&& p.Sortid == sortid);
            }
            catch (Exception e) { Console.WriteLine(e.Message); return true; }

        }

        public async Task<bool> Removechoise(Choisetable choise)
        {
            try
            { 
            _context.Choisetables.Remove(choise);
                return await Save();
            }
            catch (Exception e) { Console.WriteLine(e.Message); return true; }
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
