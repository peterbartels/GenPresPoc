using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace GenPres.Business.Data
{
    public class UnitValueDTO
    {
        public decimal Value;
        public string UIState;
        public string Time;
        public string Adjust;
        public string Total;
        public string Unit;
        public decimal[] Increments;
        public bool AllowIncrementStep;
    }

    public class MedicineDTO
    {
        public string Generic;
        public string Shape;
        public string Route;
        public UnitValueDTO Quantity { get; set; }
        public UnitValueDTO DoseIncrement { get; set; }
        public UnitValueDTO PrescriptionIncrement { get; set; }
    }

    public class PrescribeMedicationDTO 
    {
        public string DrugName { get; set; }
        public string DrugShape { get; set; }
        public string DrugRoute { get; set; }
        public UnitValueDTO DoseQuantity { get; set; }
        public UnitValueDTO DoseTotal { get; set; }
        public UnitValueDTO DoseRate { get; set; }
        public UnitValueDTO Frequency { get; set; }
        public UnitValueDTO Time { get; set; }
        public UnitValueDTO PrescriptionQuantity { get; set; }
        public UnitValueDTO PrescriptionTotal { get; set; }
        public UnitValueDTO PrescriptionRate { get; set; }
        public UnitValueDTO SubstanceQuantity { get; set; }
        public UnitValueDTO SubstanceDrugConcentration { get; set; }
        public UnitValueDTO DrugQuantity { get; set; }

        public static PrescribeMedicationDTO MapFrom(Prescription p)
        {
            Mapper.Reset();
            Mapper.CreateMap<UnitValue, UnitValueDTO>();
            
            Mapper.CreateMap<Prescription, PrescribeMedicationDTO>()
                .ForMember(dest => dest.DoseQuantity, opt => opt.MapFrom(src => src.Doses[0].Quantity))
                .ForMember(dest => dest.DoseTotal, opt => opt.MapFrom(src => src.Doses[0].Total))
                .ForMember(dest => dest.DoseRate, opt => opt.MapFrom(src => src.Doses[0].Rate))
                .ForMember(dest => dest.PrescriptionQuantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.PrescriptionTotal, opt => opt.MapFrom(src => src.Total))
                .ForMember(dest => dest.PrescriptionRate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.SubstanceQuantity, opt => opt.MapFrom(src => src.Doses[0].Substance.Quantity))
                .ForMember(dest => dest.SubstanceDrugConcentration, opt => opt.MapFrom(src => src.Doses[0].Substance.DrugConcentration))
            ;
            PrescribeMedicationDTO prDto = Mapper.Map<Prescription, PrescribeMedicationDTO>(p);
            return prDto;
        }

        public static Prescription MapTo(PrescribeMedicationDTO prDto, Prescription prescription)
        {
            Mapper.Reset();
            Mapper.CreateMap<UnitValueDTO, UnitValue>();
            Mapper.CreateMap<PrescribeMedicationDTO, Prescription>();
            Mapper.Map(prDto, prescription);

            Mapper.CreateMap<PrescribeMedicationDTO, Prescription>()
                .ConvertUsing<PrescriptionConverter>()
            ;
            
            Mapper.Map(prDto, prescription);
            return prescription;
        }
    }

    public class PrescriptionConverter : AutoMapper.ITypeConverter<PrescribeMedicationDTO, Prescription> 
    {
        public Prescription Convert(ResolutionContext rc)
        {
            Prescription p = (Prescription) rc.DestinationValue;
            PrescribeMedicationDTO pDto = (PrescribeMedicationDTO) rc.SourceValue;
            
            p.Drug.Name = pDto.DrugName;
            p.Drug.Shape = pDto.DrugShape;
            p.Drug.Route = pDto.DrugRoute;

            Mapper.Map(pDto.DrugQuantity, p.Drug.Quantity);
            
            Mapper.Map(pDto.DoseQuantity, p.Doses[0].Quantity);
            Mapper.Map(pDto.DoseTotal, p.Doses[0].Total);
            Mapper.Map(pDto.DoseRate, p.Doses[0].Rate);

            Mapper.Map(pDto.PrescriptionQuantity, p.Quantity);
            Mapper.Map(pDto.PrescriptionTotal, p.Total);
            Mapper.Map(pDto.PrescriptionRate, p.Rate);

            Mapper.Map(pDto.SubstanceQuantity, p.Doses[0].Substance.Quantity);
            Mapper.Map(pDto.SubstanceDrugConcentration, p.Doses[0].Substance.DrugConcentration);
            
            return p;
        }
    } 
}
