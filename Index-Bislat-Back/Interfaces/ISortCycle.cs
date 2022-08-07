using index_bislatContext;

namespace Index_Bislat_Back.Interfaces
{
    public interface ISortCycle
    {
        Task<ICollection<SortCycle>> GetAllSortCycles();
        Task<SortCycle> GetSortCycleDetails(string sortName);
        Task<bool> IsExist(string Name);   
        Task<bool> AddSortCycle(SortCycle sort,ICollection<string> coursesNumber);
        Task<bool> UpdateSort(SortCycle sort, ICollection<string> courses);
        Task<bool> DeleteSort(string sortName);
    }
}
