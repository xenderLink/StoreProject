using Store.Data;

namespace Store.Models;

public class EFCategoriesRepository : ICategoriesRepository
{

    private StoreDbContext? context;

    public EFCategoriesRepository(StoreDbContext ctx)
    {
        context = ctx;
    }

    public IEnumerable<Category> Categories => context.categories;
    public IQueryable <ChildCategory> Childs => context.childs;

}