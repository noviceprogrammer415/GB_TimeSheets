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
    internal sealed class JobRepository : IJobRepository
    {
        private readonly DbContextRepository _context;

        public JobRepository(DbContextRepository context)
        {
            _context = context;
        }

        public async Task<bool> CreateObjects(Job job)
        {
            try
            {
                await _context.AddAsync(job);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.Write($"Error! Error! {e.Message}");
                return false;
            }
        }

        public async Task<IReadOnlyList<Job>> GetObjects()
        {
            try
            {
                return await _context.Jobs.Where(j => j.IsDeleted == false).ToListAsync();
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return null;
            }
        }

        public async Task<bool> UpdateObjects(int id, Job job)
        {
            try
            {
                var jobFound = await _context.Jobs.FindAsync(id);
                jobFound = job;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Debug.Write(e.Message);
                return false;
            }
        }

        public Task<bool> DeleteObjects(int id)
        {
            return null;
        }
    }
}
