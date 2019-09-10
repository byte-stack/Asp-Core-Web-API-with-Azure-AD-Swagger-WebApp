using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Business.Interface;
using WebAPI.Business.Models;
using WebAPI.DataServices.Contracts;

namespace WebAPI.Business.Logic
{
    public class BillingDetails : IBillingDetails
    {
        private readonly IBillingRepository iBillingRepository;
        public BillingDetails(IBillingRepository billingRepository)
        {
            iBillingRepository = billingRepository;
        }

        public IEnumerable<BillingDetailModel> Get()
        {
            return iBillingRepository.Get().Select(x => new BillingDetailModel {
                Amount=x.Amount,
                BillingCycleId=x.BillingCycleId,
                CreatedDate=x.CreatedDate,
                UserId=x.UserId,
                Id=x.Id
            }).ToList();
        }

        public BillingDetailModel Get(long id)
        {
            var DbBillingDetails = iBillingRepository.Get(id);
            return new BillingDetailModel
            {
                Amount = DbBillingDetails.Amount,
                BillingCycleId = DbBillingDetails.BillingCycleId,
                CreatedDate = DbBillingDetails.CreatedDate,
                UserId = DbBillingDetails.UserId,
                Id = DbBillingDetails.Id
            };
        }

        public void Post(BillingDetailModel billingDetails)
        {
            iBillingRepository.Save(new DataServices.Table.BillingDetails {
                Amount = billingDetails.Amount,
                BillingCycleId = billingDetails.BillingCycleId,
                CreatedDate = billingDetails.CreatedDate,
                UserId = billingDetails.UserId,
            });
        }

        public void Put(BillingDetailModel billingDetails)
        {
            iBillingRepository.Put(new DataServices.Table.BillingDetails
            {
                Amount = billingDetails.Amount,
                BillingCycleId = billingDetails.BillingCycleId,
                CreatedDate = billingDetails.CreatedDate,
                UserId = billingDetails.UserId,
                Id= billingDetails.Id
            });
        }

        public void Delete(long id)
        {
            iBillingRepository.Delete(id);
        }

    }
}
