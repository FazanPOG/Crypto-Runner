using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_GAME_PLAYING = "IsGamePlaying";
    private const string IS_PLAYER_ATTACK = "IsPlayerAttack";
    private const string IS_PLAYER_DEATH = "IsPlayerDeath";
    private const string IS_PLAYER_VICTORY = "IsPlayerVictory";

    private Animator animator;
    private Player.PlayerState playerState;
    private PolicemanTrigger policemanTrigger;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.OnPlayerStateChanged += Player_OnPlayerStateChanged;
    }



    private void Player_OnPlayerStateChanged()
    {
        playerState = Player.Instance.GetPlayerState();

        switch (playerState)
        {
            case Player.PlayerState.Running:
                animator.SetBool(IS_GAME_PLAYING, true);
                animator.SetBool(IS_PLAYER_ATTACK, false);
                break;

            case Player.PlayerState.Fighting:
                animator.SetBool(IS_PLAYER_ATTACK, true);
                break;

            case Player.PlayerState.Death:
                animator.SetTrigger(IS_PLAYER_DEATH);
                break;

            case Player.PlayerState.Victory:
                animator.SetTrigger(IS_PLAYER_VICTORY);
                break;
        }
    }

}
