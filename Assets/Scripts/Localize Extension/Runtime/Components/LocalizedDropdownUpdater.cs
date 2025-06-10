using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

// [REF]
//  UnityForum: Localizing UI Dropdown Options https://discussions.unity.com/t/localizing-ui-dropdown-options/792432

namespace Project.Localization
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public sealed class TMP_DropdownLocalizedOptions : MonoBehaviour
    {
        [SerializeField] private List<LocalizedString> _optionStrings;
        private TMP_Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        private void OnEnable()
        {
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
            OnLocaleChanged(LocalizationSettings.SelectedLocale);
        }

        private void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }


        private async void OnLocaleChanged(Locale locale)
        {
            if (_dropdown == null || locale == null) return;
            
            _dropdown.options.Clear();
            foreach (var localizedString in _optionStrings)
            {
                if (localizedString.IsEmpty)
                {
                    Debug.LogWarning("Empty Table Reference. Must contain a Guid or Table Collection Name");
                    _dropdown.options.Add(new TMP_Dropdown.OptionData("---"));
                }
                else
                {
                    var value = localizedString.GetLocalizedString();
                    _dropdown.options.Add(new TMP_Dropdown.OptionData(value));
                }
            }

            _dropdown.RefreshShownValue();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_dropdown == null)
                _dropdown = GetComponent<TMP_Dropdown>();

            OnLocaleChanged(LocalizationSettings.SelectedLocale);
        }
#endif
    }
}