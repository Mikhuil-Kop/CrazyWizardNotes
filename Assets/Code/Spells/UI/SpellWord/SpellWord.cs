using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Spells;

namespace Spells.UI
{
    public class SpellWord : MonoBehaviour, IDragHandler
    {
        [Header("Элементы")]
        public RectTransform RectTransform;
        public Image IconRenrerer;
        public TextMeshProUGUI NameText;

        [Header("Информация")]
        public string WordName;
        public Sprite WordIcon;
        public AudioClip WordSound;
        public string WordTitle;
        [TextArea(3, 3)]
        public string WordDescription;


        public void OnDrag(PointerEventData eventData)
        {
            RectTransform.position = eventData.position;
        }
    }

}