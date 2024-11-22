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
            failurePanel.SetActive(false); // ��ʼ������UI���
        UpdateScoreText();
    }

    private void Update()
    {
        // ʵʱ�������Ƿ����60����UI��廹δ��ʾ
        if (!isPanelShown && currentScore < threshold)
        {
            StartCoroutine(CheckAnimationAndShowPanel());
            isPanelShown = true; // ��ֹ�ظ�����
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    // ��ť1������10
    public void AddScore()
    {
        currentScore += bonusScore;
        UpdateScoreText();
    }

    // ��ť2������10
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
        // �ȴ������ص�Idle״̬
        while (!IsInIdleState())
        {
            yield return null; // ÿ֡���
        }

        // �����ص�Idle����ʾUI���
        if (failurePanel != null)
        {
            failurePanel.SetActive(true);
        }
    }

    private bool IsInIdleState()
    {
        // ��ȡ��ǰ����״̬��Ϣ������Idle������״̬��Ϊ "Idle"��
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
