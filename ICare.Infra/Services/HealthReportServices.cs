﻿using ICare.Core.ApiDTO;
using ICare.Core.Data;
using ICare.Core.IRepository;
using ICare.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICare.Infra.Services
{
    public class HealthReportServices : IHealthReportServices
    {
        private readonly IHealthReportRepository _HealthReportRepository;

        public HealthReportServices(IHealthReportRepository HealthReportRepository)
        {
            this._HealthReportRepository = HealthReportRepository;
        }
        public bool Create(HealthReport healthReport)
        {
            return _HealthReportRepository.Create(healthReport);
        }

        public bool Delete(int id)
        {
            return _HealthReportRepository.Delete(id);
        }

        public IEnumerable<HealthReport> GetAll()
        {
            return _HealthReportRepository.GetAll();
        }

        public HealthReport GetById(int id)
        {
            return _HealthReportRepository.GetById(id);
        }

        public bool Update(HealthReport healthReport)
        {
            return _HealthReportRepository.Update(healthReport);
        }
        public async Task<IEnumerable<GetHelethReportsApiDTO.Reponse>> GetAllHelthReportByMonth(int patienId, int month, int year)
        {
            return await _HealthReportRepository.GetAllHealthReportByMonth(patienId,month,year);

        }
    }
}
