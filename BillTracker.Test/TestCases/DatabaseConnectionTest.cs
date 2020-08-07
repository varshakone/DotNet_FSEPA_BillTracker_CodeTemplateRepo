using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Microsoft.Extensions.Options;
using BillTracker.DataLayer;
using MongoDB.Driver;
using Xunit;
using BillTracker.Entities;
using BillTracker.Test.Utility;

namespace BillTracker.Test.Bill.TestCases
{
  public  class DatabaseConnectionTest
    {
        static FileUtility fileUtility;
        private Mock<IOptions<MongoSettings>> _mockOptions;
        private Mock<IMongoDatabase> _mockDB;
        private Mock<IMongoClient> _mockClient;
        public DatabaseConnectionTest()
        {
            _mockOptions = new Mock<IOptions<MongoSettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();
        }
        static DatabaseConnectionTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_database_revised.txt";
            fileUtility.CreateTextFile();
        }
        [Fact]
        public void MongoBookDBContext_Constructor_Success()
        {
            try {
                var settings = new MongoSettings()
                {
                    Connection = "mongodb://user:password@127.0.0.1:27017/BillTrackerDB",
                    DatabaseName = "BillTrackerDB"
                };
                _mockOptions.Setup(s => s.Value).Returns(settings);
                _mockClient.Setup(c => c
                .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                    .Returns(_mockDB.Object);

                //Action
                var context = new MongoDBContext(_mockOptions.Object);
                if (context != null)
                {
                    //Write test result in text file

                    String testResult = "MongoBookDBContext_Constructor_Success= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                }
                //Assert 
                Assert.NotNull(context);
            }
            catch(Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "MongoBookDBContext_Constructor_Success= False";
                fileUtility.WriteTestCaseResuItInText(testResult);
            }
            }


        [Fact]
        public void MongoBookDBContext_GetCollection_ValidName_Success()
        {
            try
            {
                //Arrange
                var settings = new MongoSettings()
                {
                    Connection = "mongodb://user:password@127.0.0.1:27017/BillTrackerDB",
                    DatabaseName = "BillTrackerDB"
                };

                _mockOptions.Setup(s => s.Value).Returns(settings);

                _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null)).Returns(_mockDB.Object);

                //Action 
                var context = new MongoDBContext(_mockOptions.Object);
                var myCollection = context.GetCollection<BillDetails>("BillDetails");
                if (myCollection != null)
                {
                    //Write test result in text file

                    String testResult = "MongoBookDBContext_GetCollection_ValidName_Success= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                }
                //Assert 
                Assert.NotNull(myCollection);
            }
            catch(Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "MongoBookDBContext_GetCollection_ValidName_Success= False";
                fileUtility.WriteTestCaseResuItInText(testResult);
            }
        }
    }
}
