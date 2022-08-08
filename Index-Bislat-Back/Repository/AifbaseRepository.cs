using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.EntityFrameworkCore;

namespace Index_Bislat_Back.Repository
{
    public class AifbaseRepository : IAifBase
    {
        private indexbislatContext _context;
        public AifbaseRepository(indexbislatContext context)
        {
            _context = context;
        }
        public async Task<bool> AddBase(Aifbase aifbase)
        {
            try
            { 
            _context.Add(aifbase);
            return await Save();
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }

        public async Task<ICollection<Aifbase>> GetAllBase()
        {
            return await _context.Aifbases.ToListAsync();
        }

        public async Task<bool> RemoveBase(Aifbase aifbase)
        {
            try
            {
                _context.Remove(aifbase);
                return await Save();
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }

        public async Task<bool> Save()
        {
            var saved =await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> Isexsit(string aifbase)
        {
            return await _context.Aifbases.AnyAsync(p => p.BaseName.Contains(aifbase));
        }

        //public int AddBase(string aifbases)
        //{
        //    var aifbase = _context.Aifbases.FromSqlRaw("insert into AIFBase (baseName) value ({0})", aifbases).FirstOrDefault();
        //    return aifbase.Id;
        //}

        public async Task<int> getidofCourse(string aifbase)
        {
            var afbase = await _context.Aifbases.Where(p => p.BaseName.Contains(aifbase)).FirstOrDefaultAsync();
            return afbase.Id;
        }

        public async Task<Aifbase> GetAifbaseDetails(string aifbase)
        {
            return await _context.Aifbases.Where(p => p.BaseName.Contains(aifbase)).FirstOrDefaultAsync();
        }
    }
}
