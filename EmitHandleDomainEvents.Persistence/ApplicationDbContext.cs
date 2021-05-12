using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmitHandleDomainEvents.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmitHandleDomainEvents.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private IDomainEventDispatcher _dispatcher;

        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<Sprint> Sprints { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventDispatcher dispatcher) : base(options)
        {
            _dispatcher = dispatcher;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await DispatchDomainEvents();
            return 0;
            //var res = await base.SaveChangesAsync(cancellationToken);
            //return res;
        }

        private async Task DispatchDomainEvents()
        {
            var domainEventEntities = ChangeTracker.Entries<IEntity>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                while (entity.DomainEvents.TryTake(out var domainEvent))
                    await _dispatcher.Dispatch(domainEvent);
            }
        }
    }
}