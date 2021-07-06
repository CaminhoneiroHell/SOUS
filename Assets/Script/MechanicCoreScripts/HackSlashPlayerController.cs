using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalZero.Core
{
    public class HackSlashPlayerController : MonoBehaviour
    {
        HostileBehaviour aggro;

        [SerializeField] bool attackFlag;


        private void Start()
        {
            aggro = GetComponent<HostileBehaviour>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !attackFlag)
            {
                aggro.TalhoTrigger(); 
                attackFlag = true;
            }


            //if (timeClickInputOffset < timeSinceLastClickInput)
            //{
            //    if (Input.GetMouseButtonDown(0))
            //        click++;
            //}


            //switch (click)
            //{
            //    //case 0:
            //    //    break;
            //    //case 1:
            //    //    //transform.LookAt(target.transform);
            //    //    aggro.TalhoTrigger();
            //    //    timeSinceLastAttack = 0;
            //    //    click = 0;
            //    //    break;
            //    //case 2:
            //    //    aggro.FendenteTrigger();
            //    //    timeSinceLastAttack = 0;
            //    //    click = 0;
            //    //    break;
            //    //case 3:
            //    //    aggro.CPernaTrigger();
            //    //    timeSinceLastAttack = 0;
            //    //    click = 0;
            //    //    break;
            //    //case 4:
            //    //    aggro.RevCPernaTrigger();
            //    //    timeSinceLastAttack = 0;
            //    //    click = 0;
            //    //    break;

            //}




        }
    }
}