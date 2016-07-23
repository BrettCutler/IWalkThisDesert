
using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the main text display.
/// Conversations print into this.
/// Controlls display of text over time,
/// or instant-printing on input.
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

  /// <summary>
  /// The current text to be displayed is gradually revealed.
  /// Lines are pushed up or faded out.
  /// An input code will instantly finish displaying the current text.
  /// </summary>
  public void UpdateText()
  {

  }

  /// <summary>
  /// Receive command from input or otherwise to instantly display
  /// the current text in the buffer.
  /// </summary>
  public void AdvanceText()
  {

  }
}

