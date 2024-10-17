using System;
public class HealthSystem
{
    // Variables
    public int health;
    public int healthMax = 100;
    public string healthStatus;
    //public int shield;
    public int shield = 100;
    public int lives = 3;

    // Optional XP system variables
    public int xp;
    public int level;

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
        shield -= damage;
        if (shield < 0) shield = 0;
        //once you lose all shield
        if (shield == 0)
        {
            health -= damage;
            if (health < 0) health = 0;
            if (health == 0)
            {
                //show 'gameover' screen
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
        //Reset health and shield, use one life, respawn player
        //gameObject.SetActive(true); *reference Player here
        shield = 100;
        healthMax = 100;
        lives--;
    }

    public void ResetGame()
    {
        // Reset all variables to default values
        Revive();
        lives = 3;
    }

    // Optional XP system methods
    public void IncreaseXP(int exp)
    {
        // Implement XP increase and level-up logic
    }
}
//player script add last checked shield so it doesnt regen at the wrong time
//take dmg from shield first, then health
//shield pick ups not working or not displaying
//shield doesnt show dmg until it reaches 0
