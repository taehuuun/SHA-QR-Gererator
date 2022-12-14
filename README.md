# SHA-QR-Gernerator

이 프로젝트는 입력한 데이터를 SHA-256 알고리즘으로 암호화 하여 QR코드로 생성하며, 대량 생산이 가능합니다. <br/>

| [:sparkles:사용방법_Getting Started](#사용방법_getting-started) | [:rocket: 다운로드_Download](#다운로드_download) | [:camera: 스크린샷_Screenshots](#스크린샷_screenshots) |
| --------------- | -------- | ----------- |

## 사용방법_Getting Started

아래 절차들을 따르세요. <br/>

### 프로젝트 세팅 / Project Setting
1. [요구사항/Make sure you have all Requirements](#요구사항_requirements)
2. [다운로드/Download Project](#다운로드_download)
3. 프로젝트 실행 [Open Project]

### 생성기 사용 / Using the generator
1. Assets/SHA-QR-Generator/00.Scene/Generator 씬을 엽니다. (해상도 / 1920x1080 FHD) <br/>
   
2. UI오브젝트의 UI스크립트를 찾습니다. <br/>
   
3. UI스크립트내 PathStr에 QR코드가 저장될 폴더 경로를 입력합니다. <br/>
   
4. 씬을 실행합니다. <br/>
   
5. 현재는 Type, ModleID, GenCnt(생성수)의 값을 입력해야하며 직접 커스텀하여 데이터를 추가, 제거 할수 있습니다.<br/>
   
6. Start버튼을 누릅니다.<br/>
   
7. 생성이 끝나면 생성된 QR코드를 좌,우 버튼을 통해 미리볼수 있습니다. (현재 형식은 [Type].[SHA-256] 형태로 타나냄)<br/>
   

### 생성된 QR코드 저장 / Save generated QR code
- 원하는 QR코드 데이터를 버튼으로 이동하여 선택후 Save버튼을 눌러 저장을 진행합니다. <br/>
  
  
- SaveAll 버튼으로 생성된 모든 QR코드를 저장합니다.
  

## 요구사항_Requirements

- [유니티 엔진 / Unity Game Engine](https://unity3d.com) (version: 2021.3.8f1)

## 다운로드_Download

아래 두가지 방법중 한가지를 골라 다운로드를 진행하세요. <br/>


1. 레포지토리를 클론합니다 (Clone the repository locally).

```bash
git clone https://github.com/taehuuun/SHA-QR-Gererator
```

2. Release탭을 통해 원하는 버전대(최신버전 추천) 유니티 패키지를 다운로드하고 프로젝트에 패키지를 추가하세요. <br/>


## 스크린샷_Screenshots

<p align="center">
 <img src="https://user-images.githubusercontent.com/43982651/198884287-0c9fb3e6-d5c5-46a3-a9a5-8a69b75e7ac7.png" width="50%" height="100%"/>
 <img src="https://user-images.githubusercontent.com/43982651/198884404-8d3c376d-b9e9-4f86-bd0f-1efa285c1aa4.png" width="50%" height="100%"/>
</p>
