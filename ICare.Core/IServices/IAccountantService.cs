﻿using ICare.Core.ApiDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICare.Core.IServices
{
    public interface IAccountantService
    {
        Task<IEnumerable<CareSystemReportDTO>> DailyCareSystemReport();
        Task<IEnumerable<CareSystemReportDTO>> MonthlyCareSystemReport();
        Task<IEnumerable<CareSystemReportDTO>> AnnualCareSystemReport();
        getRegisteredDailyCountDTO getRegisteredDailyCount();
        getRegisteredMonthlyCountDTO getRegisteredMonthlyCount();

        getRegisteredAnnualCountDTO getRegisteredAnnualCount();

        getMonthlyEmployeeSalariesDTO getMonthlyEmployeeSalaries();

        getAnnualEmployeeSalariesDTO getAnnualEmployeeSalaries();
    }
}
