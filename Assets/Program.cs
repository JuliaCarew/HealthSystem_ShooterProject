using System;
using UnityEngine;

public class HealthSystem
{
    // Variables    
    public int health;
    public int healthMax = 100;
    public string healthStatus;
    public int shield = 100;
    public int lives = 3;

    // Optional XP system variables
    public int xp;
    public int level;

    public void Awake()
    {
        RunAllUnitTests();
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
        if (shield < 0) shield = 0;       
        
        if (shield > 0)
        {
            shield -= damage;
        }
        else if (shield <= 0)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
            if (health == 0 && shield == 0)
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

        shield = 100;
        health = 100;
        lives--;
        if (lives < 0) lives = 0;     
    }
    public void ResetGame()
    {
        if (lives == 0)
        {
            Time.timeScale = 0;

            Revive();
            lives = 3;
        }
        
    }

    // Optional XP system methods
    public void IncreaseXP(int exp)
    {
        // Implement XP increase and level-up logic
    }   
    
        public static void RunAllUnitTests()
        {
            Test_TakeDamage_ShieldOnly();
            Test_TakeDamage_ShieldAndHealth();
            Test_TakeDamage_HealthOnly();
            Test_TakeDamage_HealthDepleted();
            Test_TakeDamage_HealthAndShieldDepleted();
            Test_TakeDamage_NegativeInput();
            Test_Heal_Normal();
            Test_Heal_MaxHealth();
            Test_Heal_NegativeInput();
            Test_RegenerateShield_Normal();
            Test_RegenerateShield_Max();
            Test_RegenerateShield_Negative();
            Test_Revive();
        }
        public static void Test_TakeDamage_ShieldOnly()
        {
            HealthSystem system = new HealthSystem();

            system.TakeDamage(10);

            Debug.Assert(system.shield == 90, " TEST TakeDamage_ShieldOnly Failed");
        }
        public static void Test_TakeDamage_ShieldAndHealth()
        {
            HealthSystem system = new HealthSystem();

            system.TakeDamage(110);

            Debug.Assert(0 == system.shield, " TEST TakeDamage_ShieldAndHealth Failed");
            Debug.Assert(90 == system.health, " TEST TakeDamage_ShieldAndHealth Failed");
        }
        public static void Test_TakeDamage_HealthOnly()
        {
            HealthSystem system = new HealthSystem();
            system.shield = 0;

            system.TakeDamage(10);

            Debug.Assert(0 == system.shield, " TEST TakeDamage_HealthOnly Failed");
            Debug.Assert(90 == system.health, " TEST TakeDamage_HealthOnly Failed");
        }
        public static void Test_TakeDamage_HealthDepleted()
        {
            HealthSystem system = new HealthSystem();
            system.shield = 0;

            system.TakeDamage(100);

            Debug.Assert(0 == system.shield, " TEST TakeDamage_HealthDepleted Failed");
            Debug.Assert(0 == system.health, " TEST TakeDamage_HealthDepleted Failed");
        }
        public static void Test_TakeDamage_HealthAndShieldDepleted()
        {
            HealthSystem system = new HealthSystem();

            system.TakeDamage(200);

            Debug.Assert(0 == system.shield, " TEST TakeDamage_HealthAndShieldDepleted Failed");
            Debug.Assert(0 == system.health, " TEST TakeDamage_HealthAndShieldDepleted Failed");
        }
        public static void Test_TakeDamage_NegativeInput()
        {
            HealthSystem system = new HealthSystem();

            system.TakeDamage(-10);

            Debug.Assert(100 == system.shield, " TEST TakeDamage_NegativeInput Failed");
            Debug.Assert(100 == system.health, " TEST TakeDamage_NegativeInput Failed");
        }
        public static void Test_Heal_Normal()
        {
            HealthSystem system = new HealthSystem();
            system.health = 90;

            system.Heal(10);

            Debug.Assert(100 == system.health, " TEST Heal_Normal Failed");
        }
        public static void Test_Heal_MaxHealth()
        {
            HealthSystem system = new HealthSystem();

            system.Heal(10);

            Debug.Assert(100 == system.health, " TEST Heal_MaxHealth Failed");
        }
        public static void Test_Heal_NegativeInput()
        {
            HealthSystem system = new HealthSystem();

            system.Heal(-10);

            Debug.Assert(100 == system.health, " TEST Heal_NegativeInput Failed");
        }
        public static void Test_RegenerateShield_Normal()
        {
            HealthSystem system = new HealthSystem();
            system.shield = 90;

            system.RegenerateShield(10);

            Debug.Assert(100 == system.shield, " TEST RegenerateShield_Normal Failed");
        }
        public static void Test_RegenerateShield_Max()
        {
            HealthSystem system = new HealthSystem();

            system.RegenerateShield(10);

            Debug.Assert(100 == system.shield, " TEST RegenerateShield_Max Failed");
        }
        public static void Test_RegenerateShield_Negative()
        {
            HealthSystem system = new HealthSystem();

            system.RegenerateShield(-10);

            Debug.Assert(100 == system.shield, " TEST RegenerateShield_Negative Failed");
        }
        public static void Test_Revive()
        {
            HealthSystem system = new HealthSystem();

            system.Revive();

            Debug.Assert(100 == system.shield, " TEST Revive Failed");
            Debug.Assert(100 == system.health, " TEST Revive Failed");
            Debug.Assert(2 == system.lives, " TEST Revive Failed");
        }      
}
