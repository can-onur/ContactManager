using MongoDB.Driver;
using Report.Domain.Aggregates.ReportAggregate;
using System.Linq.Expressions;

namespace Report.Infrastructure.Persistence.Repositories
{
    public class LocationReportRepository : ILocationReportRepository
    {
        private readonly IMongoCollection<LocationInformationReport> _collection;

        public LocationReportRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<LocationInformationReport>("LocationReports");
        }

        public async Task<LocationInformationReport> GetByIdAsync(Guid id)
        {
            var filter = Builders<LocationInformationReport>.Filter.Eq(x => x.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<LocationInformationReport>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<LocationInformationReport>> FindAsync(Expression<Func<LocationInformationReport, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task AddAsync(LocationInformationReport locationReport)
        {
            await _collection.InsertOneAsync(locationReport);
        }

        public async Task UpdateAsync(LocationInformationReport locationReport)
        {
            var filter = Builders<LocationInformationReport>.Filter.Eq(x => x.Id, locationReport.Id);
            await _collection.ReplaceOneAsync(filter, locationReport);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<LocationInformationReport>.Filter.Eq(x => x.Id, id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
