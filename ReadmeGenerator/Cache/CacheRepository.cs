using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.Try;
using Quera.Collector.Models;

namespace Quera.Cache;

public class CacheRepository(CacheDbContext db) {
    public Result<List<Problem>> Join(List<Problem?> problems) =>
        TryExtensions.Try(() => {
            var query = from problem in problems
                join cacheProblem in db.Problems on problem.QueraId.ToString() equals cacheProblem.Id
                    into g
                from cacheProblem in g.DefaultIfEmpty()
                where problem != null
                select new Problem {
                    QueraId = problem.QueraId,
                    LastSolutionsCommit = problem.LastSolutionsCommit,
                    Contributors = problem.Contributors,
                    Solutions = problem.Solutions,
                    QueraTitle = cacheProblem?.Title
                };
            return query.ToList();
        });

    public Task SaveAsync(CacheProblem cacheProblem) {
        db.Problems.Add(cacheProblem);
        return db.SaveChangesAsync();
    }
}