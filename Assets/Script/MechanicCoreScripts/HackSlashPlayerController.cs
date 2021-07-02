using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalZero.Core
{
    public class HackSlashPlayerController : MonoBehaviour
    {
        static int click = 0;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) click++;
            if(click > 0)
            {
                //Call attack anim
            }
        }
    }
}