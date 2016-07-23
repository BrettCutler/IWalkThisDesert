using UnityEngine;
using System.Collections;
using WyrmTale;

public class ConversationController : Singleton<ConversationController>
{
  public override void Init()
  {
    base.Init();

    LoadConversations();
  }

  private void LoadConversations()
  {
    TextAsset conversationData = Resources.Load<TextAsset>("Conversations");
    if( conversationData.text.Length > 0 )
    {
      JSON json = new JSON();
      json.serialized = conversationData.text;

      JSON[] jsonSheets = json.ToArray<JSON>("sheets");
      for( int i = 0; i < jsonSheets.Length; ++i )
      {
        Conversation convo = new Conversation();
        convo.Deserialize( jsonSheets[i].ToArray<JSON>( "lines" ) );
      }
    }
  }

  public void QueueConversation( Conversation convoToLoad )
  {

  }

}
