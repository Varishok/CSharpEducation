using NUnit.Framework;
using Phonebook;
using System;
using System.Collections.Generic;

namespace TestPhonebook
{
  public class PhoneNumberValidatorTests
  {
    private PhoneNumber validPhoneNumber;
    private PhoneNumber validPhoneNumber2;
    private PhoneNumber invalidPhoneNumber;

    [SetUp]
    public void Setup()
    {
      validPhoneNumber = new PhoneNumber("+7(999)999-9999", PhoneNumberType.Personal);
      validPhoneNumber2 = new PhoneNumber("+7(888)888-8888", PhoneNumberType.Work);
      invalidPhoneNumber = new PhoneNumber("abc", PhoneNumberType.Personal);
    }

    [Test]
    public void PhoneNumberValidator_Validate_SuccessfullyValidate()
    {
      Assert.DoesNotThrow(() => PhoneNumberValidator.Validate(validPhoneNumber));
    }

    [Test]
    public void PhoneNumberValidator_ValidateList_SuccessfullyValidate()
    {
      List<PhoneNumber> listValidNumber = new List<PhoneNumber>()
      {
        validPhoneNumber,
        validPhoneNumber2,
      };
      Assert.DoesNotThrow(() => PhoneNumberValidator.ValidateList(listValidNumber));
    }

    [Test]
    public void PhoneNumberValidator_Validate_ThrowsExcpetion()
    {
      Assert.Throws<ArgumentException>(() => PhoneNumberValidator.Validate(invalidPhoneNumber));
    }

    [Test]
    public void PhoneNumberValidator_ValidateList_ThrowsExcpetion()
    {
      List<PhoneNumber> listWithInvalidNumber = new List<PhoneNumber>()
      {
        validPhoneNumber,
        validPhoneNumber2,
        invalidPhoneNumber
      };
      Assert.Throws<ArgumentException>(() => PhoneNumberValidator.ValidateList(listWithInvalidNumber));
    }
  }
}