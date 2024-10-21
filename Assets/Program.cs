using System;
using UnityEngine;
//using UnityEngine.UI;

public class HealthSystem
{
    // Variables
    //[SerializeField] TextMeshProUGUI GameOverText;
    //[SerializeField] GameObject GameOverTextObject;
    //GameOverTextObject.SetActive(false);
    public int health;
    public int healthMax = 100;
    public string healthStatus;
    public bool shieldActive;
    public int shield = 100;
    public int lives = 3;

    // Optional XP system variables
    public int xp;
    public int level;

    public void Awake()
    {
        HealthSystem.RunAllUnitTests();
    }

    public HealthSystem()
    {
        ResetGame();
    }
    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }
    public string ShowHUD()
    {
        //Display health, shield, lives, and health status string
        if (health <= 10)
        {
            return health + " Imminent Danger " + " shield: " + shield + " lives: " + lives;
        }
        if (health <= 50)
        {
            return health + " Badly Hurt " + " shield: " + shield + " lives: " + lives;
        }
        if (health <= 75)
        {
            return health + " Hurt " + " shield: " + shield + " lives: " + lives;
        }
        if (health <= 90)
        {
            return health + " Healthy " + " shield: " + shield + " lives: " + lives;
        }
        if (health <= 100)
        {
            return health + " Perfect Health " + " shield: " + shield + " lives: " + lives;
        }       
        return " ";
    }
    public void TakeDamage(int damage)
    {
        //Handle damage to shield and health
        //when shield reaches 0, take dmg to health
        if (shield < 0) shield = 0;       
        
        if (shield > 0)
        {
            shieldActive = true;
            shield -= damage;
        }
        else
        {
            shieldActive = false;

            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
            if (health == 0)
            {
                Revive();
            }
        }
    }
    public void Heal(int hp)
    {
        health += hp;
        if (health > healthMax) health = healthMax;
    }
    public void RegenerateShield(int hp)
    {
        shield += hp;
        if (shield > 100) shield = 100;
    }
    public void Revive()
    {
        var originPosition = Player.instance.transform.position + Vector3.up * 0.5f;
        //need to despawn all current enemies
        shield = 100;
        health = 100;
        lives--;
        if (lives < 0) lives = 0;
    }
    public void ResetGame()
    {
        //GameOverTextObject.SetActive(true);
        Revive();
        lives = 3;
    }

    // Optional XP system methods
    public void IncreaseXP(int exp)
    {
        // Implement XP increase and level-up logic
    }

    // !!TESTS!! //
    public static void RunAllUnitTests()
    {
        Test_TakeDamage_ShieldOnly();
    }
    public static void Test_TakeDamage_ShieldOnly()
    {
        HealthSystem system = new HealthSystem();

        system.TakeDamage(10);

        Debug.Assert(system.shield == 90, " TEST TakeDamage_ShieldOnly Failed");
    }
    public void Test_TakeDamage_ShieldAndHealth()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;

        system.TakeDamage(20); //deplete shield first?

        Debug.Assert(90 == system.shield);
        Debug.Assert(90 == system.health);
    }
    public void Test_TakeDamage_HealthOnly()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;

        system.TakeDamage(110);

        Debug.Assert(0 == system.shield);
        Debug.Assert(90 == system.health);
    }
    public void Test_TakeDamage_HealthDepleted()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        shieldActive = false;

        system.TakeDamage(100);

        Debug.Assert(100 == system.shield);
        Debug.Assert(0 == system.health);
    }
    public void Test_TakeDamage_HealthAndShieldDepleted()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;

        system.TakeDamage(200);

        Debug.Assert(0 == system.shield);
        Debug.Assert(0 == system.health);
    }
    public void Test_TakeDamage_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;

        system.TakeDamage(-10);

        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
    }
    public void Test_Heal_Normal()
    {
        HealthSystem system = new HealthSystem();
        system.health = 90;

        system.Heal(10);

        Debug.Assert(100 == system.health);
    }
    public void Test_Heal_MaxHealth()
    {
        HealthSystem system = new HealthSystem();
        system.health = 100;

        system.Heal(10);

        Debug.Assert(100 == system.health);
    }
    public void Test_Heal_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.health = 100;

        system.Heal(-10);

        Debug.Assert(90 == system.health);
    }
    public void Test_RegenerateShield_Normal()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 90;

        system.RegenerateShield(10);

        Debug.Assert(100 == system.shield);
    }
    public void Test_RegenerateShield_Max()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;

        system.RegenerateShield(10);

        Debug.Assert(100 == system.shield);
    }
    public void Test_RegenerateShield_Negative()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;

        system.RegenerateShield(-10);

        Debug.Assert(90 == system.shield);
    }
    public void Test_Revive()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.Revive();

        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(2 == system.lives);
    }
}
//shield doesnt show dmg until it reaches 0
