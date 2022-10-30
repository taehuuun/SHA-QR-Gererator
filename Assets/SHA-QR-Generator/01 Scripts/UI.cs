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
    [SerializeField] private int curSelIdx = 1;

    private const int MAX_COUNT = 10000;
    

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
        topFileNameTxt.text = "";
        typeInput.text = "";
        modelIDInput.text = "";
        genCountInput.text = "";

        isActive = false;
        curGenCnt = 0;
        prevBtn.interactable = false;
        nextBtn.interactable = false;
        saveAllBtn.interactable = false;
        saveBtn.interactable = false;
        progressBarImg.fillAmount = 0f;
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
    }

    /// <summary>
    /// 입력 값이 필요한 UI, 변수값의 유효성을 체크하는 함수
    /// </summary>
    /// <returns></returns>
    private bool IsUIValidValue()
    {
        if(typeInput.text == "" || modelIDInput.text == "" || genCountInput.text =="")
        {
            Debug.LogError($"빈값을 입력 하셨습니다.");
            return false;             
        }

        if(int.Parse(genCountInput.text) > MAX_COUNT)
        {
            Debug.LogError($"생성 가능 최대치는 {MAX_COUNT} 입니다.");
            return false; 
        }
        return true;
    }

    private void ChangeSelect()
    {
        prevBtn.interactable = curSelIdx > 1;
        nextBtn.interactable = curSelIdx < genQRCodes.Count;

        qrPreivew.texture = genQRCodes[curSelIdx-1];
        topFileNameTxt.text = $"{type}.{shaDatas[curSelIdx-1]}";
        curWorkStateTxt.text = $"{curSelIdx} / {genQRCodes.Count}";
        progressBarImg.fillAmount = (float)curSelIdx / (float)genQRCodes.Count;
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
            
            // 생성된 QRCode를 Texture2D형태로 변수 할당 및 미리보기 이미지 변경
            Texture2D newQRCode = CryptoQR.CreateQRCode(sha);
            qrPreivew.texture = newQRCode;

            // 현재 SHA데이터와 QR코드를 리스트에 추가
            genQRCodes.Add(newQRCode);
            shaDatas.Add(sha);

            // 진행 상황을 업데이트
            float percent = ((float)curGenCnt / (float)maxGenCnt) * 100;
            Debug.Log(percent);
            topFileNameTxt.text = $"{type}.{sha}";
            curWorkStateTxt.text = $"{curGenCnt} / {maxGenCnt} ({percent.ToString("0.##")}%)";
            progressBarImg.fillAmount = percent * 0.01f;

            if (curGenCnt >= maxGenCnt)
            {
                Debug.Log("생성 완료");
                isActive = false;
                // Input UI를 활성화 상태로 전환
                InputActive(isActive);

                // 현재 선택한 QR코드 미리보기 인덱스를 0으로 설정
                curSelIdx = 1;
                curGenCnt = 0;

                // 0번째 QR코드를 미리보기 이미지로 변경
                qrPreivew.texture = genQRCodes[curSelIdx];

                // 생성된 QR코드 리스트의 갯수가 1보다 클경우 다음 선택버튼 활성화.
                prevBtn.interactable = false;
                nextBtn.interactable = genQRCodes.Count > 1;

                // 위와 동일 Save버튼에 적용됨.
                saveBtn.interactable = true;
                saveAllBtn.interactable = genQRCodes.Count > 1;

                curWorkStateTxt.text = $"{curSelIdx} / {maxGenCnt}";
                progressBarImg.fillAmount = 1 / (float)maxGenCnt;
                onOffBtnTxt.text = "Start";

                break;
            }

            yield return new WaitForFixedUpdate();
        }
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
            genQRCodes.Clear();
            shaDatas.Clear();
            curGenCnt = 0;

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

    public void PrevBtn()
    {
        if (curSelIdx > 1)
            curSelIdx--;
        
        ChangeSelect();
    }

    public void NextBtn()
    {
        if (curSelIdx < genQRCodes.Count)
            curSelIdx++;
        
        ChangeSelect();
    }

    public void SaveBtn()
    {
        CryptoQR.Save(genQRCodes[curSelIdx],pathStr+ @"\" + type, $"{type}.{shaDatas[curSelIdx]}");
    }

    public void SavelAllBtn()
    {
        for(int i = 0 ; i < maxGenCnt; i++)
        {
            CryptoQR.Save(genQRCodes[i],pathStr+ @"\" + type, $"{type}.{shaDatas[i]}");
        }
    }

    #endregion
}