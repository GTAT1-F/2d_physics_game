using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The last frame of the impact effect animation stays on the screen even after the object bullet has been destroyed
// This is solve that problem
// Attached to Event after last frame of the impact effect animation
public class DestroyAnimation : MonoBehaviour
{
 private void DestroyGameObject()
 {
  Destroy(gameObject);
 }
}
