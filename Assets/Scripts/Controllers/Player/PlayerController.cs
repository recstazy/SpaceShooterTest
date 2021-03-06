﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerController : MonoBehaviour
{
    public delegate void AttackHandler(Vector2 pos); // Delegates the player position for bullet to spawn at
    public static event AttackHandler OnAttack;

    public PlayerModel PlayerModel { get; set; } = new PlayerModel();
    
    private void OnEnable()
    {
        GameController.OnRestart += Restart;
    }

    private void Start()
    {
        InputController.TouchPos.AsObservable()
            .Subscribe(WritePosToModel);
        
        StartCoroutine(Attack());
    }

    void WritePosToModel(Vector2 pos)
    {
        if (pos != null && pos != Vector2.zero)
        {
            PlayerModel.Position.Value = pos;
        }
    }

    IEnumerator Attack()
    {
        while (PlayerModel.Shooting)
        {
            OnAttack?.Invoke(PlayerModel.Position.Value);
            yield return new WaitForSeconds(PlayerModel.AttackSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            PlayerModel.HP.Value--;
        }
    }

    void Restart()
    {
        PlayerModel.SetDefaults();
    }

    private void OnDestroy()
    {
        GameController.OnRestart -= Restart;
    }

    // just for easier testing, will be deleted
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PlayerModel.Position.Value += Vector2.left * 10 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            PlayerModel.Position.Value += Vector2.right * 10 * Time.deltaTime;
        }
    }
}
