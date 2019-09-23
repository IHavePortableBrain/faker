using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Test
{
    public class QuestionWithCtor
    {
        public int Answer { get; }
        public int UnsettedAnswer { get; }
        public int AnswerField;
        int _privateAnswerProp { get; set; }
        

        public QuestionWithCtor(int answer)
        {
            Answer = answer;
        }
    }
}
