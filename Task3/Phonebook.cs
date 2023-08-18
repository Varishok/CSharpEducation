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
      this.abonents.Add(ab);
      return true;
    }

    /// <summary>
    /// Delete a abonent by phone number.
    /// </summary>
    /// <param name="phoneNumber">Abonent's phone number.</param>
    /// <returns>true - abonent deleted successfully. false - abonent can't deleted.</returns>
    public bool RemoveAbonent(string phoneNumber)
    {
      var findAbonent = FindAbonentByPhone(phoneNumber);
      if (findAbonent != null)
      {
        this.abonents.Remove(findAbonent);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Find a abonent by phone number.
    /// </summary>
    /// <param name="phoneNumber">Abonent's phone number.</param>
    /// <returns>Abonent if it was found, or null otherwise.</returns>
    public Abonent? FindAbonentByPhone(string phoneNumber)
    {
      var findAbonent = this.abonents.Find(abonent => abonent.GetPhoneNumber.Equals(phoneNumber));
      return findAbonent;
    }

    /// <summary>
    /// Find a abonents by name.
    /// </summary>
    /// <param name="name">Abonent's name.</param>
    /// <returns>A list of all abonents with the specified name.</returns>
    public List<Abonent> FindAbonentsByName(string name)
    {
      var abonents = this.abonents.FindAll(abonents => abonents.GetName.Equals(name));
      return abonents;
    }
  }
}
