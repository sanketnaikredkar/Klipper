using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.DataAccess.Database
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthContext _context = null;
        readonly ILogger _logger = Log.ForContext<RoleRepository>();

        public RoleRepository(IOptions<DBConnectionSettings> settings)
        {
            _context = AuthContext.GetInstance(settings);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            try
            {
                return await _context.Roles.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while accessing Role list.");
                throw ex;
            }
        }

        public async Task<Role> Get(string name)
        {
            try
            {
                var filter = Builders<Role>.Filter.Eq("Name", name);

                return await _context.Roles
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while accessing Role with name {@Name}.", name);
                throw ex;
            }
        }

        public async Task<bool> Add(Role item)
        {
            try
            {
                var filter = Builders<Role>.Filter.Eq(x => x.Name, item.Name);
                var o = _context.Roles
                                .Find(filter)
                                .FirstOrDefaultAsync()
                                .Result;
                if (o == null)
                {
                    await _context.Roles.InsertOneAsync(item);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while adding Role with ID {@Name}.", item.Name);
                throw ex;
            }
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                DeleteResult actionResult = await _context.Roles.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while removing Role list.");
                throw ex;
            }
        }

        public async Task<bool> Remove(string name)
        {
            try
            {
                DeleteResult actionResult = await _context.Roles.DeleteOneAsync(
                     Builders<Role>.Filter.Eq("Name", name));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while removing Role with name {@Name}.", name);
                throw ex;
            }
        }

        public async Task<bool> UpdateRoleDocument(string name, string title)
        {
            var item = await Get(name) ?? new Role();
            return await Update(name, item);
        }

        public async Task<bool> Update(string name, Role item)
        {
            try
            {
                item.Name = name; //Make sure name of the item is assigned.
                ReplaceOneResult actionResult = await _context.Roles
                                                .ReplaceOneAsync(n => n.Name.Equals(name)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while updating Role with ID {@Name}.", name);
                throw ex;
            }
        }

        public async Task<bool> Exists(string name)
        {
            var filter = Builders<Role>.Filter.Eq(s => s.Name, name);
            try
            {
                var r = await _context.Roles.FindAsync(filter);
                if(r.Current.GetEnumerator().Current == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Warning("Something went wrong while querying existence of an Role with ID {@Name}.", name);
                throw ex;
            }
        }
    }
}
