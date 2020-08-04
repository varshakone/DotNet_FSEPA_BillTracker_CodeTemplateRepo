using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using BillTracker.BusinessLayer.Interface;
using BillTracker.BusinessLayer.Service;
using BillTracker.BusinessLayer.Service.Repository;
using BillTracker.DataLayer;
using BillTracker.Entities;
using BillTracker.Test.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;
namespace BillTracker.Test.Bill.TestCases
{
    [CollectionDefinition("parallel",DisableParallelization = false)]
   public class BoundaryTest
    {
        private User _user;
        private UserLogin _userLogin;
        IConfigurationRoot config;
        private Mock<IMongoDBContext> _mockContext;
        MongoDBContext context;
        private BillDetails _bill;
        MongoSettings settings;
        private readonly IBillService _billService;
        private readonly IUserService _userService;
        private readonly IBillRepository _billRepository;
        private readonly IUserRepository _userRepository;
        static FileUtility fileUtility;

        static BoundaryTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_boundary_revised.txt";
            fileUtility.CreateTextFile();

        }
        public BoundaryTest()
        {
            _bill = new BillDetails
            {
                Title = "Electricity Bill",
                Catagory = BillCategory.Office,
                Amount =1000,
                DateOfEntry = DateTime.Now,
                DueDate = DateTime.Now.AddDays(30),
                PaymentMode = BillPaymentMode.Bank_Transfer,
                Status = BillStatus.Unpaid
            };

            _user = new User
            {
                UserName = "User",
                Password = "password12",
                ConfirmPassword = "password12",
                Email = "user@gmail.com",
                UserType = UserType.Visitor
            };
            _userLogin = new UserLogin
            {
                UserName = "User",
                Password = "password12",
            };
            MongoDBUtility mongoDBUtility = new MongoDBUtility();
            context = mongoDBUtility.MongoDBContext;
            _userRepository = new UserRepository(context);
            _billRepository = new BillRepository(context);
            _userService = new UserService(_userRepository);
            _billService = new BillService(_billRepository);
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }

