using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using System;
using WyrmTale;

/// <summary>
/// Single element of a conversation.
/// This can be derived to prompt a user choice,
/// to branch, or just display a string
/// </summary>
public class ConversationNode
{
  private string m_Name;
  private string m_Content;
  private List<ConversationCondition> m_Conditions = new List<ConversationCondition>();
  private List<ConversationNode> m_OutputEdges = new List<ConversationNode>();
  private bool m_EntryPoint;
  private bool m_IsPlayerQuery;

  private List<string> m_OutputEdgeNames = new List<string>();

  public bool MeetsConditions()
  {
    for( int i = 0; i < m_Conditions.Count; ++i )
    {
      if( !m_Conditions[i].Evaluate() )
      {
        return false;
      }
    }
    return true;
  }

  internal void Deserialize( JSON json )
  {
    m_Name = json.ToString( "name" );
    m_Content = json.ToString( "content" );

    JSON[] jsonConditions = json.ToArray<JSON>("conditions");
    for( int i = 0; i < jsonConditions.Length; ++i )
    {
      ConversationCondition condition = new ConversationCondition();
      condition.Deserialize( jsonConditions[i] );
      m_Conditions.Add( condition );
    }

    JSON[] outputEdges = json.ToArray<JSON>( "outputEdges" );
    for( int i = 0; i < outputEdges.Length; ++i )
    {
      m_OutputEdgeNames.Add( outputEdges[i].ToString( "edge" ) );
    }
    // TODO: hook up to other output edges

    m_EntryPoint = json.ToBoolean( "entryPoint" );
    m_IsPlayerQuery = json.ToBoolean( "isPlayerQuery" );

    Debug.Log( "Finish deserializing: Conversation" );
    Debug_LogVariables();
  }

  /// <summary>
  /// Return the number of conditions, for ranking most-specific edge.
  /// </summary>
  public int NumberOfConditions( )
  {
    return m_Conditions.Count;
  }
  
  public List<ConversationNode> GetNext()
  {
    //List<ConversationNode> nextNodes = new List<ConversationNode>();

    //for( int i = 0; i < m_OutputEdges.Count; ++i )
    //{

    //}
    return null;
  }

  private void Debug_LogVariables()
  {
    Debug.Log( "Log variables:\n" +
      "m_Name = " + m_Name + "\n" +
      "m_Content = " + m_Content + "\n" +
      "m_Conditions.Length = " + m_Conditions.Count + "\n"
      );

    for( int i = 0; i < m_Conditions.Count; ++i )
    {
      Debug.Log( "m_Conditions[" + i + "] = " + m_Conditions[i] + "\n" );
    }

    Debug.Log( "m_OutputEdges.Count = " + m_OutputEdges.Count + "\n" );

    for( int i = 0; i <  m_OutputEdges.Count; ++i )
    {
      Debug.Log( "m_OutputEdges[" + i + "] = " + m_OutputEdges[i] + "\n" );
    }

    Debug.Log( "m_EntryPoint = " + m_EntryPoint + "\n" +
               "m_IsPlayerQuery = " + m_IsPlayerQuery + "\n"
             );
  }
}
