using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business
{
    [TestClass]
    public class AnEmployeeShould
    {
        [TestMethod]
        public void BeAbleToReturnTheNameThatWasGiven()
        {
            string employeeName = "lkjkl";
            var employee = new Employee();
            employee.Name = employeeName;
            Assert.AreEqual(employeeName, employee.Name);
        }
    }

    [TestClass]
    public class AnEmployeeDtoMapperShould
    {
        private Employee _employee;
        private EmployeeDto _employeeDto;

        [TestInitialize]
        public void Init()
        {
            _employee = new Employee();
            _employeeDto = new EmployeeDto();
        }

        [TestMethod]
        public void MapAnEmployeeNameToAEmployeeDtoName()
        {
            var employeeName = "klk";
            _employee.Name = employeeName;
            EmployeeDtoMapper.MapToDto(_employee, _employeeDto);
            Assert.AreEqual(_employee.Name, _employeeDto.Name);
        }

        [TestMethod]
        public void MapAnEmployeeAddressToAnEmployeeDtoAddress()
        {
            var employeeAddress = "street";
            _employee.Address = employeeAddress;
            EmployeeDtoMapper.MapToDto(_employee, _employeeDto);
            Assert.AreEqual(_employee.Address, _employeeDto.Address);
        }
    }

    public class EmployeeDtoMapper
    {
        public static void MapToDto(Employee employee, EmployeeDto employeeDto)
        {
            employeeDto.Name = employee.Name;
            employeeDto.Address = employee.Address;
        }
    }

    public class EmployeeDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }
}