        /// <summary>
        /// Validate bill title is empty or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BoundaryTestFor_BillTitleAsync()
        {
            try
            {            

                bool validBillTitle = false;
                if(_bill.Title != "")
                {
                    //Action
                    var result =await _billService.SaveBillAsync(_bill);
                    validBillTitle = true;

                    //Write test result in text file
                    
                    String testResult = "BoundaryTestFor_BillTitleAsync=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_BillTitleAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                             
                //Assert
                Assert.True(validBillTitle);
            }
            catch (Exception ex)
            {
                var error = ex;
                String testResult = "BoundaryTestFor_BillTitleAsync = False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_BillTitleAsync",
                        expectedOutput = "False",
                        weight = 0,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Validate Bill amount is entered or empty
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task BoundaryTestFor_BillAmountAsync()
        {
            try
            {
                

                bool validBillAmount = false;
                if (_bill.Amount != 0)
                {
                    validBillAmount = true;
                
                    //Write test result in text file
                    String testResult = "BoundaryTestFor_BillAmountAsync = " + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file

                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_BillAmountAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                        
                    

                    //Action
                     var result =await _billService.SaveBillAsync(_bill);
                }
              
                //Assert
                Assert.True(validBillAmount);
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file
                String testResult = "BoundaryTestFor_BillAmountAsync= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_BillAmountAsync",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Validate bill entry date empty or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BoundaryTestFor_BillDateOfEntryAsync()
        {
            try
            {
             

                bool validEntryDate = false;
                if (_bill.DateOfEntry != null)
                {
                    validEntryDate = true;
                   //Write test result in text file
                    String testResult = "BoundaryTestFor_BillDateOfEntryAsync=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_BillDateOfEntryAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                    //Action
                    var result = await _billService.SaveBillAsync(_bill);
                }
                

                //Assert
                Assert.True(validEntryDate);
            }
            catch (Exception ex)
            {
                var error = ex;

                String testResult = "BoundaryTestFor_BillDateOfEntryAsync= False\n";
                fileUtility.WriteTestCaseResuItInText(testResult);
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_BillDateOfEntryAsync",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Validate bill due date is empty or not  
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BoundaryTestFor_BillDueDateAsync()
        {
            try
            {
              

                bool validDueDate;
                if (_bill.DueDate != null)
                {
                    validDueDate = true;
                    
                    String testResult = "BoundaryTestFor_BillDueDateAsync=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_BillDueDateAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                    //Action
                    var result = await _billService.SaveBillAsync(_bill);
                }
                else
                {
                    validDueDate = false;
                   String testResult = "BoundaryTestFor_BillDueDateAsync=" + "False";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_BillDueDateAsync",
                            expectedOutput = "False",
                            weight = 2,
                            mandatory = "False",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }

                //Assert
                Assert.True(validDueDate);
            }
            catch (Exception ex)
            {
                var error = ex;
                String testResult = "BoundaryTestFor_BillDueDateAsync= False\n";
                fileUtility.WriteTestCaseResuItInText(testResult);
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_BillDueDateAsync",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Validate email id is in proper format or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BoundaryTestFor_ValidUserEmailAsync()
        {
            try
            {
             
                Regex regex = new Regex(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$");
                bool isEmail = regex.IsMatch(_user.Email);
                if (isEmail == true)
                {
                    //Action
                   var result = await _userService.RegisterNewUser(_user);

                    //Write test result in text file

                    String testResult = "BoundaryTestFor_ValidUserEmailAsync= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_ValidUserEmailAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                //Assert
                Assert.True(isEmail);
            }
            catch (Exception ex)
            {
                var error = ex;

                //Write test result in text file

                String testResult = "BoundaryTestFor_ValidUserEmailAsync= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file

                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_ValidUserEmailAsync",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Validate user name is in expected length or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BoundaryTestFor_ValidUserNameLengthAsync()
        {
            try
            {
                //Action
                var result = await _userService.RegisterNewUser(_user);

                var MinLength = 3;
                var MaxLength = 50;
                var actualLength = _user.UserName.Length;
                if (_user.UserName.Length < MaxLength && _user.UserName.Length > MinLength)
                {


                    //Write test result in text file

                    String testResult = "BoundaryTestFor_ValidUserNameLengthAsync= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file

                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_ValidUserNameLengthAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                    //Assert
                    Assert.InRange(_user.UserName.Length, MinLength, MaxLength);
                    Assert.InRange(actualLength, MinLength, MaxLength);
                }

            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "BoundaryTestFor_ValidUserNameLengthAsync= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_ValidUserNameLengthAsync",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        /// <summary>
        /// Validate user name is non-numeric or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BoundaryTestFor_ValidUserNameAsync()
        {
            try
            {
                //Action
                var result = await _userService.RegisterNewUser(_user);

                //Action
                //  await userRepo.RegisterNewUser(_user);
                //  var result = await userRepo.VerifyUser(_userLogin);
                bool getisUserName = Regex.IsMatch(_user.UserName, @"^[a-zA-Z0-9]{4,10}$", RegexOptions.IgnoreCase);
                if (getisUserName == true)
                {

                    //Write test result in text file

                    String testResult = "BoundaryTestFor_ValidUserNameAsync= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_ValidUserNameAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                //Assert
                Assert.True(getisUserName);
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "BoundaryTestFor_ValidUserNameAsync= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_ValidUserNameAsync",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }

        }


        /// <summary>
        /// Validate user password length is as expected 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BoundaryTestFor_ValidPasswordLengthAsync()
        {
            try
            {
                //Action
                var result = await _userService.RegisterNewUser(_user);

                var MinLength = 5;
                var MaxLength = 10;
                var actualLength = _user.Password.Length;

                if (_user.Password.Length <= 10 && _user.Password.Length >= 5)
                {

                    //Write test result in text file

                    String testResult = "BoundaryTestFor_ValidPasswordLengthAsync= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file


                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Boundary",
                            Name = "BoundaryTestFor_ValidPasswordLengthAsync",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }

                    //Assert
                    Assert.InRange(actualLength, MinLength, MaxLength);

                }
            }



            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "BoundaryTestFor_ValidPasswordLengthAsync= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_ValidPasswordLengthAsync",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }

        }

    }
}
