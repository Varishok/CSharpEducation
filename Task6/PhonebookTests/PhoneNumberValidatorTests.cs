using NUnit.Framework;
using Phonebook;
using System;
using System.Collections.Generic;

namespace TestPhonebook
{
  public class PhoneNumberValidatorTests
  {
    private PhoneNumber validPhoneNumber;
    private PhoneNumber invalidPhoneNumber;

    [SetUp]
    public void Setup()
    {
      validPhoneNumber = new PhoneNumber("+7(999)999-9999", PhoneNumberType.Personal);
      invalidPhoneNumber = new PhoneNumber("abc", PhoneNumberType.Personal);
    }

    [Test]
    public void PhoneNumberValidator_Validate_SuccessfullyValidate()
    {
      Assert.DoesNotThrow(() => PhoneNumberValidator.Validate(validPhoneNumber));
    }

    [Test]
    public void PhoneNumberValidator_Validate_ThrowsExcpetion()
    {
      Assert.Throws<ArgumentException>(() => PhoneNumberValidator.Validate(invalidPhoneNumber));
    }
  }
}