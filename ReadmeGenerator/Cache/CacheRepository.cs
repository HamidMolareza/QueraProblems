using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnRail;
using OnRail.Extensions.Try;
using Quera.Collector.Models;

namespace Quera.Cache;

public class CacheRepository(CacheDbContext db) {
    public Task<Result<List<CacheProblem>>> SaveNewItemsAsync(IEnumerable<Problem> problems) =>
        TryExtensions.Try(async () => {
            var newProblemsQuery = from problem in problems
                join cacheProblem in db.Problems on problem.QueraId.ToString() equals cacheProblem.Id into g
                from cacheProblem in g.DefaultIfEmpty()
                where cacheProblem == null
                select problem;
            var newCacheData = newProblemsQuery.Select(newItem => new CacheProblem {
                Id = newItem.QueraId.ToString(),
                Title = newItem.QueraTitle!
            }).ToList();

            db.Problems.AddRange(newCacheData);
            await db.SaveChangesAsync();

            return newCacheData;
        });

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
}