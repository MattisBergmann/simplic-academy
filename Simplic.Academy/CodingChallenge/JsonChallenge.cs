using Newtonsoft.Json;
using System;
using Xunit;

namespace CodingChallenge
{
    public class SampleJsonPoco
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class JsonChallenge
    {
        [Fact]
        public void FixBug_Json_01()
        {
            var json = @"
                {
                    ""name"": ""Max""
                    ""age"": 12
                }
            ";

            var obj = JsonConvert.DeserializeObject<SampleJsonPoco>(json);

            Assert.Equal("Max", obj.Name);
            Assert.Equal(12, obj.Age);

            // Your explanation: 
            // 
            // 
        }

        [Fact]
        public void FixBug_Json_02()
        {
            var json = @"
                {
                    ""name"": ""Max"",
                    ""age"": ""12years""
                }
            ";

            var obj = JsonConvert.DeserializeObject<SampleJsonPoco>(json);

            Assert.Equal("Max", obj.Name);
            Assert.Equal(12, obj.Age);

            // Your explanation: 
            // 
            // 
        }

        [Fact]
        public void FixBug_Json_03()
        {
            var json = @"
                {
                    ""name"": ""Max"",
                    ""birthday"": ""2000-03-04Z19:30:45.00T""
                }
            ";

            var obj = JsonConvert.DeserializeObject<SampleJsonPoco>(json);

            Assert.Equal("Max", obj.Name);
            Assert.Equal(new DateTime(2000, 4, 3, 19, 30, 45), obj.Birthday);

            // Your explanation: 
            // 
            // 
        }
    }
}
