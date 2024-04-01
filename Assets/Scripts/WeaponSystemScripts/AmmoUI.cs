using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    //public WeaponSO weaponData;
    public TextMeshProUGUI text;

    private WeaponSO currentWeaponData;
    private void Start()
    {
        UpdateAmmoText(currentWeaponData);
    }
    private void Update()
    {
        UpdateAmmoText(currentWeaponData);
    }
    private void OnEnable()
    {
        WeaponHolder.OnWeaponSwitched += UpdateAmmoText;
    }

    private void OnDisable()
    {
        WeaponHolder.OnWeaponSwitched -= UpdateAmmoText;
    }

    private void UpdateAmmoText(WeaponSO weaponData)
    {
        currentWeaponData = weaponData;

        if (currentWeaponData != null)
        {
            text.text = $"{currentWeaponData.currentClip} / {currentWeaponData.maxClipSize} | {currentWeaponData.currentAmmo} / {currentWeaponData.maxAmmoSize}";
        }
        else
        {
            text.text = "No weapon selected";
        }
    }
}
