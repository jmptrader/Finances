﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finances.Core.Entities;
using Finances.Core.Factories;
using Finances.Core.Engines;
using Finances.Core.Interfaces;
//using Finances.WinClient.Factories;

namespace Finances.IntegrationTests.MS.Fakes
{
    public class FakeScheduleFactory : IScheduleFactory
    {
        public Core.Entities.Schedule Create()
        {
            return new Schedule(new IScheduleFrequency[] { 
                                new ScheduleFrequencyMonthly(),
                                new ScheduleFrequencyWeekly() });
        }

        public void Release(Core.Entities.Schedule s)
        {
        }
    }
}
