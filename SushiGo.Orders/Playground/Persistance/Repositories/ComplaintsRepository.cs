using Microsoft.EntityFrameworkCore;
using Playground.Entities;

namespace Playground.Persistance.Repositories;

public interface IComplaintsRepository
{
    Task<List<Complaint>> GetAllAsync(CancellationToken cancellationToken = default);
}

internal sealed class ComplaintsRepository(PlaygroundContext context) : IComplaintsRepository
{
    private DbSet<Complaint> _complaints => context.Set<Complaint>();


    public Task<List<Complaint>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _complaints.ToListAsync(cancellationToken);
    }
}