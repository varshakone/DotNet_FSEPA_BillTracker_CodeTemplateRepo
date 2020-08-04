using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BillTracker.BusinessLayer.Service;
using BillTracker.DataLayer;
using BillTracker.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using Xunit;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using BillTracker.Test.Utility;

namespace BillTracker.Test.Bill.TestCases
{
    [Collection("parallel")]
    public  class FunctionalTest
    {

        /// <summary>
        /// declare reference variable of variety types
        /// </summary>
        private User _user;
        IConfigurationRoot config;
        private BillDetails _bill;
        private readonly TestServer _server;
        private readonly HttpClient _client;
        static FileUtility fileUtility;

        /// <summary>
        /// create text file to save test result
        /// </summary>
        static FunctionalTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_revised.txt";
            fileUtility.CreateTextFile();
        }

        /// <summary>
        /// Instantiate all objects used globally
        /// </summary>
        public FunctionalTest()
        {
            _bill = new BillDetails
            {
                Title = "Electricity Bill",
                Catagory = BillCategory.Office,
                Amount = 5000,
                DateOfEntry = DateTime.Now,
                DueDate = DateTime.Now.AddDays(15),
                PaymentMode = BillPaymentMode.Bank_Transfer,
                Status = BillStatus.Unpaid
            };
            _user = new User
            {
                UserName = "maza",
                Password = "maza123",
                ConfirmPassword = "maza123",
                Email = "maza@gmail.com",
                UserType = UserType.Visitor
            };
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            _server = new TestServer(new WebHostBuilder()
           .UseStartup<Startup>());
            _client = _server.CreateClient();
           
        }

        /// <summary>
        /// generate bill by submitting bill details and assert api returns message as true boolean value
        /// </summary>
        [Fact]
        public async void FunctionallTestFor_GenerateBill()
        {
            try
            {

                HttpContent content = new StringContent(JsonConvert.SerializeObject(_bill), Encoding.UTF8, "application/json");
               
                String billResponse = string.Empty;
                HttpResponseMessage response = await _client.PostAsync("https://localhost:9090/api/Bill/GenerateBill", content) ;
                var status = response.EnsureSuccessStatusCode();

                bool bills = false;
                
                if (status.IsSuccessStatusCode)
                {
                    billResponse = response.Content.ReadAsStringAsync().Result;
                   bills = JsonConvert.DeserializeObject<bool>(billResponse);
                }
                if (bills.Equals(true))
                {
                   
                    //Write test result in text file

                    String testResult = "FunctionallTestFor_GenerateBill_Success=True"  ;
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionallTestFor_GenerateBill_Success",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    Assert.True(bills);
                  
                }
            }

            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "FunctionallTestFor_GenerateBill_Success=" + "False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionallTestFor_GenerateBill_Success",
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
        /// assert api returns paid and unpaid bill list
        /// </summary>
        /// <returns></returns>
             [Fact]
        public async Task FunctionalTestFor_ListOfPaidAndUnpaidBills()
        {
            try
            {


                //Act 
              List<BillDetails> getbillList =null ;
                HttpResponseMessage response = await _client.GetAsync("https://localhost:9090/api/Bill/AllBills");
                var status = response.EnsureSuccessStatusCode();

                if (status.IsSuccessStatusCode)
                {
                    var bills =  response.Content.ReadAsStringAsync().Result;
                    getbillList =  JsonConvert.DeserializeObject<List<BillDetails>>(bills) ;
                }
                if (getbillList != null && getbillList.Count > 0)
                {
                
                    //Write test result in text file

                    String testResult = "FunctionalTestFor_ListOfPaidAndUnpaidBills= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_ListOfPaidAndUnpaidBills",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {

                    //Assert
                    Assert.Empty( getbillList);
                    

                }
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "FunctionalTestFor_ListOfPaidAndUnpaidBills= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_ListOfPaidAndUnpaidBills",
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
        /// 
        /// Assert whether receiving list of bill filtered by due date
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task FunctionalTestFor_ListBillsByDueDate()
        {
            try
            {

                DateTime duedate = DateTime.Now.AddDays(15);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(duedate), Encoding.UTF8, "application/json");
                //Act 
                List<BillDetails> getbillList = null;
                HttpResponseMessage response = await _client.GetAsync("https://localhost:9090/api/Bill/BillByDueDate?duedate="+_bill.DueDate.ToString());

                var status = response.EnsureSuccessStatusCode();

                if (status.IsSuccessStatusCode)
                {
                    var bills =response.Content.ReadAsStringAsync().Result;
                    getbillList = JsonConvert.DeserializeObject<List<BillDetails>>(bills) ;
                }

                if (getbillList != null && getbillList.Count >0)
                {
                    
                    //Write test result in text file

                    String testResult = "FunctionalTestFor_ListBillsByDueDate= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_ListBillsByDueDate",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
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

                String testResult = "FunctionalTestFor_ListBillsByDueDate= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_ListBillsByDueDate",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }

        [Fact]
        public async void FunctionalTestFor_RegisterNewUser()
        {
            try
            {
                // Arrange

                //Act 
                HttpContent content = new StringContent(JsonConvert.SerializeObject(_user), Encoding.UTF8, "application/json");
                //Act 
                String userResponse = string.Empty;
                HttpResponseMessage response = await _client.PostAsync("https://localhost:9090/api/User/NewUser", content);
                var status = response.EnsureSuccessStatusCode();

                if (status.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    userResponse = Response;

                }
                if (userResponse == "User Register Successfully")
                {

                    //Write test result in text file

                    String testResult = "functionTestFor_RegisterNewUser_Success= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_RegisterNewUser",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert
                    Assert.Equal("User Register Successfully", userResponse);
                }


            }

            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "functionTestFor_RegisterNewUser_Success= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_RegisterNewUser",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }
            }
        }
        [Fact]
        public async Task FunctionTestFor_ValidUserFound()
        {
            try
            {                //Action
                UserLogin userCredentials = new UserLogin
                {
                    UserName = "maza",
                    Password = "maza123"
                };
                //Act 
                HttpContent content = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8, "application/json");
                //Act 
                User userResponse = null;
                HttpResponseMessage response = await _client.PostAsync("https://localhost:9090/api/User/ValidateUser", content);
                var status = response.EnsureSuccessStatusCode();

                if (status.IsSuccessStatusCode)
                {
                    var Vresponse = response.Content.ReadAsStringAsync().Result;
                    userResponse = JsonConvert.DeserializeObject<User>(Vresponse);
                }

                if (userResponse != null)
                {

                    //Write test result in text file

                    String testResult = "functionTestFor_ValidUserFound= True";
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    //Write test result in xml file

                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "functionTestFor_ValidUserFound",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }

                }
                else
                {
                    //Assert
                    Assert.NotNull(userResponse);
                }
            }
            catch (Exception ex)
            {
                var error = ex;
                //Write test result in text file

                String testResult = "functionTestFor_ValidUserFound= False";
                fileUtility.WriteTestCaseResuItInText(testResult);

                //Write test result in xml file

                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "functionTestFor_ValidUserFound",
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
