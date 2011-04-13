using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain;

namespace GenPres.Business.Data
{
    internal class NewPrescriptionAssembler : INewPrescriptionAssembler
    {
        internal NewPrescriptionDto CreateDto(Prescription p)
        {
            NewPrescriptionDto dto = DtoFactory.Create<NewPrescriptionDto>();
            return dto;
        }

        internal void MapToDto(Prescription p, NewPrescriptionDto dto)
        {
            
            throw new NotImplementedException();
        }
    }
}
