using SpaceShooter;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

namespace TowerDefense
{
    public class TowerBuyControl : MonoBehaviour
    {
        [SerializeField] private TowerAsset m_ta;
        [SerializeField] private Text m_text;
        [SerializeField] private Button m_button;
        [SerializeField] private Transform buildSite;
      
        public void SetBuildSite(Transform value)
        {  
            buildSite = value;  
        }

        private void Start()
        {
            TDPlayer.GoldUpdateSubscribe(GoldStatusCheck);
            m_text.text = m_ta.goldCost.ToString();
            m_button.GetComponent<Image>().sprite = m_ta.GUISprite;
        }
        private void GoldStatusCheck(int gold)
        {
            if (gold >= m_ta.goldCost != m_button.interactable)
            {
                m_button.interactable = !m_button.interactable;
                m_text.color = m_button.interactable ? Color.white : Color.red;
            }
        }
       
        public void Buy()
        {
            TDPlayer.Instance.TryBuild(m_ta, buildSite);
            BuildSite.HideControls();
        }
    }
}

