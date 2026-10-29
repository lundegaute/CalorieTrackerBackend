using Books.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Books.Helper;

public static class DatabaseTracker
{
    public static int Count<T>(EntityState state, BooksDataContext context) where T : class
    {
        var entries = context.ChangeTracker.Entries().ToList();
        return entries.Count(e => e.Entity is T && e.State == state);
    }
}
