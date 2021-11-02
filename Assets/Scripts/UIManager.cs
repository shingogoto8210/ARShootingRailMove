using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button btnRightPrefab;
    [SerializeField]
    private Button btnLeftPrefab;
    private Button btnRight;
    private Button btnLeft;
    [SerializeField]
    private Transform canvasTran;
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameManager gameManager;

    public void UpdateBranchDisplay()
    {
        if (btnRight == null)
        {
            btnRight = Instantiate(btnRightPrefab, canvasTran, false);
            btnLeft = Instantiate(btnLeftPrefab, canvasTran, false);
            btnRight.onClick.AddListener(() => 
            {
                gameManager.nextBranchDirectionType = BranchDirectionType.Right;
                gameManager.isSelect = true;
                canvasGroup.alpha = 0;
            });
            btnLeft.onClick.AddListener(() =>
            {
                gameManager.nextBranchDirectionType = BranchDirectionType.Left;
                gameManager.isSelect = true;
                canvasGroup.alpha = 0;
            });
        }
        else
        {
            canvasGroup.alpha = 1;
        }
    }
}
