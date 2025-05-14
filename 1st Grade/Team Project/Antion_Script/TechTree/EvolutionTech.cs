using Karin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tkfkadlsi
{
    public class EvolutionTech : MonoBehaviour
    {
        [SerializeField] private GameObject ChangePrefab;
        [SerializeField] private UnitSelectButton unitSelectButton;

        public void Activated()
        {
            if(ChangePrefab == null) 
            {
                Debug.Log("�ٲ� �������� �ȳ־��ݾ�!!!!");
                return;
            }
            if(unitSelectButton == null)
            {
                Debug.Log("�ٲ� ���� ���������ݾ�!!!!");
                return;
            }


            unitSelectButton.ChangeUnit(ChangePrefab);
        }
    }
}
