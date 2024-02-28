using Castle.Components.DictionaryAdapter.Xml;
using RsystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsystemsAssignment.Tests
{
    public static class MockData
    {
        public static AccountApiResponse GetAccounts()
        {
            AccountApiResponse apiResponse = new AccountApiResponse()
            {
                Accounts = new List<AccountDTO>()
            {
                    new AccountDTO()
                    {
                        AccountID=1,
                        AccountName="Google",
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    },
                    new AccountDTO()
                    {
                         AccountID=2,
                        AccountName="Yahoo",
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    },
                    new AccountDTO()
                    {
                         AccountID=3,
                        AccountName="Rediff",
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    }
                },
                TotalCount = 3
            };
            return apiResponse;
        }

        public static AccountDTO GetAccount()
        {
            AccountDTO apiResponse = new AccountDTO()
            {

                AccountID = 1,
                AccountName = "Google",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddDays(1)
            };
            return apiResponse;
        }

        public static AppointmentApiResponse GetAppointments()
        {
            AppointmentApiResponse apiResponse = new AppointmentApiResponse()
            {
                Appointments = new List<AppointmentDTO>()
            {
                    new AppointmentDTO()
                    {
                        AppointmentID=1,
                        AppointmentStartTime=DateTime.UtcNow,
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    },
                    new AppointmentDTO()
                    {
                         AppointmentID=2,
                        AppointmentStartTime=DateTime.UtcNow,
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    },
                    new AppointmentDTO()
                    {
                        AppointmentID=3,
                        AppointmentStartTime=DateTime.UtcNow,
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    }
                },
                TotalCount = 3
            };
            return apiResponse;
        }

        public static AppointmentDTO GetAppointment()
        {
            AppointmentDTO apiResponse = new AppointmentDTO()
            {

                AppointmentID = 1,
                AppointmentStartTime = DateTime.Now,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddDays(1)
            };
            return apiResponse;
        }

        public static AccountDTO AddAccount()
        {
            AccountDTO apiResponse = new AccountDTO()
            {

                AccountID = 1,
                AccountName = "Google",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddDays(1)
            };
            return apiResponse;
        }

        public static ClientApiResponse GetClients()
        {
            ClientApiResponse apiResponse = new ClientApiResponse()
            {
                Clients = new List<ClientDTO>()
            {
                    new ClientDTO()
                    {
                        AccountID=1,
                        ClientID=1,
                        ClientName="Google",
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    },
                    new ClientDTO()
                    {
                         AccountID=2,
                       ClientID=2,
                        ClientName="Google",
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    },
                    new ClientDTO()
                    {
                         AccountID=3,
                        ClientID=3,
                        ClientName="Google",
                        CreatedDate=DateTime.Now,
                        ModifiedDate=DateTime.Now.AddDays(1)
                    }
                },
                TotalCount = 3
            };
            return apiResponse;
        }

        public static ClientDTO GetClient()
        {
            ClientDTO apiResponse = new ClientDTO()
            {

                AccountID = 1,
                ClientID = 3,
                ClientName = "Google",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now.AddDays(1)
            };
            return apiResponse;
        }
    }
}
