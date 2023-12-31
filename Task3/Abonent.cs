﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
  /// <summary>
  /// Abonent with his/her data.
  /// </summary>
  internal class Abonent
  {

    #region Поля и свойства

    /// <summary>
    /// The abonent's name.
    /// </summary>
    private string name;

    /// <summary>
    /// The abonent's phone number.
    /// </summary>
    private string phoneNumber;

    /// <summary>
    /// Get the abonent's name.
    /// </summary>
    public string Name 
    { 
      get { return name; } 
    }

    /// <summary>
    /// Get the abonent's phone number.
    /// </summary>
    public string PhoneNumber 
    { 
      get { return phoneNumber; } 
    }

    #endregion

    #region Конструкторы

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

    #endregion

    #region Базовый класс

    /// <summary>
    /// Equal's method.
    /// </summary>
    /// <param name="obj">The specified object.</param>
    /// <returns>true if the specified object phone number is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
      if (obj is Abonent)
      {
        Abonent other = (Abonent)obj;
        return this.phoneNumber.Equals(other.phoneNumber);
      }
      else 
      {  
        return false; 
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

    #endregion
  }
}
