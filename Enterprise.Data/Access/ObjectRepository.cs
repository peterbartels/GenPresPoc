using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Enterprise.Data.Access
{
    public class ObjectRepository
    {
        private DataContext _context;

        public ObjectRepository(DataContext context)
        {
            _context = context;
        }
    }
}
