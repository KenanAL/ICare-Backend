﻿using ICare.Core.ApiDTO;
using ICare.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICare.Core.IServices
{
    public interface IWaterServices
    {
        bool AddWater(AddWaterApiDTO.Request water, int patientId);
        bool DeleteWater(int id);
        bool EditWater(EditWaterApiDTO.Request water);
        Task<Water> GetPatientWater(int patientId);
    }
}
