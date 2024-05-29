using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PassportLogin.AuthService;
using PassportLogin.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Input;
using universal_windows_platform_cs.Core.Services;
using universal_windows_platform_cs.Services;
using universal_windows_platform_cs.Views;

namespace universal_windows_platform_cs.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private UserAccount _account;
        private bool _isExistingAccount;
        public ICommand MainViewCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand ResetPassCommand { get; set; }
        private string _errorMessage = "";
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        public MainViewModel()
        {
            MainViewCommand = new RelayCommand(DebugCommand);
            LoginCommand = new RelayCommand<AccountUser>(LoginAsync);
            ResetPassCommand = new RelayCommand<AccountUser>(ResetPassAsync);
        }

        public async Task LoadDataAsync()
        {
            await DataSourceService.TestConnection();
        }

        private void DebugCommand()
        {
            Debug.WriteLine($"DebugCommand");
        }

        private async void LoginAsync(AccountUser account)
        {
            if (!string.IsNullOrEmpty(account.username) &&
                !string.IsNullOrEmpty(account.password) &&
                !AuthCoreService.LoginByUser(account.username, account.password))
            {
                ErrorMessage = "Wrong user or password";
                return;
            }
            else if ((!string.IsNullOrEmpty(account.username) && string.IsNullOrEmpty(account.password)) ||
                (!string.IsNullOrEmpty(account.username) &&
                !string.IsNullOrEmpty(account.password) &&
                AuthCoreService.LoginByUser(account.username, account.password)))
            {
                if (string.IsNullOrEmpty(account.password))
                {
                    account.password = "PIN";
                }

                AuthService.Instance.Register(account.username, account.password);
                List<UserAccount> accounts = AuthService.Instance.GetUserAccountsForDevice(AuthHelper.GetDeviceId());

                if (!accounts.Any())
                {
                    if (AuthService.Instance.ValidateCredentials(account.username, account.password))
                    {
                        Guid userId = AuthService.Instance.GetUserId(account.username);

                        if (userId != Guid.Empty)
                        {
                            //Now that the account exists on server try and create the necessary passport details and add them to the account
                            bool isSuccessful = await MicrosoftPassportHelper.CreatePassportKeyAsync(userId, account.username);
                            if (isSuccessful)
                            {
                                Debug.WriteLine("Successfully signed in with Windows Hello!");
                                //Navigate to the Welcome Screen. 
                                _account = AuthService.Instance.GetUserAccount(userId);
                                NavigationService.Navigate<CompanyListPage>();
                            }
                            else
                            {
                                //The passport account creation failed.
                                //Remove the account from the server as passport details were not configured
                                AuthService.Instance.PassportRemoveUser(userId);

                                ErrorMessage = "Account Creation Failed";
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage = "Invalid Credentials";
                    }
                }
                else
                {
                    NavigationService.Navigate<CompanyListPage>();
                }
            }
            else
            {
                ErrorMessage = "Please input user name!";
            }
        }

        public async void ResetPassAsync(AccountUser account)
        {
            if (!string.IsNullOrEmpty(account.username) &&
                AuthCoreService.GetUserByUserName(account.username))
            {
                AccountUser acc = AuthCoreService.GetInfo();

                if (acc == null || string.IsNullOrEmpty(acc.email))
                {
                    Console.WriteLine("Account information is invalid or email is missing.");
                }

                MailMessage message = new MailMessage
                {
                    From = new MailAddress("vudh@icd-vn.com", "ICD"),
                    Subject = "Forgot password",
                    Body = "Your password: 123456",
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true
                };
                message.To.Add(new MailAddress(acc.email));
                message.ReplyToList.Add(new MailAddress("vudh@icd-vn.com"));

                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("", ""),
                    EnableSsl = true
                };

                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid username or user not found.");
            }
        }
    }
}
