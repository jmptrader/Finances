﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finances.WinClient.DesignTimeData
{
    public class BankAccount
    {
        public BankAccount()
        {
            Bank = new Bank();
            AccountName = "Current 2";
        }

        public Bank Bank { get; set; }

        public string AccountName { get; set; }

        public string SortCode { get; set; }

        public string AccountNumber { get; set; }

    }
}
