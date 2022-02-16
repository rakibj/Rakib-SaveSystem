using System;
using SaveSystem.Core;
using UnityEngine;
using UnityEngine.UI;

namespace SaveSystem.Test
{
    [Serializable]
    public class CurrencyEntity : BaseEntity
    {
        public Color iconColor;
        public int value;

        public CurrencyEntity(Color iconColor, int value)
        {
            this.iconColor = iconColor;
            this.value = value;
        }
    }

    public class CurrencyPanel : MonoBehaviour, IEntityHolder<CurrencyEntity>
    {
        [SerializeField] private Image iconImg;
        [SerializeField] private Text valueText;
        [SerializeField] private Button increaseButton;
        [SerializeField] private Button decreaseButton;

        public CurrencyEntity GetEntity()
        {
            var currencyEntity = new CurrencyEntity(iconImg.color, getCurrencyValue());
            return currencyEntity;
        }

        public CurrencyEntity GetDefaultEntity()
        {
            return GetEntity();
        }

        public void SetEntity(CurrencyEntity entity)
        {
            iconImg.color = entity.iconColor;
            valueText.text = entity.value.ToString();
        }

        private void OnEnable()
        {
            increaseButton.onClick.AddListener(increaseCurrencyByOne);
            decreaseButton.onClick.AddListener(decreaseCurrencyByOne);
        }

        private void increaseCurrencyByOne() => addCurrency(1);
        private void decreaseCurrencyByOne() => addCurrency(-1);

        private void addCurrency(int value)
        {
            var visibleValue = getCurrencyValue();
            var increasedValue = visibleValue + value;
            valueText.text = increasedValue.ToString();
        }

        private int getCurrencyValue()
        {
            return int.Parse(valueText.text);
        }

        private void OnDisable()
        {
            increaseButton.onClick.RemoveListener(increaseCurrencyByOne);
            decreaseButton.onClick.RemoveListener(decreaseCurrencyByOne);
        }

    }
}