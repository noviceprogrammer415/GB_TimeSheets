﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeSheets.DAL.Interfaces;
using TimeSheets.DAL.Models;
using TimeSheets.DAL.Repositories.Context;
using TimeSheets.DTO;

namespace TimeSheets.DAL.Repositories
{
    internal sealed class InvoicesRepository : IInvoicesRepository
    {
        private readonly DbContextRepository _context;

        public InvoicesRepository(DbContextRepository context)
        {
            _context = context;
        }

        public async Task<bool> CreateObjects(Invoice invoice)
        {
            try
            {
                await _context.AddAsync(invoice);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.Write($"Error! Error! {e.Message}");
                return false;
            }
        }

        public async Task<IReadOnlyList<Invoice>> GetObjects()
        {
            try
            {
                return await _context.Invoices.Where(i => i.IsDeleted == false).ToListAsync();
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return null;
            }
        }

        public Task<bool> UpdateObjects(int id, Invoice invoice)
        {
            return null;
        }

        public Task<bool> DeleteObjects(int id)
        {
            return null;
        }
    }
}
