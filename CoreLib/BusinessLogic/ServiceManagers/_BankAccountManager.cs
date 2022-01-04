using Library.Enums;
using Microsoft.EntityFrameworkCore;
using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Library;
using RitmaFlexPro.Message.Model;
using RitmaFlexPro.Message.Request;
using RitmaFlexPro.Message.Response;
using RitmaFlexPro.Message.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _BankAccountManager : BaseManager<ContactBankAccount, RequestBankAccount, RequestBankAccount, RequestBankAccount, RequestBaseSearch>
    {
        public _BankAccountManager()
        {

        }

        public override TaskResult Create(RequestBankAccount request)
        {

            return this.RunWithTransactionScope(new Task<TaskResult>(() =>
            {
                TaskResult r = new TaskResult();
                _AddressManager ad = new _AddressManager();
                r.ValidationResult.Result.Add(ad.Create(new RequestAddress { }).ValidationResult);
                //  t.ValidationResult;


                this.DataManager.Add(new ContactAddress { ContactId = -1, AddressName = "Demo" + DateTime.Now.EnglishDateTime() });
                return new TaskResult();

                // validator.Execute();

            }));
        }
        public override ValidationResult CreateValidation(RequestBankAccount request)
        {
            return base.CreateValidation(request);
        }


        public TaskResult GetAllBanks(BaseRequest request)
        {
            try
            {
                ResponseList<FeatureValue> data = DataManager.GetList<FeatureValue>(x => x.FeatureId == Features.Bank.ToInt64() && x.PrimaryId < 0, request.PageNo, request.PageSize);
                List<BankModel> bankAccounts = new List<BankModel>();
                foreach (FeatureValue item in data.List)
                {
                    BankModel curr = new BankModel();
                    List<FeatureValue> subFeaturesBankAccount = DataManager.GetList<FeatureValue>(o => o.ParentId == item.Id);

                    curr.Logo = subFeaturesBankAccount.FirstOrDefault(o => o.FeatureId == FeatureBank.Logo.ToInt64())?.Value;
                    curr.MersisNo = subFeaturesBankAccount.FirstOrDefault(o => o.FeatureId == FeatureBank.MersisNo.ToInt64())?.Value;
                    curr.Id = item.Id;
                    curr.Name = item.Value;
                    bankAccounts.Add(curr);
                }
                return TaskResult.Successful(new ResponseList<BankModel>(bankAccounts));
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
        public BankModel GetBankById(long bankId)
        {
            FeatureValue featureBank = DataManager.Get<FeatureValue>(x => x.Id == bankId && x.FeatureId == Features.Bank.ToInt64());
            if (featureBank != null)
            {
                List<FeatureValue> bankSubFeatures = DataManager.GetList<FeatureValue>(x => x.ParentId == featureBank.Id);

                BankModel bankModel = new BankModel();

                bankModel.Id = featureBank.Id;
                bankModel.Name = featureBank.Value;
                bankModel.Logo = bankSubFeatures.FirstOrDefault(x => x.FeatureId == FeatureBank.Logo.ToInt64())?.Value;

                return bankModel;
            }
            return null;
        }

        public TaskResult GetBankAccountListByContactId(BaseRequest request)
        {
            try
            {
                List<ContactBankAccount> bankAccounts = DataManager.GetList<ContactBankAccount>(x => x.ContactId == request.Id && x.IsDeleted == false);
                List<BankAccountModel> bankAccountListModel = new List<BankAccountModel>();
                foreach (var item in bankAccounts)
                {
                    BankAccountModel bankAccountModel = new BankAccountModel();
                    bankAccountModel.Id = item.Id;
                    bankAccountModel.BranchName = item.BranchName;
                    bankAccountModel.BranchCode = item.BranchCode;
                    bankAccountModel.Iban = item.Iban;
                    bankAccountModel.AccountNo = item.AccountNo;
                    //bankAccountModel.AccountType = item.AccountType;
                    bankAccountModel.ContactId = item.ContactId;
                    bankAccountModel.BankId = item.FeatureBankId;

                    BankModel bankModel = GetBankById(item.FeatureBankId);
                    bankAccountModel.BankName = bankModel.Name;
                    bankAccountModel.BankLogo = bankModel.Logo;

                    bankAccountListModel.Add(bankAccountModel);
                }

                return TaskResult.Successful(bankAccountListModel);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

        public TaskResult GetBankAccountById(BaseRequest request)
        {
            try
            {
                ContactBankAccount bankAccount = DataManager.Get<ContactBankAccount>(x => x.Id == request.Id);
                if (bankAccount != null)
                {
                    return TaskResult.Successful(bankAccount);
                }
                return TaskResult.NotFound();

            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
        public TaskResult CreateBankAccount(RequestBankAccount request)
        {
            try
            {
                ValidationResult validationResult = CreateContactValidate(request);
                if (validationResult.Success)
                {
                    DataManager.Add<ContactBankAccount>(request.BankAccount);
                    BankModel bankModel = GetBankById(request.BankAccount.FeatureBankId);
                    return TaskResult.Successful(bankModel);
                }
                else
                    return TaskResult.BadRequest(validationResult.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
        public ValidationResult CreateContactValidate(RequestBankAccount request)
        {
            ValidationResult validationResult = new ValidationResult();

            //if (request == null)
            //    return ValidationResult.BadRequest(DataManager.Get<FeatureValueTranslate>(o => o.FeatureId == ErrorEnums.RequestMustNotNull.ToInt64() && o.LangCode == request.LangCode).TranslatedText);

            //else if (request.BankAccount.FeatureBankId <= 0)
            //    return ValidationResult.BadRequest(DataManager.Get<FeatureValueTranslate>(o => o.FeatureId == ErrorEnums.NumberMustGreaterThanZero.ToInt64() && o.LangCode == request.LangCode).TranslatedText);
            //else
            //    return ValidationResult.Successful();

            return ValidationResult.Successful();
        }


        public TaskResult UpdateBankAccount(RequestBankAccount request)
        {
            try
            {
                ValidationResult validationResult = CreateContactValidate(request);
                if (validationResult.Success)
                {
                    BaseEntity result = DataManager.Update(request.BankAccount);
                    return TaskResult.Successful(result);
                }
                else
                    return TaskResult.BadRequest(validationResult.Message);
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }
        public TaskResult DeleteBankAccount(BaseRequest request)
        {
            try
            {
                ContactBankAccount bankAccount = DataManager.Get<ContactBankAccount>(x => x.Id == request.Id);
                if (bankAccount != null)
                {
                    ContactBankAccount result = DataManager.DeleteSoft<ContactBankAccount>(bankAccount);
                    return TaskResult.Successful(result);
                }
                return TaskResult.NotFound();
            }
            catch (Exception ex)
            {
                return ex.Throw();
            }
        }

    }
}
