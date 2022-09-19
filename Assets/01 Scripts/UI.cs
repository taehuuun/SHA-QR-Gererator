using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("Top")]
    [SerializeField] private TMP_Text topFileNameTxt;
    [SerializeField] private TMP_Text onOffBtnTxt;

    [Header("Main")]
    [SerializeField] private RawImage qrPreivew;
    [SerializeField] private Image progressBarImg;
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button saveAllBtn;
    [SerializeField] private Button prevBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private TMP_InputField typeInput;
    [SerializeField] private TMP_InputField modelIDInput;
    [SerializeField] private TMP_InputField genCountInput;
    [SerializeField] private TMP_Text curWorkStateTxt;

    [Header("Variable")]
    [SerializeField] private bool isActive;
    [SerializeField] private int curGenCnt;
    [SerializeField] private int maxGenCnt;
    [SerializeField] private string type;
    [SerializeField] private string modelID;
    [SerializeField] private string curSHAVal;
    [SerializeField] private string pathStr;

    [SerializeField] private List<Texture2D> genQRCodes = new List<Texture2D>();
    [SerializeField] private List<string> shaDatas = new List<string>();
    [SerializeField] private int curSelIdx = 0;

    private const int MAX_COUNT = 100;
    

    #region Private Func

    private void Start()
    {
        // 최초 실행시
        Init();
    }

    /// <summary>
    /// 초기 변수 값들을 세팅하는 함수
    /// </summary>
    private void Init()
    {
        isActive = false;
        curGenCnt = 0;
        prevBtn.interactable = false;
        nextBtn.interactable = false;
    }

    private void InputActive(bool active)
    {
        typeInput.interactable = !active;
        modelIDInput.interactable = !active;
        genCountInput.interactable = !active;
    }

    private void SetVariable()
    {
        maxGenCnt = int.Parse(genCountInput.text);
        type = typeInput.text;
        modelID = modelIDInput.text;
        progressBarImg.fillAmount = 0f;
        curGenCnt = 1;
        maxGenCnt = int.Parse(genCountInput.text);
    }

    /// <summary>
    /// 입력 값이 필요한 UI, 변수값의 유효성을 체크하는 함수
    /// </summary>
    /// <returns></returns>
    private bool IsUIValidValue()
    {
        if(int.Parse(genCountInput.text) > MAX_COUNT)
        {
            Debug.LogError($"생성 가능 최대치는 {MAX_COUNT} 입니다.");
            return false; 
        }
        return true;
    }

    /// <summary>
    /// 실질적인 생성동작을 하는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator Gernerate()
    {
        while(isActive)
        {
            // QRCode 생성
            curGenCnt++;
            string sha = CryptoQR.EncryptSHA(type + modelID + curGenCnt.ToString());
            
            Texture2D newQRCode = CryptoQR.CreateQRCode(sha);
            qrPreivew.texture = newQRCode;

            genQRCodes.Add(newQRCode);
            shaDatas.Add(sha);

            float percent = (curGenCnt / maxGenCnt) * 100;
            curWorkStateTxt.text = $"{curGenCnt} / {maxGenCnt} ({percent.ToString("0.##")}";
            progressBarImg.fillAmount = percent * 0.01f;

            yield return new WaitForSeconds(1f);
        }

        InputActive(isActive);
        curSelIdx = 0;
        qrPreivew.texture = genQRCodes[curSelIdx];
        nextBtn.interactable = genQRCodes.Count > 1;
    }

    #endregion

    #region Public Func
    
    /// <summary>
    /// 생성기 On/Off 함수
    /// </summary>
    public void OnOffClick()
    {
        isActive = !isActive;
        onOffBtnTxt.text = isActive ? "Stop" : "Start";

        InputActive(isActive);

        if(isActive)
        {
            if(IsUIValidValue())
            {
                SetVariable();
                StartCoroutine(Gernerate());
            }
            else
            {
                isActive = false;
                InputActive(isActive);
            }
        }
    }

    public void QRCodeSelect(int value)
    {
        if (curSelIdx > 0 && curSelIdx < genQRCodes.Count)
        {
            curGenCnt += value;

            prevBtn.interactable = curGenCnt > 0;
            nextBtn.interactable = curGenCnt < genQRCodes.Count;
        }
    }

    public void SaveBtn()
    {
        CryptoQR.Save(genQRCodes[curGenCnt],pathStr, shaDatas[curGenCnt]);
    }

    public void SavelAllBtn()
    {
        for(int i = 0 ; i < maxGenCnt; i++)
        {
            CryptoQR.Save(genQRCodes[i],pathStr, shaDatas[i]);
        }
    }

    #endregion
}