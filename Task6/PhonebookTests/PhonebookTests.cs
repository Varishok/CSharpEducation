using NUnit.Framework;
using Phonebook;
using System;
using System.Collections.Generic;

namespace TestPhonebook
{
  public class PhonebookTests
  {
    private Phonebook.Phonebook phonebook;
    private List<PhoneNumber> phoneNumbers;

    [SetUp]
    public void Setup()
    {
      phonebook = new Phonebook.Phonebook();
      phoneNumbers = new List<PhoneNumber>()
        { new PhoneNumber("+7(999)999-9999", PhoneNumberType.Personal) };
    }

    [Test]
    public void Phonebook_AddSubscriber_SuccessfullyAdded()
    {
      Subscriber expectedSub = new Subscriber("Egor", phoneNumbers);
      phonebook.AddSubscriber(expectedSub);

      Subscriber actualSub = phonebook.GetSubscriber(expectedSub.Id);
      Assert.AreEqual(expectedSub, actualSub);
    }

    [Test]
    public void Phonebook_AddNumberToSubscriber_SuccessfullyAdded()
    {
      PhoneNumber phoneNumber = new PhoneNumber("+7(888)888-8888", PhoneNumberType.Personal);

      Subscriber expectedSub = new Subscriber("Egor", phoneNumbers);
      phonebook.AddSubscriber(expectedSub);
      phonebook.AddNumberToSubscriber(expectedSub, phoneNumber);

      Subscriber actualSub = phonebook.GetSubscriber(expectedSub.Id);
      Assert.AreEqual(actualSub.PhoneNumbers[1], phoneNumber);
    }

    [Test]
    public void Phonebook_RenameSubscriber_SuccessfullyRename()
    {
      Subscriber expectedSub = new Subscriber("Egor", phoneNumbers);
      phonebook.AddSubscriber(expectedSub);

      string newName = "Pavel";
      phonebook.RenameSubscriber(expectedSub, newName);

      Subscriber actualSub = phonebook.GetSubscriber(expectedSub.Id);
      Assert.AreEqual(actualSub.Name, newName);
    }

    [Test]
    public void Phonebook_UpdateSubscriber_SuccessfullyUpdate()
    {
      Subscriber expectedSub = new Subscriber("Egor", phoneNumbers);
      Subscriber actualSub = new Subscriber("Pavel", phoneNumbers);
      phonebook.AddSubscriber(expectedSub);
      phonebook.UpdateSubscriber(expectedSub, actualSub);

      Assert.Throws<InvalidOperationException>(
        () => phonebook.GetSubscriber(expectedSub.Id));
      Assert.DoesNotThrow(() => phonebook.GetSubscriber(actualSub.Id));
    }

    [Test]
    public void Phonebook_DeleteSubscriber_SuccessfullyDelete()
    {
      Subscriber expectedSub = new Subscriber("Egor", phoneNumbers);
      phonebook.AddSubscriber(expectedSub);
      phonebook.DeleteSubscriber(expectedSub);

      Assert.Throws<InvalidOperationException>(
        () => phonebook.GetSubscriber(expectedSub.Id));
    }
  }
}