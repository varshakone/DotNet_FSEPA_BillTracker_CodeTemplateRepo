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
using BillTracker.Test.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace BillTracker.Test.Bill.TestCases
{
    [Collection("parallel")]
    public  class BusinessLogicTest
    {
        private User _user;
        IConfigurationRoot config;
        private BillDetails _bill;
        private MongoDBContext context;
        static FileUtility fileUtility;


        private readonly IBillService _billService;
        private readonly IUserService _userService;
        private readonly IBillRepository _billRepository;
        private readonly IUserRepository _userRepository;

        static BusinessLogicTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_business_revised.txt";
            fileUtility.CreateTextFile();
        }
     public BusinessLogicTest()
        {
            _bill = new BillDetails
            {
                Title = "Electricity Bill",
                Catagory = BillCategory.Office,
                Amount = 12000,
                DateOfEntry = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
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
            MongoDBUtility mongoDBUtility = new MongoDBUtility();
            context = mongoDBUtility.MongoDBContext;
            _userRepository = new UserRepository(context);
            _billRepository = new BillRepository(context);
            _userService = new UserService(_userRepository);
            _billService = new BillService(_billRepository);
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }


        /// <summary>
        /// generate new bill by submitting bill details and assert positive result
        /// </summary>
        [Fact]
        public async void BusinessTestFor_GenerateBill_Success()
        {
            try
            {
                  
                //Act 
               
                var billresult =await _billService.SaveBillAsync(_bill);

                if (billresult == true)
                {
                    

                    //Write test result in text file

                    String testResult = " BusinessTestFor_GenerateBill_Success=" + "True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                 
                }
                else
                {
                    // Assert
                    Assert.True(billresult);
                }


            }

            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "BusinessTestFor_GenerateBill_Success= False";
                fileUtility.WriteTestCaseResuItInText(testResult);
             }
        }


        /// <summary>
        /// retrieve list of bills and assert same
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_ListOfPaidAndUnpaidBills()
        {
            try
            {
                
                //Action
                var getbillList =await _billService.GetAllBillsAsync();

                if (getbillList != null && getbillList.Count > 0)
                {
                   
                    //Write test result in text file

                    String testResult = "BusinessTestFor_ListOfPaidAndUnpaidBills= True\n";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                                       
                }
                else
                {

                    //Assert
                    Assert.NotEmpty(getbillList);

                }
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "BusinessTestFor_ListOfPaidAndUnpaidBills= False\n";
                fileUtility.WriteTestCaseResuItInText(testResult);
               
            }
        }

        /// <summary>
        /// retrieve all bills generated filtered by due date and assert return result is list of bills
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_ListBillsByDueDate()
        {
            try
            {  
                //Action
                var getbillList =await _billService.GetAllBillsByDueDateAsync(DateTime.Now.AddDays(15));

                if (getbillList != null)
                {
                   //Write test result in text file
                   String testResult = "BusinessTestFor_ListBillsByDueDate= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                                       
                }
                else
                {

                    //Assert
                    Assert.NotEmpty(getbillList);

                }
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "BusinessTestFor_ListBillsByDueDate= false";
                fileUtility.WriteTestCaseResuItInText(testResult);
                               
            }
        }

        /// <summary>
        /// create new user by submitting user details and assert return result is as User Register Successfully
        /// </summary>

        [Fact]
        public async void BusinessTestFor_RegisterNewUser_Success()
        {
            try
            {

                //Act 

                var register =await _userService.RegisterNewUser(_user);
                if (register == "User Register Successfully")
                {

                    //Write test result in text file

                    String testResult = "BusinessTestFor_RegisterNewUser_Success= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                                       
                }

                // Assert
                Assert.Equal("User Register Successfully", register);

            }

            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "BusinessTestFor_RegisterNewUser_Success= False";
                fileUtility.WriteTestCaseResuItInText(testResult);
             

                
            }
        }
    
        /// <summary>
        /// validate user present in db and assert retrun result is as user details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_ValidUserFound()
        {
            try
            {
                          

                //Action
                var getUser =await _userService.VerifyUser(new UserLogin()
                {
                    UserName = "User",
                    Password = "password12",
                });
                if (getUser != null)
                {

                    //Write test result in text file

                    String testResult = "BusinessTestFor_ValidUserFound= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);
                                       
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

                String testResult = "BusinessTestFor_ValidUserFound= False";
                fileUtility.WriteTestCaseResuItInText(testResult);
                               
            }
        }
    }
}
