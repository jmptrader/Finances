﻿using System;
using System.Collections.Generic;
using System.Linq;

using Finances.Core.Entities;

namespace Finances.Core.Engines.Cashflow
{
    public class DetailAggregatedProjectionItemsGenerator : IAggregatedProjectionItemsGenerator
    {
        public ProjectionModeEnum ProjectionMode
        {
            get
            {
                return ProjectionModeEnum.Detail;
            }
        }

        public List<Entities.CashflowProjectionItem> GenerateAggregatedProjectionItems(List<CashflowProjectionTransfer> cashflowProjectionTransfers)
        {
            var cpis = new List<CashflowProjectionItem>();

            foreach (var cpt in cashflowProjectionTransfers.OrderBy(c => c.Date))
            {
                var cpi = new CashflowProjectionItem()
                {
                    Period = cpt.Date.ToString("yyyy-MM-dd"),
                    PeriodStartDate = cpt.Date,
                    PeriodEndDate = cpt.Date,
                    Item = (cpt.TransferDirection.Transfer.Category.Code == "NONE" ? "" : cpt.TransferDirection.Transfer.Category.Name + " -> ") + cpt.TransferDirection.Transfer.Name,
                    In = cpt.TransferDirection.IsInbound ? cpt.TransferDirection.Transfer.Amount : (decimal?)null,
                    Out = cpt.TransferDirection.IsOutbound ? cpt.TransferDirection.Transfer.Amount : (decimal?)null
                };

                cpis.Add(cpi);
            }

            return cpis;
        }
    }
}