using System;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.Test
{
    public class MouseTest : MonoBehaviour
    {
        private void OnMouseEnter()
        {
            Debug.Log("Mouse Enter");
        }

        private void OnMouseExit()
        {
            Debug.Log("Mouse Exit");
        }

        private void OnMouseDown()
        {
            Debug.Log("Mouse Down");
        }

        private void OnMouseUp()
        {
            Debug.Log("Mouse Up");
        }
    }
}