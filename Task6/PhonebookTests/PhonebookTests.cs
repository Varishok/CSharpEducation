using NUnit.Framework;
using Phonebook;
using System.Collections.Generic;

namespace TestPhonebook
{
  public class PhonebookTests
  {
    private Phonebook.Phonebook phonebook;

    [SetUp]
    public void Setup()
    {
      phonebook = new Phonebook.Phonebook();
    }

    [Test]
    public void Phonebook_AddSubscriber_SuccessfullyAdded()
    {
      List<PhoneNumber> phoneNumbers = new List<PhoneNumber>() 
        { new PhoneNumber("+7(999)999-99", PhoneNumberType.Personal) };

      Subscriber expectedSub = new Subscriber("Egor", phoneNumbers);
      phonebook.AddSubscriber(expectedSub);

      Subscriber actualSub = phonebook.GetSubscriber(expectedSub.Id);
      Assert.AreEqual(expectedSub, actualSub);
    }

    [Test]
    public void Phonebook_AddNumberToSubscriber_SuccessfullyAdded()
    {
      List<PhoneNumber> phoneNumbers = new List<PhoneNumber>()
        { new PhoneNumber("+7(999)999-99", PhoneNumberType.Personal) };
      PhoneNumber phoneNumber = new PhoneNumber("1234567890", PhoneNumberType.Personal);

      Subscriber expectedSub = new Subscriber("Egor", phoneNumbers);
      phonebook.AddSubscriber(expectedSub);
      phonebook.AddNumberToSubscriber(expectedSub, phoneNumber);

      Subscriber actualSub = phonebook.GetSubscriber(expectedSub.Id);
      Assert.AreEqual(actualSub.PhoneNumbers[1], phoneNumber);
    }
  }
}