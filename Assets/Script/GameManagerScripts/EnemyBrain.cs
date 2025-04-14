using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBrain : MonoBehaviour
{
    private GameObject player;

    public static EnemyBrain Instance{get; private set;}
    public Transform playerLocation;

    void OnEnable()
    {
        Instance=this;
        getPlayer();
    }

    void getPlayer()
    {
        player= FindAnyObjectByType<Player3rdPersonMovement>().gameObject;        
    }
    void Update()
    {
        playerLocation=player.transform;
    }

    

}