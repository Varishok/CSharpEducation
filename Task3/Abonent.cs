using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
  internal class Abonent
  {
    /// <summary>
    /// The abonent's name.
    /// </summary>
    private string name;

    /// <summary>
    /// The abonent's phone number.
    /// </summary>
    private string phoneNumber;

    /// <summary>
    /// Init.
    /// </summary>
    /// <param name="name">The abonent's name.</param>
    /// <param name="phoneNumber">The abonent's phone number.</param>
    public Abonent(string name, string phoneNumber)
    {
      this.name = name;
      this.phoneNumber = phoneNumber;
    }

    /// <summary>
    /// Equal's method.
    /// </summary>
    /// <param name="obj">The specified object.</param>
    /// <returns>true if the specified object phone number is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
      if ((obj == null) || !this.GetType().Equals(obj.GetType()))
      {
        return false;
      }
      else
      {
        Abonent other = (Abonent)obj;
        return this.phoneNumber.Equals(other.phoneNumber);
      }
    }

    /// <summary>
    /// Hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
      return this.phoneNumber.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
      return String.Format("{0} - {1}", this.phoneNumber, this.name);
    }
  }
}
