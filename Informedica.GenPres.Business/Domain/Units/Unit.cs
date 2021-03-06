﻿using System;

namespace Informedica.GenPres.Business.Domain.Units
{
    public class Unit
    {
        private String _name;
        private decimal _multiplier;
        private UnitConverter.UnitGroup _group;

        internal Unit(String name,
                       decimal multiplier,
                       UnitConverter.UnitGroup group)
        {
            this._name = name;
            this._multiplier = multiplier;
            this._group = group;
        }


        public String GetUnitName()
        {
            return _name;
        }

        public UnitConverter.UnitGroup GetGroup()
        {
            return _group;
        }

        public decimal GetMultiplier()
        {
            return _multiplier;
        }

        public decimal GetBaseValue(decimal value)
        {
            return value * _multiplier;
        }

        public decimal GetUnitValue(decimal value)
        {
            return value / _multiplier;
        }
    }
}
