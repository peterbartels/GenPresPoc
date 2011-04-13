using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public static class UnitConverter
    {
        public const decimal KILO = 1000;
        public const decimal DECI = 0.1m;
        public const decimal CENTI = 0.01m;
        public const decimal MILLI = 0.001m;
        public const decimal MICRO = MILLI * MILLI;
        public const decimal NANO = MICRO * MILLI;

        public const decimal GRAM = 1;
        public const decimal KG = KILO * GRAM;
        public const decimal MG = MILLI * GRAM;
        public const decimal MCG = MICRO * GRAM;
        public const decimal NANOG = NANO * GRAM;

        public const decimal MOL = 1;
        public const decimal MMOL = MILLI * MOL;

        public const decimal LITER = 1;
        public const decimal ML = MILLI * LITER;

        public const decimal METER = 1;
        public const decimal DM = DECI * METER;
        public const decimal CM = CENTI * METER;

        public const decimal SEC_PER_MIN = 60;
        public const decimal MIN_PER_HOUR = 60;
        public const decimal HOUR_PER_DAY = 24;
        public const decimal DAY_PER_WEEK = 7;
        public const decimal WEEK_PER_YEAR = 52;
        public const decimal MONTH_PER_YEAR = 12;

        public const decimal SEC = 1;
        public const decimal MSEC = MILLI * SEC;
        public const decimal MIN = SEC_PER_MIN;
        public const decimal HOUR = MIN_PER_HOUR * MIN;
        public const decimal DAY = HOUR_PER_DAY * HOUR;
        public const decimal WEEK = DAY_PER_WEEK * DAY;
        public const decimal YEAR = WEEK_PER_YEAR * WEEK;
        public const decimal MONTH = YEAR / MONTH_PER_YEAR;

        public const decimal JOULE = 1;
        public const decimal CAL = 4.1868m;
        public const decimal KJ = KILO * JOULE;
        public const decimal KCAL = KILO * CAL;

        public const decimal UNIT = 1;

        public enum UnitGroup
        {
            TIME,
            MASS,
            MOLAR,
            LENGTH,
            AREA,
            SUBSTANCE,
            UNIT,
            VOLUME,
            ENERGY,
            PHARM,
        }

        private static List<Unit> units;


        private static void PopulateUnits()
        {
            units.Add(new Unit("kg", KILO, UnitGroup.MASS));
            units.Add(new Unit("gram", GRAM, UnitGroup.MASS));
            units.Add(new Unit("g", GRAM, UnitGroup.MASS));
            units.Add(new Unit("mg", MG, UnitGroup.MASS));
            units.Add(new Unit("mcg", MCG, UnitGroup.MASS));
            units.Add(new Unit("microg", MCG, UnitGroup.MASS));
            units.Add(new Unit("nanog", NANOG, UnitGroup.MASS));
            units.Add(new Unit("mol", MOL, UnitGroup.SUBSTANCE));
            units.Add(new Unit("mmol", MMOL, UnitGroup.SUBSTANCE));
            units.Add(new Unit("liter", LITER, UnitGroup.VOLUME));
            units.Add(new Unit("L", LITER, UnitGroup.VOLUME));
            units.Add(new Unit("mL", ML, UnitGroup.VOLUME));
            units.Add(new Unit("ml", ML, UnitGroup.VOLUME));
            units.Add(new Unit("m", METER, UnitGroup.LENGTH));
            units.Add(new Unit("cm", CM, UnitGroup.LENGTH));
            units.Add(new Unit("second", SEC, UnitGroup.TIME));
            units.Add(new Unit("sec", SEC, UnitGroup.TIME));
            units.Add(new Unit("seconde", SEC, UnitGroup.TIME));
            units.Add(new Unit("min", MIN, UnitGroup.TIME));
            units.Add(new Unit("hour", HOUR, UnitGroup.TIME));
            units.Add(new Unit("day", DAY, UnitGroup.TIME));
            units.Add(new Unit("uur", HOUR, UnitGroup.TIME));
            units.Add(new Unit("dag", DAY, UnitGroup.TIME));
            units.Add(new Unit("week", WEEK, UnitGroup.TIME));
            units.Add(new Unit("month", MONTH, UnitGroup.TIME));
            units.Add(new Unit("year", YEAR, UnitGroup.TIME));
            units.Add(new Unit("E", UNIT, UnitGroup.UNIT));
            units.Add(new Unit("tabl", UNIT, UnitGroup.PHARM));
            units.Add(new Unit("", UNIT, UnitGroup.PHARM));
            units.Add(new Unit("stuk", UNIT, UnitGroup.PHARM));
            units.Add(new Unit("amp", UNIT, UnitGroup.PHARM));
            units.Add(new Unit("supp", UNIT, UnitGroup.PHARM));
            units.Add(new Unit("caps", UNIT, UnitGroup.PHARM));
            units.Add(new Unit("m2", UNIT, UnitGroup.AREA));
            units.Add(new Unit("J", JOULE, UnitGroup.ENERGY));
            units.Add(new Unit("KJ", KJ, UnitGroup.ENERGY));
            units.Add(new Unit("Cal", CAL, UnitGroup.ENERGY));
            units.Add(new Unit("KCal", KCAL, UnitGroup.ENERGY));
            units.Add(new Unit("mmol", MMOL, UnitGroup.MOLAR));
            units.Add(new Unit("mol", MOL, UnitGroup.MOLAR));
        }

        public static Unit GetUnitById(int id)
        {
            return GetUnitList()[id];
        }

        public static Unit GetUnitByName(String name)
        {
            Unit unit = null;

            foreach (Unit item in GetUnitList().ToList<Unit>())
            {
                if (item == null) continue;
                if (item.GetUnitName().Equals(name))
                {
                    unit = item;
                    return unit;
                }
            }
            return GetUnitByName("stuk");
            throw new Exception("Unit not found:" + name);
        }

        public static List<Unit> GetUnitList()
        {
            if (units == null)
            {
                units = new List<Unit>();
                PopulateUnits();
            }
            return units;
        }

        public static List<Unit> GetUnitListByGroup(UnitGroup group)
        {
            List<Unit> list = new List<Unit>();

            foreach (Unit item in GetUnitList())
            {
                if (item.GetGroup() == group) list.Add(item);
            }

            return list;
        }

        public static decimal GetBaseValue(Unit unit, decimal value)
        {
            return unit.GetBaseValue(value);
        }

        public static decimal GetUnitValue(Unit unit, decimal value)
        {
            return unit.GetUnitValue(value);
        }

        public static decimal GetBaseValue(String name, decimal value)
        {
            return GetBaseValue(GetUnitByName(name), value);
        }

        public static decimal GetUnitValue(String name, decimal value)
        {
            return GetUnitValue(GetUnitByName(name), value);
        }

        public static decimal ConvertValue(Unit fromUnit,
                                          Unit toUnit,
                                          decimal value)
        {
            if (fromUnit.GetGroup() == toUnit.GetGroup())
            {
                value = fromUnit.GetBaseValue(value);
                value = toUnit.GetUnitValue(value);
            }

            return value;
        }

        public static decimal ConvertValue(String fromUnit,
                                          String toUnit,
                                          decimal value)
        {
            value = ConvertValue(GetUnitByName(fromUnit),
                                 GetUnitByName(toUnit),
                                 value);
            return value;
        }
    }
}
