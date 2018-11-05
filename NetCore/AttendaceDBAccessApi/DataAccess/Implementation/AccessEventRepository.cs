using AttendanceApi.DataAccess.Interfaces;
using ERPCore.Models.HR.Attendance;
using AttendanceApi.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceApi.DataAccess.Implementation
{
    public class AccessEventRepository : IAccessEventRepository
    {
        private readonly AttendanceContext _context = null;

        public AccessEventRepository(IOptions<DBConnectionSettings> settings)
        {
            _context = AttendanceContext.GetInstance(settings);
        }

        public async Task AddAccessEvent(AccessEvent item)
        {
            try
            {
                var filter = Builders<AccessEvent>.Filter.Eq(x => x.ID, item.ID) &
                    Builders<AccessEvent>.Filter.Eq(x => x.EmployeeID, item.EmployeeID) &
                    Builders<AccessEvent>.Filter.Eq(x => x.AccessPointIPAddress, item.AccessPointIPAddress) &
                    //Builders<AccessEvent>.Filter.Eq(x => x.EmployeeFirstName, item.EmployeeFirstName) &
                    Builders<AccessEvent>.Filter.Eq(x => x.AccessPointID, item.AccessPointID);

                var o =  _context.AccessEvents
                                .Find(filter)
                                .FirstOrDefaultAsync()
                                .Result;

                if(o == null)
                {
                    await _context.AccessEvents.InsertOneAsync(item);
                }
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<AccessEvent> GetAccessEvent(int id)
        {
            try
            {
                var filter = Builders<AccessEvent>.Filter.Eq("ID", id);

                return await _context.AccessEvents
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<AccessEvent>> GetAllAccessEvents()
        {
            try
            {
                return await _context.AccessEvents.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAccessEvent(int id)
        {
            try
            {
                DeleteResult actionResult = await _context.AccessEvents.DeleteOneAsync(
                     Builders<AccessEvent>.Filter.Eq("ID", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveAllAccessEvents()
        {
            try
            {
                DeleteResult actionResult = await _context.AccessEvents.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateAccessEvent(int id, AccessEvent item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.AccessEvents
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
