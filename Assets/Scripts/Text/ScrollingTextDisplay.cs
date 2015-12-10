using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollingTextDisplay : MonoBehaviour
{
  Text text;

  void Awake()
  {
    text = GetComponent<Text>( );
  }

	void Update ()
  {
    text.text = ScrollingTextController.Instance.curDisplayText; 
	}
}
