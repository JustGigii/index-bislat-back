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
        public bool AddBase(Aifbase aifbase)
        {
            _context.Add(aifbase);
            return Save();
        }

        public ICollection<Aifbase> GetAllBase()
        {
            return _context.Aifbases.ToList();
        }

        public bool RemoveBase(Aifbase aifbase)
        {
            _context.Remove(aifbase);
            return Save();
        }

        public bool UpDateBase(Aifbase aifbase)
        {
            _context.Update(aifbase);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Isexsit(string aifbase)
        {
            return _context.Aifbases.Any(p => p.BaseName.Contains(aifbase));
        }

        public int AddBase(string aifbases)
        {
            var aifbase = _context.Aifbases.FromSqlRaw("insert into AIFBase (baseName) value ({0})", aifbases).FirstOrDefault();
            return aifbase.Id;
        }

        public int getidofCourse(string aifbase)
        {
            var afbase = _context.Aifbases.Where(p => p.BaseName.Contains(aifbase)).FirstOrDefault();
            return afbase.Id;
        }
    }
}
