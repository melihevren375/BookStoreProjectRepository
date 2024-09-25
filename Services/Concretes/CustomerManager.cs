using AutoMapper;
using Entities.Concretes;
using Entities.DataTransferObjects.CustomerDataTransferObjects;
using Entities.DataTransferObjects.EmailCodeDataTransferObjects;
using Entities.Exceptions.NotFoundExceptions;
using Entities.RequestFeatures;
using Repositories.Contracts;
using Services.Contracts;
using System.Dynamic;
using System.Net;
using System.Net.Mail;

namespace Services.Concretes
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmailCodeRepository _emailCodeRepository;
        private readonly IMapper _mapper;
        private readonly IDataShaper<CustomerDtoForRead> _dataShaper;
        public CustomerManager(ICustomerRepository customerRepository, IMapper mapper, IDataShaper<CustomerDtoForRead> dataShaper, IEmailCodeRepository emailCodeRepository)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _emailCodeRepository = emailCodeRepository;
        }

        public async Task ChangedPasswordAsync(CustomerDtoForChangePassword customerDtoForChangePassword, bool trackChanges)
        {
            var customer = await _customerRepository.GetCustomerByConditionAsync(trackChanges, c => c.Email.Equals(customerDtoForChangePassword.Email));

            var emailCodeControl = await _emailCodeRepository.ControlEmailCodeByCustomerAndCodeAsync(customer.Id, customerDtoForChangePassword.Code);

            if (emailCodeControl)
            {
                customer.Password = customerDtoForChangePassword.Password;

                _customerRepository.UpdateCustomer(customer);
            }
            else
            {
                throw new Exception("Code ve customer uyuşmuyor!");
            }
        }


        public async Task CreateCustomerAsync(CustomerDtoForInsertion customerDtoForInsertion)
        {
            var customer = _mapper.Map<Customer>(customerDtoForInsertion);
            _customerRepository.CreateCustomer(customer);
        }

        public async Task DeleteCustomerAsync(Guid id, bool trackChanges)
        {
            var customer = await CheckAndReturnCustomer(id, trackChanges);
            _customerRepository.DeleteCustomer(customer);
        }

        public async Task<bool> EmailControlAsync(string email)
        {
            var result = _customerRepository.
                EmailControlAsync(email).
                Result;

            int code = 0;

            if (result is true)
            {
                var customer = await _customerRepository
                .GetCustomerByConditionAsync(true, c => c.Email.Equals(email));

                var customerId = customer.Id; 


                code = await RandomEmailNumberAsync(customerId);
                SendEmail(email, code);
            }

            return result;
        }

        public async Task<(IEnumerable<ExpandoObject> customerDtosForRead, MetaData metaData)> GetAllCustomersAsync(CustomerParams customerParams)
        {
            var pagedListResults = await _customerRepository.GetAllCustomerAsync(customerParams);
            var customerDtosForRead = _mapper.Map<IEnumerable<CustomerDtoForRead>>(pagedListResults);
            var dataShape = _dataShaper.ShapeData(customerDtosForRead, customerParams.Fields);
            return (customerDtosForRead: dataShape, metaData: pagedListResults.MetaData);
        }

        public async Task<bool> SignInControlAsync(CustomerDtoForSignInControl customerDtoForSign)
        {
            var customer = _mapper.Map<Customer>(customerDtoForSign);

            var result = _customerRepository.
                SignInControl(customer);

            return result;
        }

        public async Task UpdateCustomerAsync(Guid id, bool trackChanges, CustomerDtoForUpdate customerDtoForUpdate)
        {
            var customer = await CheckAndReturnCustomer(id, trackChanges);
            _mapper.Map(customerDtoForUpdate, customer);
            _customerRepository.UpdateCustomer(customer);
        }

        private async Task<Customer> CheckAndReturnCustomer(Guid id, bool trackChanges)
        {
            var customer = await _customerRepository.GetCustomerByConditionAsync(trackChanges, c => c.Id.Equals(id));

            if (customer is null)
                throw new CustomerNotFoundException(id);

            return customer;
        }

        private async Task<int> RandomEmailNumberAsync(Guid customerId)
        {
            Random random = new Random();
            int fourDigitNumber;
            bool emailCodeControlResult;
            bool customerControlResult;

            do
            {
                fourDigitNumber = random.Next(1000, 10000);

                emailCodeControlResult = await _emailCodeRepository.ControlEmailCodeByCodeAsync(fourDigitNumber);
            }
            while (emailCodeControlResult);

            customerControlResult = await _emailCodeRepository.ControlEmailCodeByCustomerAsync(customerId);

            if (customerControlResult is false)
            {
                _emailCodeRepository.CreateEmailCode(new EmailCode()
                {
                    CustomerId = customerId,
                    Code = fourDigitNumber
                });
            }
            else
            {
                var emailCodeDtoForUpdate = new EmailCodeDtoForUpdate()
                {
                    Code = fourDigitNumber,
                    CustomerId = customerId,
                };

                var emailCode = await _emailCodeRepository.GetEmailCodeByConditionAsync(ec => ec.CustomerId.Equals(customerId), true);

                emailCode.Code = emailCodeDtoForUpdate.Code;

                _emailCodeRepository.UpdateEmailCode(emailCode);
            }

            return fourDigitNumber;
        }


        private void SendEmail(string email, int code)
        {

            try
            {
                string fromAddress = "evrenmelih_44@hotmail.com";
                string toAddress = email;

                string subject = "Password Changed";
                string body = $"Your code is {code}";

                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("evrenmelih_44@hotmail.com", "Garrix2016"),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, body);

                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderme başarısız: " + ex.Message);
            }
        }
    }

}

