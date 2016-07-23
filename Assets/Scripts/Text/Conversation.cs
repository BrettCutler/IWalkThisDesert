using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WyrmTale;

/// <summary>
/// A single conversation is
/// a branching dialogue, with
/// linked strings and marked 
/// choices
/// </summary>
public class Conversation
{
  public List<ConversationNode> m_Nodes;
  private int m_NextNodeSelection;

  /// <summary>
  /// Called by ConversationController
  /// to carry forward to next node.
  /// </summary>
  public void AdvanceConversation()
  {

  }

  /// <summary>
  /// Move the currently-selected 'next node'
  /// up or down
  /// </summary>
  /// <param name="counterIncrement"></param>
  public void ChangeSelection( int counterIncrement )
  {
    m_NextNodeSelection = Mathf.Clamp( m_NextNodeSelection + counterIncrement, 0, m_Nodes.Count );
  }

  /// <summary>
  /// Return a formatted string showing current node text
  /// </summary>
  public string PrintText()
  {
    return null;
  }

  public void Deserialize( JSON[] jsonLines )
  {
    for( int i = 0; i < jsonLines.Length; ++i )
    {
      ConversationNode node = new ConversationNode();
      node.Deserialize( jsonLines[i] );
    }
  }
}
