using UnityEngine;

public class RadioSwitch : ClickableObject
{
    [SerializeField] private AK.Wwise.Event radioOn; // Івент для старту
    [SerializeField] private AK.Wwise.Event radioOff; // Івент для зупинки

    private bool _isRadioOn = false; // Стан радіо

    private void Awake()
    {
        // Перевірка, чи встановлені івенти через інспектор
        if (radioOn == null || radioOff == null)
        {
            Debug.LogWarning("Не всі івенти встановлені в інспекторі.");
        }
    }

    public override void OnClick()
    {
        if (_isRadioOn)
        {
            radioOff.Post(gameObject); // Виклик івенту зупинки
        }
        else
        {
            radioOn.Post(gameObject); // Виклик івенту старту
        }

        _isRadioOn = !_isRadioOn; // Зміна стану радіо
    }
}