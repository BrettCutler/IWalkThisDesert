using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Record and check all facts that may be used by conversations.
/// TODO: Serialize this for save data, read in defaults for game start.
/// </summary>
public class GameStateFacts : Singleton<GameStateFacts>
{
  public Dictionary<string, int> m_FactsCollection = new Dictionary<string, int>( );

  /// <summary>
  /// Add a new fact if it doesn't exist, or overwrite the existing one.
  /// </summary>
  /// <param name="propertyName"></param>
  /// <param name="value"></param>
  public void Overwrite( string propertyName, int value )
  {
    if( !m_FactsCollection.ContainsKey( propertyName ) )
    {
      m_FactsCollection.Add( propertyName, value );
    }
    else
    {
      m_FactsCollection[propertyName] = value;
    }
  }

  /// <summary>
  /// Add a value to an existing fact.
  /// Errors if the fact doesn't yet exist.
  /// </summary>
  public void AddValue( string propertyName, int value )
  {
    if( !m_FactsCollection.ContainsKey( propertyName ) )
    {
      Debug.LogError( "Error in AddValue: no key (" + propertyName + ")" );
    }

    m_FactsCollection[propertyName] += value;
  }

  /// <summary>
  /// Return the value of a fact.
  /// Errors if the fact doesn't yet exist.
  /// </summary>
  /// <param name="propertyName"></param>
  /// <returns></returns>
  public int Lookup( string propertyName )
  {
    if( !m_FactsCollection.ContainsKey( propertyName ) )
    {
      Debug.LogError( "GameStateFacts does not contain key (" + propertyName + ")" );
      return 0;
    }
    else
    {
      return m_FactsCollection[propertyName];
    }
  }
}
