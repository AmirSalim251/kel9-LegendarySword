using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public Collider swordCollider;
    private Collider enemyCollider;
  
    public Controller_Battle cb;
    public GameController gameController;
    private int turnCount = 0;

    private void Start() {
        enemyCollider = gameController.GetSpawnedMonsterCollider();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == enemyCollider)
        {
            cb.playerAttackRT();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == enemyCollider)
        {
            // You can add logic here if you want to continuously check while colliding
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == enemyCollider)
        {
            // Logic for when the sword leaves the enemy collider
        }
    }

  

    
}
