using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Test
{
    public class RecurrentDependent
    {
        public RecurrentDependent2 Recurrent2;
        public RecurrentDependent RecurrentProperty { get; set; }
        public int Intt;
    }
}
