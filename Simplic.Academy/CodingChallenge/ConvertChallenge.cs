using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace CodingChallenge
{
    public class ConvertChallenge
    {
        [Fact]
        public void FixBug_Convert_01()
        {
            // Rule: The assertation must not be changed

            var value = "24 1";

            var intValue = int.Parse(value.Replace(" ", ""));

            Assert.Equal(241, intValue);

            // Your explanation: 
            // There was a whitespace that is not supported by Parse
            // 
        }

        [Fact]
        public void FixBug_Convert_02()
        {
            // Rule: The assertation must not be changed

            var value = "24​1";

            var regex = new Regex("[^\\d]");
            var intValue = int.Parse(regex.Replace(value, ""));

            Assert.Equal(241, intValue);

            // Your explanation: 
            // There was an invisible character between the 4 and 1
            // 
        }

        [Fact]
        public void FixBug_Convert_03()
        {
            // Rule: The assertation must not be changed

            var value = "24.1";
            var culture = new CultureInfo("de-DE");

            var doubleValue = double.Parse(value.Replace(".",","), culture);

            Assert.Equal(24.1, doubleValue);

            // Your explanation: 
            // In countries where a ',' is used as the decimal point, the double.Parse() would only accept that
            // 
        }
    }
}
