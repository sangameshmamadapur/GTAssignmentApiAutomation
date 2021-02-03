using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using GTAssignment.Models;

namespace GTAssignment.Utils
{
    public static class ConfigReader
    {
        public static TestConfig ParseConfig()
        {
            byte[] json = File.ReadAllBytes("Config/config.json");
            TestConfig config = JsonSerializer.Deserialize<TestConfig>(json);
            return config;
        }
    }
}
