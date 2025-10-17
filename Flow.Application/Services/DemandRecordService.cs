using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Flow.Application.Models;
using Flow.Infrastructure.DbContexts;

namespace Flow.Application.Services;

public interface IDemandRecordService {
    Task<List<DemandRecordVm>> GetAllDemandRecordsAsync();
}

public sealed class DemandRecordService : IDemandRecordService {
    private readonly IDbContextFactory<FlowDbContext> _flowDbContextFactory;
    private readonly IMemoryCache _memoryCache;
    private readonly CancellationToken _cancellationToken;
    
    public DemandRecordService(IDbContextFactory<FlowDbContext> flowDbContextFactory, IMemoryCache memoryCache, CancellationToken cancellationToken) {
        _flowDbContextFactory = flowDbContextFactory;
        _memoryCache = memoryCache;
        _cancellationToken = cancellationToken;
    }

    public async Task<List<DemandRecordVm>> GetAllDemandRecordsAsync() {
        const string cacheKey = nameof(GetAllDemandRecordsAsync);
        if (_memoryCache.TryGetValue(cacheKey, out List<DemandRecordVm>? demandRecords)) return demandRecords ?? [];

        await using FlowDbContext flowDbContext = await _flowDbContextFactory.CreateDbContextAsync(_cancellationToken);
        List<DemandRecordVm> result = await flowDbContext.DemandRecords
            .Select(dr => new DemandRecordVm {
                DemandRecordId = dr.DemandRecordId,
                Demand = dr.Demand,
                Site = dr.Site,
                Timestamp = dr.Timestamp
            }).ToListAsync(_cancellationToken);
        
        return _memoryCache.Set(cacheKey, result, absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5));
    }
}