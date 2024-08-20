using System;
using UnityEngine;

namespace Hmxs_GMTK.Scripts.UI
{
    public class Rolling : MonoBehaviour
    {
        [SerializeField] private RectTransform startPoint;
        [SerializeField] private RectTransform endPoint;
        [SerializeField] private float speed;

        private RectTransform _rectTransform;

        private void OnEnable()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.position = startPoint.position;
        }

        private void Update()
        {
            _rectTransform.position = Vector3.Lerp(_rectTransform.position, endPoint.position, Time.deltaTime * speed);
            if (Vector3.Distance(_rectTransform.position, endPoint.position) < 0.5f)
            {
                _rectTransform.position = startPoint.position;
                ShutDown();
            }

            if (Input.GetMouseButton(0))
            {
                ShutDown();
            }
        }

        private void ShutDown()
        {
            gameObject.SetActive(false);
        }
    }
}