using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.DataAccess.Database
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AuthContext _context = null;
        readonly ILogger _logger = Log.ForContext<PermissionRepository>();

        public PermissionRepository(IOptions<DBConnectionSettings> settings)
        {
            _context = AuthContext.GetInstance(settings);
        }

        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            try
            {
                return await _context.Permissions.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while accessing Permission list.");
                throw ex;
            }
        }

        public async Task<Permission> Get(string name)
        {
            try
            {
                var filter = Builders<Permission>.Filter.Eq("Name", name);

                return await _context.Permissions
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while accessing Permission with Name {@Name}.", name);
                throw ex;
            }
        }

        public async Task<bool> Add(Permission item)
        {
            try
            {
                var filter = Builders<Permission>.Filter.Eq(x => x.Name, item.Name);
                var o = _context.Permissions
                                .Find(filter)
                                .FirstOrDefaultAsync()
                                .Result;
                if (o == null)
                {
                    await _context.Permissions.InsertOneAsync(item);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while adding Permission with Name {@Name}.", item.Name);
                throw ex;
            }
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                DeleteResult actionResult = await _context.Permissions.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while removing Permission list.");
                throw ex;
            }
        }

        public async Task<bool> Remove(string name)
        {
            try
            {
                DeleteResult actionResult = await _context.Permissions.DeleteOneAsync(
                     Builders<Permission>.Filter.Eq("Name", name));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while removing Permission with Name {@Name}.", name);
                throw ex;
            }
        }

        public async Task<bool> UpdatePermissionDocument(string name, string title)
        {
            var item = await Get(name) ?? new Permission();
            return await Update(name, item);
        }

        public async Task<bool> Update(string name, Permission item)
        {
            try
            {
                item.Name = name; //Make sure Name of the item is assigned.
                ReplaceOneResult actionResult = await _context.Permissions
                                                .ReplaceOneAsync(n => n.Name.Equals(name)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while updating Permission with Name {@Name}.", name);
                throw ex;
            }
        }

        public async Task<bool> Exists(string name)
        {
            var filter = Builders<Permission>.Filter.Eq(s => s.Name, name);
            try
            {
                var r = await _context.Permissions.FindAsync(filter);
                if(r.Current.GetEnumerator().Current == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while querying existence of an Permission with Name {@Name}.", name);
                throw ex;
            }
        }
    }
}
