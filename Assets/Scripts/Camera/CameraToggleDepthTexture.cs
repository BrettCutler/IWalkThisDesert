using UnityEngine;
using System.Collections;

/// <summary>
/// This script sets a camera's depth texture flag.
/// Make sure it's attached to a camera object.
/// </summary>
public class CameraToggleDepthTexture : MonoBehaviour
{
  public DepthTextureMode m_depthTextureMode;

  void Awake()
  {
    GetComponent<Camera>().depthTextureMode = m_depthTextureMode;
  }
}
