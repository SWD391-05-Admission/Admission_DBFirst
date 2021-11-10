using Admission.Bussiness.Response;
using Admission.Data.Models;
using Admission.Data.Repository;
using System.Collections.Generic;

namespace Admission.Bussiness.Service
{
    public interface IAdmisstionService
    {
        IEnumerable<AdmissionFormRes> GetAdmissionForms();
    }

    public class AdmissionService : IAdmisstionService
    {
        private readonly IAdmissionRepository _iAdmissionRepository;

        public AdmissionService(IAdmissionRepository iAdmissionRepository)
        {
            _iAdmissionRepository = iAdmissionRepository;
        }

        public IEnumerable<AdmissionFormRes> GetAdmissionForms()
        {
            var adForms = _iAdmissionRepository.GetAdmissionForms();
            if (adForms != null)
            {
                List<AdmissionFormRes> adFormsReq = new();

                foreach (AdmissionForm adForm in adForms)
                {
                    adFormsReq.Add(new AdmissionFormRes
                    {
                        Id = adForm.Id,
                        Method = adForm.Method,
                        Description = adForm.Description
                    });
                }
                return adFormsReq;
            }
            return null;
        }
    }
}
