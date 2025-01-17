﻿using ICare.Core.Data;
using ICare.Core.IRepository;
using ICare.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICare.Infra.Services
{
    public class SubscriptionTypeServices : ISubscriptionTypeServices
    {
        private readonly ISubscriptionTypeRepository _subscriptionTypeRepository;

        public SubscriptionTypeServices(ISubscriptionTypeRepository subscriptionTypeRepositorys)
        {
            this._subscriptionTypeRepository = subscriptionTypeRepositorys;
        }

        public async Task<IEnumerable<SubscribeType>> GetAll()
        {
        return await _subscriptionTypeRepository.GetAll();

        }
        public async Task<SubscribeType> GetById(int id)
        {
            return await _subscriptionTypeRepository.GetById(id);

        }

        public bool EditSubscribeType(SubscribeType subscribeType)
        {
            return  _subscriptionTypeRepository.EditSubscribeType(subscribeType);

        }

        //public bool Create(SubscribeType subscribeType)
        //{
        //    return _subscriptionTypeRepository.Create(subscribeType);
        //}

        //public bool Delete(int id)
        //{
        //    return _subscriptionTypeRepository.Delete(id);
        //}

        //public IEnumerable<SubscribeType> GetAll()
        //{
        //    return _subscriptionTypeRepository.GetAll();
        //}

        //public SubscribeType GetById(int id)
        //{
        //    return _subscriptionTypeRepository.GetById(id);
        //}

        //public bool Update(SubscribeType subscribeType)
        //{
        //    return _subscriptionTypeRepository.Update(subscribeType);
        //}
    }
}
