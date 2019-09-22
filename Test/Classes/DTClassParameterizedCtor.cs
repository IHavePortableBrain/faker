using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Test
{
    public class DTClassParameterizedCtor
    {
        internal long longg;
        public DateTime DateTime;
        internal string str;

        public DTClassParameterizedCtor(long longg,string str)
        {
            this.longg = longg;
            this.str = str;
        }
    }
}
