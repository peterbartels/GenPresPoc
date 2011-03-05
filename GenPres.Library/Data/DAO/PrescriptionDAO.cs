using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Linq;
using System.Text;
using GenPres.Database;
using AutoMapper;

namespace GenPres.Business.Data
{
    public class PrescriptionDAO
    {
        public static void LoadMapping()
        {
            Mapper.CreateMap<GenPres.Business.UnitValue, Database.UnitValue>()
                .ForMember(x=>x.BaseValue, opt => opt.MapFrom(src=>src.BaseValue))
            ;

            Mapper.CreateMap<Prescription, Database.Prescription>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.Frequency, opt => opt.Ignore())
                .ForMember(x => x.Medicine, opt => opt.Ignore())
                .ForMember(x => x.Drug, opt => opt.Ignore())
                .ForMember(x => x.Total, opt => opt.Ignore())
                .ForMember(x => x.Rate, opt => opt.Ignore())
                .ForMember(x => x.Time, opt => opt.Ignore())
                .ForMember(x => x.Doses, opt => opt.Ignore())
                .ForMember(x => x.AdjustWeight, opt => opt.Ignore())
                .ForMember(x => x.AdjustLength, opt => opt.Ignore())
            ;

            Mapper.CreateMap<Component, Database.Component>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.DrugConcentration, opt => opt.Ignore())
                .ForMember(x => x.Substances, opt => opt.Ignore())
            ;

            Mapper.CreateMap<Dose, Database.Dose>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.Total, opt => opt.Ignore())
                .ForMember(x => x.Rate, opt => opt.Ignore())
            ;

            Mapper.CreateMap<Drug, Database.Drug>()
                .ForMember(x => x.Components, opt => opt.Ignore())
                .ForMember(x => x.Quantity, opt => opt.Ignore())
            ;

            Mapper.CreateMap<Medicine, Database.Medicine>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.ComponentIncrement, opt => opt.Ignore())
                .ForMember(x => x.DoseIncrement, opt => opt.Ignore())
            ;

            Mapper.CreateMap<Patient, Database.Patient>();

            Mapper.CreateMap<Substance, Database.Substance>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.ComponentConcentration, opt => opt.Ignore())
                .ForMember(x => x.DrugConcentration, opt => opt.Ignore())
            ;

            Mapper.CreateMap<Database.UnitValue, UnitValue>();

            Mapper.CreateMap<Database.Prescription, Prescription>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.Frequency, opt => opt.Ignore())
                .ForMember(x => x.Medicine, opt => opt.Ignore())
                .ForMember(x => x.Drug, opt => opt.Ignore())
                .ForMember(x => x.Total, opt => opt.Ignore())
                .ForMember(x => x.Rate, opt => opt.Ignore())
                .ForMember(x => x.Time, opt => opt.Ignore())
                .ForMember(x => x.Doses, opt => opt.Ignore())
                .ForMember(x => x.AdjustWeight, opt => opt.Ignore())
                .ForMember(x => x.AdjustLength, opt => opt.Ignore())
            ;
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Drug, dest => dest.Drug);
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Medicine, dest => dest.Medicine);
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Quantity, dest => dest.Quantity);
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Total, dest => dest.Total);
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Rate, dest => dest.Rate);
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Time, dest => dest.Time);
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Frequency, dest => dest.Frequency);
            DataAccessMapper.MapKey<Database.Prescription, Prescription>(src => src.Doses, dest => dest.Doses);

            DataAccessMapper.MapKey<Database.Component, Component>(src => src.Quantity, dest => dest.Quantity);
            
            Mapper.CreateMap<Database.Component, Component>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.DrugConcentration, opt => opt.Ignore())
                .ForMember(x => x.Substances, opt => opt.Ignore())
            ;
            DataAccessMapper.MapKey<Database.Component, Component>(src => src.Quantity, dest => dest.Quantity);
            DataAccessMapper.MapKey<Database.Component, Component>(src => src.DrugConcentration, dest => dest.DrugConcentration);
            DataAccessMapper.MapKey<Database.Component, Component>(src => src.Substances, dest => dest.Substances);

            Mapper.CreateMap<Database.Dose, Dose>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.Total, opt => opt.Ignore())
                .ForMember(x => x.Rate, opt => opt.Ignore())
                .ForMember(x => x.Substance, opt => opt.Ignore())
            ;
            DataAccessMapper.MapKey<Database.Dose, Dose>(src => src.Quantity, dest => dest.Quantity);
            DataAccessMapper.MapKey<Database.Dose, Dose>(src => src.Total, dest => dest.Total);
            DataAccessMapper.MapKey<Database.Dose, Dose>(src => src.Rate, dest => dest.Rate);

            Mapper.CreateMap<Database.Drug, Drug>()
                .ForMember(x => x.Components, opt => opt.Ignore())
                .ForMember(x => x.Quantity, opt => opt.Ignore())
            ;
            DataAccessMapper.MapKey<Database.Drug, Drug>(src => src.Quantity, dest => dest.Quantity);
            DataAccessMapper.MapKey<Database.Drug, Drug>(src => src.Components, dest => dest.Components);

            Mapper.CreateMap<Database.Medicine, Medicine>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.ComponentIncrement, opt => opt.Ignore())
                .ForMember(x => x.DoseIncrement, opt => opt.Ignore())
            ;
            DataAccessMapper.MapKey<Database.Medicine, Medicine>(src => src.Quantity, dest => dest.Quantity);
            DataAccessMapper.MapKey<Database.Medicine, Medicine>(src => src.ComponentIncrement, dest => dest.ComponentIncrement);
            DataAccessMapper.MapKey<Database.Medicine, Medicine>(src => src.DoseIncrement, dest => dest.DoseIncrement);

            Mapper.CreateMap<Database.Patient, Patient>();

            Mapper.CreateMap<Database.Substance, Substance>()
                .ForMember(x => x.Quantity, opt => opt.Ignore())
                .ForMember(x => x.ComponentConcentration, opt => opt.Ignore())
                .ForMember(x => x.DrugConcentration, opt => opt.Ignore())
            ;
            DataAccessMapper.MapKey<Database.Substance, Substance>(src => src.Quantity, dest => dest.Quantity);
            DataAccessMapper.MapKey<Database.Substance, Substance>(src => src.ComponentConcentration, dest => dest.ComponentConcentration);
            DataAccessMapper.MapKey<Database.Substance, Substance>(src => src.DrugConcentration, dest => dest.DrugConcentration);

            Mapper.CreateMap<Double, Decimal>().ConvertUsing(src => Convert.ToDecimal(src));
            Mapper.CreateMap<Decimal, Double>().ConvertUsing(src => Convert.ToDouble(src));
        }
        public static Database.Prescription MapFrom(Prescription prescription, Database.Prescription prDAO)
        {
            Mapper.Map(prescription, prDAO);
            return prDAO;
        }

        public static Database.Prescription MapTo(Database.Prescription prDAO, Prescription prescription)
        {
            Prescription.GetPrescription(prDAO);
            return prDAO;
        }
    }
}
