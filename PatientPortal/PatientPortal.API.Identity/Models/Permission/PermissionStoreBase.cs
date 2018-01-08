﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PatientPortal.API.Identity.Models
{
    public class PermissionStoreBase
    {
        public DbContext Context { get; private set; }
        public DbSet<Permission> DbEntitySet { get; private set; }


        public IQueryable<Permission> EntitySet
        {
            get
            {
                return this.DbEntitySet;
            }
        }

        public PermissionStoreBase(DbContext context)
        {
            this.Context = context;
            this.DbEntitySet = context.Set<Permission>();
        }

        public void Create(Permission entity)
        {
            this.DbEntitySet.Add(entity);
        }


        public void Delete(Permission entity)
        {
            this.DbEntitySet.Remove(entity);
        }


        public virtual List<Permission> GetAll()
        {
            return this.DbEntitySet.ToList();
        }

        public virtual Task<List<Permission>> GetAllAsync()
        {
            return this.DbEntitySet.ToListAsync();
        }

        public virtual Task<Permission> GetByIdAsync(object id)
        {
            return this.DbEntitySet.FindAsync(new object[] { id });
        }


        public virtual Permission GetById(object id)
        {
            return this.DbEntitySet.Find(new object[] { id });
        }


        public virtual void Update(Permission entity)
        {
            if (entity != null)
            {
                this.Context.Entry<Permission>(entity).State = EntityState.Modified;
            }
        }
    }
}