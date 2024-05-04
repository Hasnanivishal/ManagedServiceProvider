using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSP.Common.Model
{
    public class OrderNotification
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ProfileId { get; set; }
    }
}
