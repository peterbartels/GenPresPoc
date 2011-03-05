using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Operations;

namespace GenPres.Business
{
//**
// * This class stores and performs the calculations 
// * form a unit. The multiplier is set so multiplication
// * by the multiplier results the value to change to 
// * the value in base units. For example for weight the base unit 
// * is gram. Therefore, the unit kg has multiplier 1000.
// * 1 kg = 1000 * 1 gram.
// * 
// * The reverse, restoring a value from base value to unit value 
// * can also be performed.
// * 
// * @author hal
// * @version 1.0
// *
// */

public class Unit {
	private String _name;
	private decimal _multiplier;
	private UnitConverter.UnitGroup _group;
	
	internal Unit(String name,
			       decimal multiplier,
			       UnitConverter.UnitGroup group){
		this._name = name;
		this._multiplier = multiplier;
		this._group = group;
	}
	
	
	public String GetUnitName(){
		return _name;
	}
	
	public UnitConverter.UnitGroup GetGroup(){
		return _group;
	}
	
	public decimal GetMultiplier(){
		return _multiplier;
	}
	
	public decimal GetBaseValue(decimal value){
		return value * _multiplier;
	}
	
	public decimal GetUnitValue(decimal value){
		return value / _multiplier;
	}
}
}
