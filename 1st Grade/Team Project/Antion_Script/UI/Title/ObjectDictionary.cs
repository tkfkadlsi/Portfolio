using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Tkfkadlsi
{
    public class ObjectDictionary : MonoBehaviour
    {
        public List<ObjectInDictionary> Dictionary = new List<ObjectInDictionary>();

        public Image DictionaryImage;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI ATK;
        public TextMeshProUGUI ATKSpeed;
        public TextMeshProUGUI HP;
        public TextMeshProUGUI Range;
        public TextMeshProUGUI Cost;

        public int count;

        private void Start()
        {
            count = 0;
            SetDictionary();
        }

        public void UpDictionary()
        {
            count++;

            if (count > Dictionary.Count-1)
            {
                count = Dictionary.Count-1;
                return;
            }

            SetDictionary();
        }

        public void DownDictionary()
        {
            count--;

            if (count < 0) 
            {
                count = 0;
                return;
            }

            SetDictionary();
        }

        private void SetDictionary()
        {
            DictionaryImage.sprite = Dictionary[count].Image;
            Name.text = Dictionary[count].Name;
            Description.text = Dictionary[count].ObjectTpye;
            ATK.text = "���ݷ� : " + Dictionary[count].ATK;
            ATKSpeed.text = "���ݼӵ� : " + Dictionary[count].ATKSpeed;
            HP.text = "ü�� : " + Dictionary[count].MaxHP;
            Range.text = "��Ÿ� : " + Dictionary[count].Range;
            Cost.text = "�ʿ� �ڿ� : " + Dictionary[count].CostName + Dictionary[count].Cost + "��";
        }
    }
}
