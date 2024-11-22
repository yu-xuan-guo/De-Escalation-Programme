using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // TextMeshPro-TextMeshProUGUI
    public int currentScore;
    public int bonusScore;
    public int penaltyScore;
    public int threshold = 60; 
    public GameObject failurePanel;
    private bool isPanelShown = false;

    public Animator characterAnimator;

    void Start()
    {
        if (failurePanel != null)
            failurePanel.SetActive(false); // 初始化隐藏UI面板
        UpdateScoreText();
    }

    private void Update()
    {
        // 实时检测分数是否低于60并且UI面板还未显示
        if (!isPanelShown && currentScore < threshold)
        {
            StartCoroutine(CheckAnimationAndShowPanel());
            isPanelShown = true; // 防止重复触发
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    // 按钮1：增加10
    public void AddScore()
    {
        currentScore += bonusScore;
        UpdateScoreText();
    }

    // 按钮2：减少10
    public void SubtractScore()
    {
        currentScore -= penaltyScore;
        UpdateScoreText();
    }

    void ShowUIPanel()
    {
        if (failurePanel != null && !failurePanel.activeSelf)
        {
            failurePanel.SetActive(true);
        }
    }

    private IEnumerator CheckAnimationAndShowPanel()
    {
        // 等待动画回到Idle状态
        while (!IsInIdleState())
        {
            yield return null; // 每帧检测
        }

        // 动画回到Idle后显示UI面板
        if (failurePanel != null)
        {
            failurePanel.SetActive(true);
        }
    }

    private bool IsInIdleState()
    {
        // 获取当前动画状态信息（假设Idle动画的状态名为 "Idle"）
        AnimatorStateInfo stateInfo = characterAnimator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Idle");
    }


    public void CheckScore()
    {
        if (currentScore < threshold)
        {
            ShowUIPanel();
        }
    }

}
