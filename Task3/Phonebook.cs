namespace Task3
{
  internal class Phonebook
  {
    /// <summary>
    /// Singleton field.
    /// </summary>
    private static Phonebook instance;

    /// <summary>
    /// List of abonents.
    /// </summary>
    private List<Abonent> abonents = new List<Abonent>();

    /// <summary>
    /// Get list of abonents.
    /// </summary>
    public List<Abonent> GetAbonents 
    {
      get { return abonents; }
    }

    /// <summary>
    /// Init.
    /// </summary>
    private Phonebook() { }

    /// <summary>
    /// Singleton static init.
    /// </summary>
    /// <returns>Singleton object.</returns>
    public static Phonebook GetInstance() 
    { 
      if (instance == null)
      {
        instance = new Phonebook();
      }
      return instance; 
    }

    /// <summary>
    /// Add abonent in list.
    /// </summary>
    /// <param name="name">Abonent's name.</param>
    /// <param name="phoneNumber">Abonent's phone number.</param>
    public bool AddAbonent(string name, string phoneNumber)
    {
      var ab = new Abonent(name, phoneNumber);
      abonents.Add(ab);
      return true;
    }

    public bool RemoveAbonent(string phoneNumber)
    {
      var findAbonent = abonents.Find(abonent => abonent.GetPhoneNumber.Equals(phoneNumber));
      if (findAbonent != null)
      {
        abonents.Remove(findAbonent);
        return true;
      }
      return false;
    }
  }
}
