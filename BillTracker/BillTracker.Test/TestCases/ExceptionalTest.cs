using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BillTracker.BusinessLayer.Interface;
using BillTracker.BusinessLayer.Service;
using BillTracker.BusinessLayer.Service.Repository;
using BillTracker.DataLayer;
using BillTracker.Entities;
using BillTracker.Test.Exceptions;
using BillTracker.Test.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace BillTracker.Test.Bill.TestCases
{
    [Collection("parallel")]
    public  class ExceptionalTest
    {
        private User _user;
        IConfigurationRoot config;
        private BillDetails _bill;
        MongoSettings settings;
        private readonly MongoDBContext context;
        static FileUtility fileUtility;

        private readonly IBillService _billService;
        private readonly IUserService _userService;
        private readonly IBillRepository _billRepository;
        private readonly IUserRepository _userRepository;

        static ExceptionalTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_exception_revised.txt";
            fileUtility.CreateTextFile();
        }

        public ExceptionalTest()
        {
            _bill = new BillDetails
            {
                Title = "Electricity Bill",
                Catagory = BillCategory.Office,
                Amount = 0,
                DateOfEntry = null,
                DueDate = null,
                PaymentMode = BillPaymentMode.Bank_Transfer,
                Status = BillStatus.Unpaid
            };
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            MongoDBUtility mongoDBUtility = new MongoDBUtility();
            context = mongoDBUtility.MongoDBContext;
            _userRepository = new UserRepository(context);
            _billRepository = new BillRepository(context);
            _userService = new UserService(_userRepository);
            _billService = new BillService(_billRepository);
        }


        /// <summary>
        /// generate new bill by submitting bill null details and check throws exception
        /// </summary>
        [Fact]
        public async void ExceptionTestFor_GenerateBill_Null_Failure()
        {
            try
            {
                // Arrange
                _bill = null;

                  //Act 
                var billresult =await  _billService.SaveBillAsync(_bill);

                if (billresult.Equals(false))
                {
                   //Write test result in text file

                    String testResult = " ExceptionTestFor_GenerateBill_Null_Failure=False";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file

                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "ExceptionTestFor_GenerateBill_Null_Failure",
                            expectedOutput = "False",
                            weight = 2,
                            mandatory = "False",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }

                }
                else
                {
                    // Assert
                     Assert.False(billresult);
                }
            }

            catch (Exception ex)
            {
                var error = ex;
             
                //Write test result in text file

                String testResult = " ExceptionTestFor_GenerateBill_Null_Failure=" +"True";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "ExceptionTestFor_GenerateBill_Null_Failure",
                        expectedOutput = "True",
                        weight = 2,
                        mandatory = "True",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// retrieve list of paid and unpaid bill and assert it is empty
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ExceptionTestFor_ListOfPaidAndUnpaidBillsNotFound()
        {
            try
            {
               //Action
                var getbillList =await _billService.GetAllBillsAsync();
            
                if (getbillList ==null)
                {
                    
                    //Write test result in text file

                    String testResult = "ExceptionTestFor_ListOfPaidAndUnpaidBillsNotFound= False";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "ExceptionTestFor_ListOfPaidAndUnpaidBillsNotFound",
                            expectedOutput = "False",
                            weight = 2,
                            mandatory = "False",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {

                    //Assert
                     Assert.Empty(getbillList);
                   
                }
            }
            catch (Exception ex)
            {
                var error = ex;

                //Write test result in text file

                String testResult = "ExceptionTestFor_ListOfPaidAndUnpaidBillsNotFound= True";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "ExceptionTestFor_ListOfPaidAndUnpaidBillsNotFound",
                        expectedOutput = "True",
                        weight = 2,
                        mandatory = "True",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// retrieve list of bill filtered by due date and check it throws exception or empty list
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ExceptionTestFor_ListBillsByDueDate_NotFound()
        {
            try
            {
                //Action
                var getbillList =await  _billService.GetAllBillsByDueDateAsync(DateTime.Parse("07/27/2020"));

                if (getbillList == null)
                {
                   
                    //Write test result in text file

                    String testResult = "ExceptionTestFor_ListBillsByDueDate_NotFound= False";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "ExceptionTestFor_ListBillsByDueDate_NotFound",
                            expectedOutput = "False",
                            weight = 2,
                            mandatory = "False",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {

                    //Assert
                    Assert.Empty(getbillList);

                }
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "ExceptionTestFor_ListBillsByDueDate_NotFound= True";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "ExceptionTestFor_ListBillsByDueDate_NotFound",
                        expectedOutput = "True",
                        weight = 2,
                        mandatory = "True",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }


        /// <summary>
        /// create new user by submitting null user details and check it throws exception as expected
        /// </summary>
        [Fact]
        public async void ExceptionTestFor_RegisterNewUser_Null_Failure()
        {
            try
            {
                // Arrange
                _user = null;

                //Act 
                
                var register =await _userService.RegisterNewUser(_user);
                if (register != null)
                {

                    //Write test result in text file

                    String testResult = "ExceptionTestFor_RegisterNewUser_Null_Failure= False";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "ExceptionTestFor_RegisterNewUser_Null_Failure",
                            expectedOutput = "False",
                            weight = 2,
                            mandatory = "False",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                    // Assert
                }
                else
                {
                    Assert.NotEqual("User Register Successfully", register);
                }
            }

            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "ExceptionTestFor_RegisterNewUser_Null_Failure= True";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file

                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "ExceptionTestFor_RegisterNewUser_Null_Failure",
                        expectedOutput = "True",
                        weight = 2,
                        mandatory = "True",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// verify user by passing user credentials and check it returns null or throws exception as expected
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ExceptionTestFor_UserNotFound()
        {
            try
            {

                //Action
                var getUser =await _userService.VerifyUser(new UserLogin()
                {
                    UserName = "User123",
                    Password = "pass12",
                });
                if (getUser != null)
                {

                    //Write test result in text file

                    String testResult = "ExceptionTestFor_UserNotFound= False";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file

                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Exception",
                            Name = "ExceptionTestFor_UserNotFound",
                            expectedOutput = "False",
                            weight = 2,
                            mandatory = "False",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    //Assert
                    Assert.NotNull(getUser);
                }
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "ExceptionTestFor_UserNotFound= True";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Exception",
                        Name = "ExceptionTestFor_UserNotFound",
                        expectedOutput = "True",
                        weight = 2,
                        mandatory = "True",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }
    }
}
