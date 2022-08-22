using Index_Bislat_Back.Interfaces;
using index_bislatContext;
using Microsoft.EntityFrameworkCore;

namespace Index_Bislat_Back.Repository
{
    public class SortCycleRepository : ISortCycle
    {
        private readonly indexbislatContext _context;

        public SortCycleRepository(indexbislatContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSortCycle(SortCycle sort, ICollection<string> coursesNumber)
        {
            try
            {
                _context.Add(sort);
                if (!await Save())
                    return false;
                int sortId = sort.Sortid;
                List<int> coursetableList = new List<int>();
                foreach (var item in coursesNumber)
                {
                    var course = _context.Coursetables.FirstOrDefault(p => p.CourseNumber.Contains(item));
                    if (course is object)
                        _context.Add(new Couseofsort() { CourseId = course.CourseId, Sortid = sortId });
                }
                return await Save();
            }
            catch (Exception err) { Console.WriteLine(err.Message); return false; }
        }

        public async Task<bool> DeleteSort(string sortName)
        {
            try
            { 
            var sort = await _context.SortCycles.Where(e => e.Name.Contains(sortName)).FirstOrDefaultAsync();
            foreach (var item in _context.Couseofsorts.Where(p=> p.Sortid == sort.Sortid))
             _context.Couseofsorts.Remove(item);
                foreach (var item in _context.Choisetables.Where(p => p.Sortid == sort.Sortid))
                    _context.Choisetables.Remove(item);
                if (!await Save())return false;
            _context.SortCycles.Remove(sort);
            return await Save();
            }
            catch (Exception err) { Console.WriteLine(err.Message); return false; }
        }

        public async Task<ICollection<SortCycle>> GetAllSortCycles()
        {
            return await _context.SortCycles.ToListAsync();
        }

        public async Task<SortCycle> GetSortCycleDetails(string sortName)
        {
           return await _context.SortCycles.Include(p=> p.Couseofsorts).ThenInclude(i=> i.Course).FirstOrDefaultAsync(e => e.Name.Contains(sortName));
        }

        public async Task<bool> IsExist(string name)
        {
            return await _context.SortCycles.AnyAsync(p => p.Name.Contains(name));
        }

        public async Task<bool> UpdateSort(SortCycle sort, ICollection<string> courses)
        {
            try
            { return await DeleteSort(sort.Name) && await AddSortCycle(sort, courses); }
            catch (Exception err) { Console.WriteLine(err.Message); return false; }
        }

        private async Task<bool> Save()
        {
            int saved = await _context.SaveChangesAsync();
            return saved >0? true: false;
        }
    }
}
