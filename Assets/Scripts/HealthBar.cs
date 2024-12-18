using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float _maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] float _fillSpeed;
    [SerializeField] Gradient _colorGradient;
    //[SerializeField] private Controller theScriptName
    [SerializeField] float _damageAmount,_healAmount;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthText.text = "Health: " + _currentHealth;
    }

    public void UpdateHealth(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        _healthText.text = "Health: " + _currentHealth;
        UpdateHealthBar();
        
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        if(_currentHealth == 0)
        {
            //Die
            // Change the die script in game controller to public
            //theSciptname.Die() (method to die)
            _currentHealth = _maxHealth;
        } 
        _healthText.text = "Health: " + _currentHealth;
        UpdateHealthBar();

    }

    private void Heal(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        UpdateHealthBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            TakeDamage(_damageAmount);
        }
        else if (other.CompareTag("Health"))
        {
            Heal(_healAmount);
            other.gameObject.SetActive(false);
        }
    }

    private void UpdateHealthBar()
    {
        float targetFillAmount = _currentHealth / _maxHealth;
        //_healthBarFill.fillAmount = targetFillAmount;
        _healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        //_healthBarFill.color = _colorGradient.Evaluate(targetFillAmount);
        _healthBarFill.DOColor(_colorGradient.Evaluate(targetFillAmount), _fillSpeed);

    }
}
