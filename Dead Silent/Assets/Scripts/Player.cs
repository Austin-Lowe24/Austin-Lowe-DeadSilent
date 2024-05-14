using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] CharacterController Controller;
    
    [SerializeField] int Health;

    [SerializeField] int Speed;
    [SerializeField] int SprintMod;

    [SerializeField] int JumpMax;
    [SerializeField] int JumpSpeed;
    [SerializeField] int Gravity;

    [SerializeField] FireArm Weapon;

    Vector3 MoveDir;
    Vector3 PlayerVel;

    

    int JumpCount;

    private int MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = Health;
        UpdatePlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetButton("Fire1") && Weapon != null)
        {
            Weapon.Attack();
        }
    }

    void Movement()
    {
        if (Controller.isGrounded)
        {
            JumpCount = 0;
            PlayerVel = Vector3.zero;
        }

        MoveDir = (Input.GetAxis("Horizontal") * transform.right) + (Input.GetAxis("Vertical") * transform.forward);
        Controller.Move(MoveDir * Speed * Time.deltaTime);

        Sprint();

        if (Input.GetButtonDown("Jump") && JumpCount < JumpMax)
        {
            JumpCount++;
            PlayerVel.y = JumpSpeed;
        }

        PlayerVel.y -= Gravity * Time.deltaTime;
        Controller.Move(PlayerVel * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            Speed *= SprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            Speed /= SprintMod;
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        UpdatePlayerUI();
        StartCoroutine(flashDamage());

        if (Health <= 0)
        {
            // trigger loss
            GameManager.Instance.lostState();
        }
    }
    IEnumerator flashDamage()
    {
        GameManager.Instance.playerDFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.playerDFlash.SetActive(false);
    }

    void UpdatePlayerUI()
    {
        // update health bar
        GameManager.Instance.PlayerHPBar.fillAmount = (float)Health / MaxHealth;
    }
}
