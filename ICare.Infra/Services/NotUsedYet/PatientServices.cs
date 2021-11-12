﻿using ICare.Core.Data;
using ICare.Core.IRepository;
using ICare.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICare.Infra.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly IPatientRepository _patientRepository;

        public PatientServices(IPatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }
        public bool Create(Patient patient)
        {
            return _patientRepository.Create(patient);
        }

        public bool Delete(int id)
        {
            return _patientRepository.Delete(id);
        }

        public IEnumerable<Patient> GetAll()
        {
            return _patientRepository.GetAll();
        }

        public Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }

        public bool Update(Patient patient)
        {
            return _patientRepository.Update(patient);
        }
    }
}