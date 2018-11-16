// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Models.Core.Employment;
using Models.Core.Operationals;
using System.Threading.Tasks;

namespace KlipperApi.DataAccess
{
    public interface IEmployeeAccessor
    {
        Task<Employee> GetEmployeeAsync(int employeeId);
    }
}