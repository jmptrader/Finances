﻿using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using System.Data;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Finances.Core.Interfaces;

namespace Finances.Persistence.EF
{
    public class TransferRepository : ITransferRepository
    {
        private readonly IModelContextFactory factory;
        private readonly IMappingEngine mapper;

        public TransferRepository(
                        IModelContextFactory factory, 
                        IMappingEngine mapper)
        {
            this.factory = factory;
            this.mapper = mapper;
        }


        public int Add(Core.Entities.Transfer entity)
        {
            int id=0;
            var ef = mapper.Map<Transfer>(entity);
            using (FinanceEntities context = factory.CreateContext())
            {
                context.Entry(ef).State = EntityState.Added;
                context.SaveChanges();
            }
            //read back columns which may have changed
            entity.TransferId = ef.TransferId;
            entity.RecordCreatedDateTime = ef.RecordCreatedDateTime;
            entity.RecordUpdatedDateTime = ef.RecordUpdatedDateTime;
            id = ef.TransferId;
            return id;
        }

        public bool Update(Core.Entities.Transfer entity)
        {
            var ef = mapper.Map<Transfer>(entity);
            using (FinanceEntities context = factory.CreateContext())
            {
                context.Entry(ef).State = EntityState.Modified;
                context.SaveChanges();
            }
            //read back columns which may have changed
            entity.RecordUpdatedDateTime = ef.RecordUpdatedDateTime;
            return true;
        }


        public bool Delete(Core.Entities.Transfer entity)
        {
            var ef = mapper.Map<Transfer>(entity);
            using (FinanceEntities context = factory.CreateContext())
            {
                context.Entry(ef).State = EntityState.Deleted;
                context.SaveChanges();
            }
            return true;
        }

        public bool Delete(List<int> ids)
        {
            using (FinanceEntities context = factory.CreateContext())
            {
                foreach (var id in ids)
                {
                    var ef = new Transfer() { TransferId = id };
                    context.Entry(ef).State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
            return true;
        }


        public Core.Entities.Transfer Read(int id)
        {
            Core.Entities.Transfer entity = null;
            using (FinanceEntities context = factory.CreateContext())
            {
                var ef = (from b in context.Transfers
                                .Include(a => a.FromBankAccount)
                                .Include(a => a.FromBankAccount.Bank)
                                .Include(a => a.ToBankAccount)
                                .Include(a => a.ToBankAccount.Bank)
                                .Include(a => a.TransferCategory)
                            where b.TransferId==id
                            select b).FirstOrDefault();

                entity = mapper.Map<Core.Entities.Transfer>(ef);
            }
            return entity;
        }

        public List<Core.Entities.Transfer> ReadList()
        {
            List<Core.Entities.Transfer> list = null;
            using (FinanceEntities context = factory.CreateContext())
            {
                var ef = (from b in context.Transfers
                                .Include(a => a.FromBankAccount)
                                .Include(a => a.FromBankAccount.Bank)
                                .Include(a => a.ToBankAccount)
                                .Include(a => a.ToBankAccount.Bank)
                                .Include(a => a.TransferCategory)
                            select b).ToList();

                list = mapper.Map<List<Core.Entities.Transfer>>(ef);
            }
            return list;
        }


        public List<Core.Entities.DataIdName> ReadListDataIdName()
        {
            List<Core.Entities.DataIdName> list = null;
            using (FinanceEntities context = factory.CreateContext())
            {
                list = (from b in context.Transfers
                        select b).Project(mapper).To<Core.Entities.DataIdName>().ToList();

            }
            return list;
        }


        public List<Core.Entities.TransferCategory> ReadListTransferCategories()
        {
            List<Core.Entities.TransferCategory> list = null;
            using (FinanceEntities context = factory.CreateContext())
            {
                var ef = (from b in context.TransferCategories
                            select b).ToList();

                list = mapper.Map<List<Core.Entities.TransferCategory>>(ef);
            }
            return list;
        }



    }
}
