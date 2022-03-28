using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodingChallenge
{
    public class NumberChallenge
    {   
        [Fact]
        public void Number_01()
        {
            // Rule: The static values (1), must not be changed

            var i = 0;

            Assert.Equal(1, i++);
            Assert.Equal(1, ++i);

            // Your explanation: 
            // 
            // 
        }
    }
}
