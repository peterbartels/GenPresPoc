using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GenPres.Business.WebService
{
    public class GenFormService : IGenFormService
    {
        
        public static XDocument genericsDoc = XDocument.Load(@"C:\Development\GenPres\GenPres.Web\generics.xml");
        
        public string[] GetGenerics(string route, string shape)
        {    
            var generics = (genericsDoc.Descendants("generic").Where(
                generic => 
                    generic.Attribute("shape").Value == ((shape.Trim() == "") ? generic.Attribute("shape").Value : shape) &&
                    generic.Attribute("route").Value == ((route.Trim() == "") ? generic.Attribute("route").Value : route)
            ));
            return (from i in generics select i.Attribute("name").Value).Distinct().ToArray();
        }

        public string[] GetRoutes(string generic, string shape)
        {
            

            var routes = (genericsDoc.Descendants("generic").Where(
                route =>
                    route.Attribute("shape").Value == ((shape.Trim() == "") ? route.Attribute("shape").Value : shape) &&
                    route.Attribute("name").Value == ((generic.Trim() == "") ? route.Attribute("name").Value : generic)
            ));
            return (from i in routes select i.Attribute("route").Value).Distinct().ToArray();
        }

        public string[] GetShapes(string generic, string route)
        {
            XDocument genericsDoc = XDocument.Load(@"C:\Development\GenPres\GenPres.Web\generics.xml");
            var shapes = (genericsDoc.Descendants("generic").Where(
                shape =>
                    shape.Attribute("name").Value == ((generic.Trim() == "") ? shape.Attribute("name").Value : generic) &&
                    shape.Attribute("route").Value == ((route.Trim() == "") ? shape.Attribute("route").Value : route)
            ));
            return (from i in shapes select i.Attribute("shape").Value).Distinct().ToArray();
        }

        public decimal[] GetComponentIncrements(string generic, string route, string shape)
        {
            return new[]{1m};
        }

        public decimal[] GetSubstanceIncrements(string generic, string route, string shape)
        {
            return new[] { 0.05m, 0.075m, 0,1m, 0.225m, 0.5m };
        }

        public ReadOnlyCollection<SelectionItem> GetSubstanceUnits(string generic, string route, string shape)
        {
            var items = genericsDoc.Descendants("generic").Where(
                item =>
                    item.Attribute("shape").Value == shape &&
                    item.Attribute("route").Value == route &&
                    item.Attribute("name").Value == generic
            );

            if(items.Count() == 0) return new ReadOnlyCollection<SelectionItem>(new List<SelectionItem>());

            return
                (items.Descendants("substanceUnit").Select(
                    i => new SelectionItem()
                    {
                        selected = bool.Parse(i.Attribute("selected").Value),
                        Value =  i.Value
                    }
                )).ToList().AsReadOnly();
        }

        public ReadOnlyCollection<SelectionItem> GetComponentUnits(string generic, string route, string shape)
        {
            XDocument genericsDoc = XDocument.Load(@"C:\Development\GenPres\GenPres.Web\generics.xml");
            var items = genericsDoc.Descendants("generic").Where(
                item =>
                    item.Attribute("shape").Value == shape && 
                    item.Attribute("route").Value == route && 
                    item.Attribute("name").Value == generic
            );

            if (items.Count() == 0) return new ReadOnlyCollection<SelectionItem>(new List<SelectionItem>());

            return
                (items.Descendants("componentUnit").Select(
                    i => new SelectionItem()
                    {
                        selected = bool.Parse(i.Attribute("selected").Value),
                        Value = i.Value
                    }
                )).ToList().AsReadOnly();
        }
    }
}
