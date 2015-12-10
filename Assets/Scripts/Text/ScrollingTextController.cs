
using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the main text display.
/// Accepts input to advance and select choices.
/// Conversations are fed into this.
/// </summary>
public class ScrollingTextController : Singleton<ScrollingTextController>
{
  public string curDisplayText
  {
    get;
    private set;
  }

  public void QueueText( string textToQueue )
  {
    curDisplayText = textToQueue;
  }

  public void QueueText( Conversation convoToLoad )
  {

  }
}

/// <summary>
/// A single conversation is
/// a branching dialogue, with
/// linked strings and marked 
/// choices
/// </summary>
public class Conversation
{
  public ConversationElement[] lines;
}

/// <summary>
/// Single element of a conversation.
/// This can be derived to prompt a user choice,
/// to branch, or just display a string
/// </summary>
public interface ConversationElement
{
  //public 
}
