using System.Collections;
using System.Text;

namespace Task3
{
  internal class Phonebook : IEnumerable
  {

    #region Константы

    /// <summary>
    /// File Path.
    /// </summary>
    private const string path = "./phonebook.txt";

    #endregion

    #region Поля и свойства

    /// <summary>
    /// Singleton field.
    /// </summary>
    private static Phonebook instance;

    /// <summary>
    /// List of abonents.
    /// </summary>
    private List<Abonent> abonents = new List<Abonent>();

    /// <summary>
    /// Indexer declaration.
    /// </summary>
    /// <param name="i">Item's index.</param>
    /// <returns>Item by a given index.</returns>
    public Abonent this[int i]
    {
      get => this.abonents[i];
      set 
      {
        this.abonents[i] = value;
        SaveAll();
      }
    }

    #endregion

    #region Конструкторы

    /// <summary>
    /// Init.
    /// </summary>
    private Phonebook()
    {
      Read();
    }

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

    #endregion

    #region Методы взаимодействия с контактами

    /// <summary>
    /// Add abonent in list.
    /// </summary>
    /// <param name="name">Abonent's name.</param>
    /// <param name="phoneNumber">Abonent's phone number.</param>
    public bool AddAbonent(string name, string phoneNumber)
    {
      var findAbonent = FindAbonentByPhone(phoneNumber);
      if (findAbonent == null)
      {
        var ab = new Abonent(name, phoneNumber);
        this.abonents.Add(ab);
        Save(ab);
        return true;
      }
      return false;
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
        SaveAll();
        return true;
      }
      return false;
    }

    #endregion

    #region Методы поиска

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
      var findAbonents = this.abonents.FindAll(abonent => abonent.GetName.Equals(name));
      return findAbonents;
    }

    #endregion

    #region IEnumarable

    /// <summary>
    /// Implementation of IEnumerable.
    /// </summary>
    /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
    public IEnumerator<Abonent> GetEnumerator()
    {
      return this.abonents.GetEnumerator();
    }

    /// <summary>
    /// Implementation of IEnumerable.
    /// </summary>
    /// <returns>An enumerator that iterates through a collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    #endregion

    #region Методы взаимодействия с файловой системой

    /// <summary>
    /// Save abonent to a file.
    /// </summary>
    /// <param name="abonent">Saved abonent.</param>
    /// <exception cref="Exception">Errors when writing to a file.</exception>
    private void Save(Abonent abonent)
    {
      string text = abonent.ToString() + "\n";
      try
      {
        File.AppendAllText(path, text, Encoding.Unicode); // Create or write in file
      }
      catch 
      {
        throw new Exception("Can't write in file.");
      }
    }

    /// <summary>
    /// Save all abonents to a file.
    /// </summary>
    private void SaveAll()
    {
      File.Delete(path);
      foreach (var abonent in this.abonents)
      {
        Save(abonent);
      }
    }

    /// <summary>
    /// Reading abonents from a file.
    /// </summary>
    private void Read()
    {
      if(File.Exists(path))
      {
        string[] lines = File.ReadAllLines(path);
        lines = lines.SkipLast(1).ToArray(); // The last line is blank.

        foreach (string line in lines)
        {
          string[] split = line.Split(" - ");
          var abonent = new Abonent(split[1], split[0]); // Abonent's in file is "phone - name"
          this.abonents.Add(abonent);
        }
      }
    }

    #endregion
  }
}
