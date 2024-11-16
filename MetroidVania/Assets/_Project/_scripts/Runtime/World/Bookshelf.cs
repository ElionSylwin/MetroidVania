using UnityEngine;
using Vania.Runtime.Interfaces;

namespace Vania.Runtime.World
{
    public class Bookshelf : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("This is a bookshelf");
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
