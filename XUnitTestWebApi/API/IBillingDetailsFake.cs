using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPI.Business.Interface;
using WebAPI.Business.Models;

namespace WebAPI.XUnitTest.API
{
    class IBillingDetailsFake : IBillingDetails
    {
        private readonly List<BillingDetailModel> _billingDetails;
        public IBillingDetailsFake()
        {
            _billingDetails = new List<BillingDetailModel>()
            {
                new BillingDetailModel() { Id = 1,Amount=10,BillingCycleId =1,CreatedDate=DateTime.Now,UserId=1},
                new BillingDetailModel()  { Id = 2,Amount=20,BillingCycleId =1,CreatedDate=DateTime.Now,UserId=1},
                new BillingDetailModel()  { Id = 3,Amount=30,BillingCycleId =1,CreatedDate=DateTime.Now,UserId=1},
            };
        }

        public void Delete(long id)
        {
            var existing = _billingDetails.Where(a => a.Id == id).FirstOrDefault();
            _billingDetails.Remove(existing);
        }

        public IEnumerable<BillingDetailModel> Get()
        {
            return _billingDetails;
        }

        public BillingDetailModel Get(long id)
        {
            return _billingDetails.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Post(BillingDetailModel billingDetails)
        {
            if(billingDetails.Amount==0)
            {
                throw new Exception("Invalid Model state.");
            }
            _billingDetails.Add(billingDetails);
        }

        public void Put(BillingDetailModel billingDetails)
        {
            var existing = _billingDetails.Where(a => a.Id == billingDetails.Id).FirstOrDefault();
            if (existing==null)
            {
                throw new Exception("Invalid Model state.");
            }
            existing.Amount = billingDetails.Amount;
        }
    }
}
