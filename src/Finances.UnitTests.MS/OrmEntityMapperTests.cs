﻿using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Finances_Persistence = Finances.Persistence;

namespace Finances.UnitTests.MS
{
    [TestClass]
    public class OrmEntityMapperTests
    {
        IMappingEngine _mapper;

        [TestInitialize]
        public void Initialize()
        {

            new Finances.Persistence.EF.MappingCreator().CreateMappings();

            _mapper = Mapper.Engine;
        }


        [TestMethod]
        public void Test_Bank_EntityToOrm()
        {
            var entity = new Core.Entities.Bank() { BankId=1, Name="one", Logo=new byte[] { 1, 2, 3 } };

            var orm = _mapper.Map<Finances.Persistence.EF.Bank>(entity);


            CompareBanks(entity, orm);
        }

        [TestMethod]
        public void Test_Bank_OrmToEntity()
        {
            var orm = new Finances.Persistence.EF.Bank() { BankId = 1, Name = "one", Logo = new byte[] { 1, 2, 3 } };

            var entity = _mapper.Map<Core.Entities.Bank>(orm);

            CompareBanks(entity, orm);
        }


        private void CompareBanks(Core.Entities.Bank entity, Finances.Persistence.EF.Bank orm)
        {
            Assert.AreEqual(entity.BankId, orm.BankId, "BankId");
            Assert.AreEqual(entity.Name, orm.Name, "BankName");
            Assert.AreEqual(entity.Logo, orm.Logo, "Logo");
        }


        [TestMethod]
        public void Test_BankAccount_EntityToOrm()
        {
            var entity = new Core.Entities.BankAccount()
            {
                BankAccountId = 1,
                Bank = new Core.Entities.Bank() { BankId = 1, Name = "one", Logo = new byte[] { 1, 2, 3 } },
                AccountNumber = "1234",
                AccountOwner = "me",
                Name = "savings",
                LoginID = "login",
                LoginURL = "abc",
                PasswordHint = "hint",
                OpenedDate = DateTime.Now.Date,
                ClosedDate = DateTime.Now.Date.AddDays(1),
                SortCode = "00-00-10",
                InitialRate = 1.1M,
                PaysTaxableInterest = true,
                MilestoneDate = DateTime.Now.Date.AddDays(2),
                MilestoneNotes = "msnotes",
                Notes = "test notes"
            };

            var orm = _mapper.Map<Finances.Persistence.EF.BankAccount>(entity);

            CompareBankAccounts(entity, orm, false);
        }


        [TestMethod]
        public void Test_BankAccount_OrmToEntity()
        {
            var orm = new Finances.Persistence.EF.BankAccount()
            {
                BankAccountId = 1,
                Bank = new Finances.Persistence.EF.Bank() { BankId = 1, Name = "one", Logo = new byte[] { 1, 2, 3 } },
                AccountNumber = "1234",
                AccountOwner = "me",
                Name = "savings",
                LoginID = "login",
                LoginURL = "abc",
                PasswordHint = "hint",
                OpenedDate = DateTime.Now.Date,
                ClosedDate = DateTime.Now.Date.AddDays(1),
                SortCode = "00-00-10",
                InitialRate = 1.1M,
                PaysTaxableInterest = true,
                MilestoneDate = DateTime.Now.Date.AddDays(2),
                MilestoneNotes = "msnotes",
                Notes = "test notes"
            };

            var entity = _mapper.Map<Core.Entities.BankAccount>(orm);

            CompareBankAccounts(entity, orm, true);
        }


        
        private void CompareBankAccounts(Core.Entities.BankAccount entity, Finances.Persistence.EF.BankAccount orm, bool isToOrm)
        {
            Assert.AreEqual(entity.BankAccountId, orm.BankAccountId, "BankAccountId");
            Assert.AreEqual(entity.Name, orm.Name, "Name");
            
            if(isToOrm)
                CompareBanks(entity.Bank, orm.Bank);
            else
                Assert.AreEqual(entity.Bank.BankId, orm.BankId, "BankId");

            Assert.AreEqual(entity.SortCode, orm.SortCode, "SortCode");
            Assert.AreEqual(entity.AccountNumber, orm.AccountNumber, "AccountNumber");
            Assert.AreEqual(entity.AccountOwner, orm.AccountOwner, "AccountOwner");
            Assert.AreEqual(entity.PaysTaxableInterest, orm.PaysTaxableInterest, "PaysTaxableInterest");
            Assert.AreEqual(entity.LoginURL, orm.LoginURL, "LoginURL");
            Assert.AreEqual(entity.LoginID, orm.LoginID, "LoginID");
            Assert.AreEqual(entity.PasswordHint, orm.PasswordHint, "PasswordHint");
            Assert.AreEqual(entity.OpenedDate, orm.OpenedDate, "OpenedDate");
            Assert.AreEqual(entity.ClosedDate, orm.ClosedDate, "ClosedDate");
            Assert.AreEqual(entity.InitialRate, orm.InitialRate, "InitialRate");
            Assert.AreEqual(entity.MilestoneDate, orm.MilestoneDate, "MilestoneDate");
            Assert.AreEqual(entity.MilestoneNotes, orm.MilestoneNotes, "MilestoneNotes");
            Assert.AreEqual(entity.Notes, orm.Notes, "Notes");
        }



    }
}
