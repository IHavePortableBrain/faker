using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Test
{
    public class QuestionWithNoCtor
    {
        public int Answer { get; set;  }
        public int UnsettedAnswer { get; }
        public int NoAnswerField;
        int _privateAnswerProp { get; set; }
        public string Mambo;

        public QuestionWithNoCtor()
        {
        }
    }
}
