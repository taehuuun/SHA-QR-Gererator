using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("Top")]
    [SerializeField] private TMP_Text topFileNameTxt;
    [SerializeField] private Button onOffBtn;

    [Header("Main")]
    [SerializeField] private Image qrPreivew;
    [SerializeField] private Image progressBarImg;
    [SerializeField] private TMP_InputField typeInput;
    [SerializeField] private TMP_InputField modelIDInput;
    [SerializeField] private TMP_InputField genCountInput;
    [SerializeField] private TMP_Text typeInTxt;
    [SerializeField] private TMP_Text modelIDInTxt;
    [SerializeField] private TMP_Text genCountTxt;
    [SerializeField] private TMP_Text curWorkStateTxt;

    #region Private Func

    private void Start()
    {
        // 최초 실행시
    }

    /// <summary>
    /// 초기 변수 값들을 세팅하는 함수
    /// </summary>
    private void Init()
    {
    }

    /// <summary>
    /// UI요소 업데이트 함수
    /// </summary>
    private void SetUI()
    {
    }

    /// <summary>
    /// 입력 값이 필요한 UI, 변수값의 유효성을 체크하는 함수
    /// </summary>
    /// <returns></returns>
    private bool IsValidValue()
    {
        return false;
    }

    /// <summary>
    /// 실질적인 생성동작을 하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator Gernerate()
    {
        yield return null;
    }

    #endregion

    #region Public Func
    
    /// <summary>
    /// 생성기 On/Off 함수
    /// </summary>
    public void OnOffClick()
    {
    }
    #endregion
}