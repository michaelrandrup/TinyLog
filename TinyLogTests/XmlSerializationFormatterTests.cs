﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyLog.Formatters;
using TinyLog;
using static TinyLogTests.LogHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyLogTests
{
    [TestClass]
    public class XmlSerializationFormatterTests : FileLogBaseClass
    {
        [TestInitialize]
        public void initialize()
        {
            log.RegisterLogFormatter(new XmlSerializationFormatter());
        }
        [TestMethod]
        [TestCategory("Public")]
        public async Task Write100LogEntries_XmlSerializationFormatter()
        {
            int num = 0;
            for (int i = 0; i < 100; i++)
            {
                LogEntry entry = LogEntry.Create(string.Format("Company Entry #{0}", i), "", LogEntrySourceDefaults.Log, "XmlSerializationFormatterTests", LogHelpers.RandomSeverity);
                bool b = await log.WriteLogEntryAsync<List<Company>>(entry, LogHelpers.GetCompanies(random.Next(1, 50), random.Next(1, 10)));
                num += b ? 1 : 0;
            }
            Assert.IsTrue(num == 100);
        }

        [TestMethod]
        [TestCategory("Public")]
        public async Task Write100CompanyObjects_XmlSerializationFormatter()
        {
            int num = 0;
            List<Company> list = LogHelpers.GetCompanies(random.Next(1, 50), random.Next(1, 10));
            foreach (Company company in list)
            {
                LogEntry entry = LogEntry.Create(string.Format("Company Entry: ", company.Name), "", LogEntrySourceDefaults.Log, "XmlSerializationFormatterTests", LogHelpers.RandomSeverity);
                bool b = await log.WriteLogEntryAsync<Company>(entry, company);
                num += b ? 1 : 0;
            }
            Assert.IsTrue(num == list.Count);
        }



    }
}
