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
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _drugRepository;

        public DrugService(IDrugRepository drugRepository)
        {
            _drugRepository = drugRepository;
        }

        public bool Create(Drug drug)
        {
            return _drugRepository.Create(drug);
        }

      

        public async Task<GetDrugByIdApiDTO.Response> GetById(int drugId)
        {
            return await _drugRepository.GetById(drugId);

        }

        public async Task<IEnumerable<GetCategoryDrugsApiDTO.Response>> GetCategoryDrugs(int drugId)
        {
            return await _drugRepository.GetCategoryDrugs(drugId);

        }



    }
}
