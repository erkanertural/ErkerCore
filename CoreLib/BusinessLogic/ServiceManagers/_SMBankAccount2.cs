using RitmaFlexPro.Library.Enums;
using RitmaFlexPro.BusinessLogic.Helpers;
using RitmaFlexPro.Entities;
using RitmaFlexPro.Library;
using RitmaFlexPro.Message.Model;
using RitmaFlexPro.Message.Request;

using RitmaFlexPro.Message.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RitmaFlexPro.Message.Response;

namespace RitmaFlexPro.BusinessLogic.Managers
{
    public class _SMBankAccount2 : SMFeatureValue<BaseModel>
    {
        public _SMBankAccount2()
        {

        }

        internal Response<List<ModelContactBank>> GetAllBankOfContact(BaseRequest baseRequest)
        {
            throw new NotImplementedException();
        }
        /*
public override ValidationResult CreateValidation(RequestBankAccount request)
{
   ValidationResult vlr = base.CheckAuthorizeAndAttributeValidation((BaseRequest)request);
   if (!vlr.Success)
       return vlr;
   if (request.BankAccount == null)
       return vlr.Invalid(FeatureValidationErrorEnum.ObjectCannotBeNull);
   if (request.BankAccount.Iban.IsNullOrEmpty())
       return vlr.Invalid(FeatureValidationErrorEnum.FieldCannotBeEmptyOrNull);
   return vlr.Successful();
}
public override Response<long> Create(RequestBankAccount request)
{
   try
   {
       ValidationResult validationResult = CreateValidation(request);
       if (validationResult.Success)
       {
           ContextDataManager.Add<ContactBankAccount>(request.BankAccount);
           ModelBank bankModel = GetBankById(request.BankAccount.FeatureBankId);
           return Response<long>.Successful(request.BankAccount.Id);
       }
       else
           return Response<long>.Fail(validationResult);
   }
   catch (Exception ex)
   {
       return ex.Throw<long>();
   }
}

public override ValidationResult EditValidation(RequestBankAccount request)
{
   return CreateValidation(request); // zaten aynı işleri yapıyor createvalidasyonla
}
public override Response<bool> Edit(RequestBankAccount request)
{
   try
   {
       ValidationResult validationResult = EditValidation(request);
       if (validationResult.Success)
       {
           ContextDataManager.Update(request.BankAccount);
           ModelBank bankModel = GetBankById(request.BankAccount.FeatureBankId);
           return Response<bool>.Successful(true);
       }
       else
           return Response<bool>.Fail(validationResult);
   }
   catch (Exception ex)
   {
       return ex.Throw<bool>();
   }
}

public override ValidationResult RemoveValidation(BaseRequestRemove request)
{
   ValidationResult vlr = CheckAuthorizeAndAttributeValidation(request);
   if (vlr.Success == false)
       return vlr;
   if (request.Id <= 0)
       return vlr.Invalid(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero);
   return vlr.Successful();
}
public override Response<bool> Remove(BaseRequestRemove request)
{
   try
   {
       ValidationResult valres = RemoveValidation(request);
       if (valres.Success)
       {
           ContactBankAccount bankAccount = ContextDataManager.Get<ContactBankAccount>(x => x.Id == request.Id);
           if (bankAccount != null)
           {
               ContactBankAccount result = ContextDataManager.DeleteSoft<ContactBankAccount>(bankAccount);
               return Response<bool>.Successful(true);
           }
           return Response<bool>.NotFound();
       }
       else
           return Response<bool>.Fail(valres);
   }
   catch (Exception ex)
   {
       return ex.Throw<bool>();
   }
}


public ValidationResult GetAllBanksValidation(BaseRequest request)
{
   ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
   if (vlr.Success == false)
       return vlr;

   return vlr.Successful();
}
public Response<List<ModelBank> > GetAllBanks(BaseRequest request)
{
   try
   {
       ValidationResult valres = GetAllBanksValidation(request);
       if (valres.Success)
       {
           List<FeatureValue> data = ContextDataManager.GetList<FeatureValue>(x => x.FeatureId == Features.Bank.ToInt64() && x.PrimaryId < 0);
           List<ModelBank> bankAccounts = new List<ModelBank>();
           foreach (FeatureValue item in data)
           {
               ModelBank curr = new ModelBank();
               List<FeatureValue> subFeaturesBankAccount = ContextDataManager.GetList<FeatureValue>(o => o.ParentId == item.Id);

               curr.Logo = subFeaturesBankAccount.FirstOrDefault(o => o.FeatureId == FeatureBank.Logo.ToInt64())?.Value;
               curr.MersisNo = subFeaturesBankAccount.FirstOrDefault(o => o.FeatureId == FeatureBank.MersisNo.ToInt64())?.Value;
               curr.Id = item.Id;
               curr.Name = item.Value;
               bankAccounts.Add(curr);
           }
           return Response<List<ModelBank>>.Successful(bankAccounts);
       }
       else
           return Response< List<ModelBank>>.Fail(valres);
   }
   catch (Exception ex)
   {
       return ex.Throw< List<ModelBank>>();
   }
}

public ValidationResult GetBankByIdValidation(BaseRequest request)
{
   ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
   if (vlr.Success == false)
       return vlr;
   if (request.Id <= 0)
       return vlr.Invalid(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero);

   return vlr.Successful();
}
public ModelBank GetBankById(long bankId)
{
   //todo: Burası Response olmalı mı?
   FeatureValue featureBank = ContextDataManager.Get<FeatureValue>(x => x.Id == bankId && x.FeatureId == Features.Bank.ToInt64());
   if (featureBank != null)
   {
       List<FeatureValue> bankSubFeatures = ContextDataManager.GetList<FeatureValue>(x => x.ParentId == featureBank.Id);

       ModelBank bankModel = new ModelBank();

       bankModel.Id = featureBank.Id;
       bankModel.Name = featureBank.Value;
       bankModel.Logo = bankSubFeatures.FirstOrDefault(x => x.FeatureId == FeatureBank.Logo.ToInt64())?.Value;

       return bankModel;
   }
   return null;
}

public override ValidationResult GetValidation(BaseRequest request)
{
   ValidationResult vlr = CheckAuthorizeAndAttributeValidation(request);
   if (vlr.Success == false)
       return vlr;
   if (request.Id <= 0)
       return vlr.Invalid(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero);

   return vlr.Successful();
}
public Response<List<ModelBankAccount>> GetBankAccountListByContactId(BaseRequest request)
{
   try
   {
       ValidationResult valres = GetValidation(request);
       if (valres.Success)
       {
           List<ContactBankAccount> bankAccounts = ContextDataManager.GetList<ContactBankAccount>(x => x.ContactId == request.Id);
           List<ModelBankAccount> bankAccountListModel = new List<ModelBankAccount>();
           foreach (var item in bankAccounts)
           {
               ModelBankAccount bankAccountModel = new ModelBankAccount();
               bankAccountModel.Id = item.Id;
               bankAccountModel.BranchName = item.BranchName;
               bankAccountModel.BranchCode = item.BranchCode;
               bankAccountModel.Iban = item.Iban;
               bankAccountModel.AccountNo = item.AccountNo;
               //bankAccountModel.AccountType = item.AccountType;
               bankAccountModel.ContactId = item.ContactId;
               bankAccountModel.BankId = item.FeatureBankId;

               ModelBank bankModel = GetBankById(item.FeatureBankId);
               bankAccountModel.BankName = bankModel.Name;
               bankAccountModel.BankLogo = bankModel.Logo;

               bankAccountListModel.Add(bankAccountModel);
           }

           return Response<List<ModelBankAccount>>.Successful(bankAccountListModel);

       }
       else
           return Response<List<ModelBankAccount>>.Fail(valres);
   }
   catch (Exception ex)
   {
       return ex.Throw<List<ModelBankAccount>>();
   }
}

public ValidationResult GetBankAccountByIdValidation(BaseRequest request)
{
   ValidationResult vlr = CheckAuthorizeAndAttributeValidation(request);
   if (vlr.Success == false)
       return vlr;
   if (request.Id <= 0)
       return vlr.Invalid(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero);

   return vlr.Successful();
}
public Response<ContactBankAccount> GetBankAccountById(BaseRequest request)
{
   try
   {
       ValidationResult valres = GetBankAccountByIdValidation(request);
       if (valres.Success)
       {
           ContactBankAccount bankAccount = ContextDataManager.Get<ContactBankAccount>(x => x.Id == request.Id);
           if (bankAccount != null)
           {
               return Response<ContactBankAccount>.Successful(bankAccount);
           }
           return Response<ContactBankAccount>.NotFound();
       }
       else
           return Response<ContactBankAccount>.Fail(valres);
   }
   catch (Exception ex)
   {
       return ex.Throw<ContactBankAccount>();
   }
}

public ValidationResult UpdateContactDefaultbankAccountByIdValidate(BaseRequest request)
{
   ValidationResult vlr = this.CheckAuthorizeAndAttributeValidation(request);
   if (vlr.Success == false)
       return vlr;
   if (request.Id < 1)
       return vlr.Invalid(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero);
   if (request.NumericValue < 1)
       return vlr.Invalid(FeatureValidationErrorEnum.IdFieldCannotBeLtEqZero);
   else
       return vlr.Successful();
}
public Response<bool> UpdateContactDefaultbankAccountById(BaseRequest request)
{
   try
   {
       ValidationResult valres = UpdateContactDefaultbankAccountByIdValidate(request);
       if (valres.Success == true)
       {
           Contact contactInfo = ContextDataManager.Get<Contact>(request.Id);
           contactInfo.Id = request.Id;
           contactInfo.DefaultBankAccountId = request.NumericValue;
           ContextDataManager.Update(contactInfo);
           return Response<bool>.Successful(true);
       }
       else
           return Response<bool>.Fail(valres);
   }
   catch (Exception ex)
   {
       return ex.Throw<bool>();
   }
} 

*/
    }
}
