using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodingChallenge
{
    public class StringSplitChallenge
    {
        [Fact]
        public void StringSplit_01()
        {
            // Rule: do not change the input-text or assertation

            var words = "Hello, this is  a  text !  !";

            var wordList = words.Split(new char[] { ' ' });

            Assert.Equal(7, wordList.Length);
            Assert.DoesNotContain(" ", wordList);

            // Your explanation: 
            // 
            // 
        }
    }
}
