using AttendaceDBAccessApi.DataAccess.Interfaces;
using ERPCore.Models.HR.Attendance;
using AttendaceDBAccessApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendaceDBAccessApi.DataAccess.Implementation
{
    public class AccessPointRepository : IAccessPointRepository
    {
        private readonly HRContext _context = null;

        public AccessPointRepository(IOptions<DBConnectionSettings> settings)
        {
            _context = HRContext.GetInstance(settings);
        }

        public async Task AddAccessPoint(AccessPoint item)
        {
            try
            {
                var filter = Builders<AccessPoint>.Filter.Eq(x => x.ID, item.ID) &
                    Builders<AccessPoint>.Filter.Eq(x => x.IpAddress, item.IpAddress);
                    //& Builders<AccessPoint>.Filter.Eq(x => x.Name, item.Name);

                var o = _context.AccessPoints
                                .Find(filter)
                                .FirstOrDefaultAsync()
                                .Result;

                if (o == null)
                {
                    await _context.AccessPoints.InsertOneAsync(item);
                }
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<AccessPoint> GetAccessPoint(int id)
        {
            try
            {
                var filter = Builders<AccessPoint>.Filter.Eq("ID", id);

                return await _context.AccessPoints
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<AccessPoint>> GetAllAccessPoints()
        {
            try
            {
                return await _context.AccessPoints.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAccessPoint(int id)
        {
            try
            {
                DeleteResult actionResult = await _context.AccessPoints.DeleteOneAsync(
                     Builders<AccessPoint>.Filter.Eq("ID", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllAccessPoints()
        {
            try
            {
                DeleteResult actionResult = await _context.AccessPoints.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateAccessPoint(int id, AccessPoint item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.AccessPoints
                                                .ReplaceOneAsync(n => n.ID.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

    }
}
