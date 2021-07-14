# Monoon
### ❄️  모험의 눈송이

 🛠  `Unity` `C#` `blender` `SketchUp` `Qubicle` `Firebase`

--- 
***나 대신 학교 가주는 눈송이가 있다?!***

귀여운 눈송이가 되어 학교를 누벼보세요!<br/>
학교에 있는 다른 눈송이들과 대화를 할 수 있어요!<br/>
비오는 날엔 우산을 빌려주는 것은 어떨까요?<br/>
학교 곳곳에 숨어있는 눈덩이를 모아 눈송이를 꾸며보세요! 

---

**1. 로그인화면 구성(로그인화면 - 사용자 등록화면 - 비밀번호 재설정 화면)**

  인증된 숙명 웹메일만을 사용해 회원가입 및 로그인이 가능합니다. 우측 아래에 있는 Register 버튼을 통해 회원가입이 가능하고, 비밀번호를 잊었다면 forgot password를 클릭해 등록된 이메일로 비밀번호를 재설정할 수 있습니다.

![Untitled 6](https://user-images.githubusercontent.com/53874628/125563283-6ae837ca-b174-4139-b97e-3376bb3055f1.png)

**2. 메인 화면(메인 기본 화면 - 확대 화면 - 축소 화면)**

로그인 후 자동으로 학교 전경이 3D로 표현되어 있는 메인화면으로 넘어갑니다. 터치를 통해 원하는 곳으로 눈송이를 이동시킬 수 있으며, 화면 축소, 회전이 지원됩니다. 원하는 관을 클릭하면 관별 채팅방에 입장 할 수 있습니다.

![Untitled 7](https://user-images.githubusercontent.com/53874628/125563300-f1b3a0bc-be7c-4df0-8885-7a2f9c582703.png)

**3. 날씨에 따라 변하는 배경(비 오는 화면 - 눈오는 화면)**

메인화면의 배경에는 시간과 날씨 정보가 지원됩니다. 비나 눈이 오는 날에는 배경에도 비가 내리거나 눈이 내려 시각적으로도 확인할 수 있습니다.

![Untitled 8](https://user-images.githubusercontent.com/53874628/125563306-c5fa8d03-0171-432b-a5ef-a87c0667d997.png)

**4. 채팅방(순헌관 - 새힘관 - 도서관)**

  감정표현, 전체채팅, 1:1채팅 : 관별 채팅방에 입장할 시 해당 관에 접속해 있는 다른 유저들과 대화를 할 수 있습니다. 관 별로 대표적인 공간을 모델 삼아 오브젝트들을 제작 및 배치하였습니다. 구현된 채팅방은 명신, 새힘, 진리, 순헌, 도서관 입니다. 채팅방 내에서는 눈송이를 터치할 시 3가지 감정표현(인사, 앉기, 놀람) 이모티콘이 뜨고, 터치 할 시 그에 맞는 행동을 하는 눈송이를 볼 수 있습니다. 왼쪽 아래 채팅창에 보내고 싶은 말을 입력 후 전송을 누르면 캐릭터 위에 말풍선으로 표현됩니다. 또한, 채팅창 좌측하단에 채팅 대상을 변경 하는 버튼을 누르면 현재 접속 해 있는 유저에게 다른 사람에게는 보이지 않는 개인 메시지를 보낼 수 있습니다.

![Untitled 9](https://user-images.githubusercontent.com/53874628/125563313-5c6e91b1-eb12-4172-a366-871067febcbe.png)

**(채팅 기능 - 귓속말 기능)**

![Untitled](https://user-images.githubusercontent.com/53874628/125563318-7210c5d3-d969-41d6-9092-4c20500979c4.png)

**(감정 표현)**

![Untitled 1](https://user-images.githubusercontent.com/53874628/125563227-196c92f9-cbfe-4d6b-b8e2-e7b2f1097c3a.png)

**5. 메인 UI - 지도(꿀팁), 마이홈, 설정, 종료**

**(지도)** 지도 아이콘을 누르면 나오는 모습입니다. 전체적인 학교의 모습을 한눈에 볼 수 있습니다.

**(순간이동)** 원하는 관을 클릭하고 순간이동할래! 버튼을 누르면 사진과 같이 해당하는 관 앞으로 캐릭터를 순간이동시킬 수 있습니다.

![Untitled 2](https://user-images.githubusercontent.com/53874628/125563249-863fba33-2319-402a-8943-7709eeff1606.png)

**(꿀팁)** 원하는 관을 클릭하고 꿀팁볼래! 버튼을 누르면 사진과 같이 꿀팁 목록이 나오고, 보고싶은 꿀팁을 클릭하면 해당하는 꿀팁을 볼 수 있습니다.

![Untitled 3](https://user-images.githubusercontent.com/53874628/125563255-dd1e6024-7940-48b9-8935-5794f4a9e581.png)

**(상점)** 학생회관 자리에 위치한 상점을 클릭해 입장하여 자판기에서 아이템을 구매 할 수 있습니다. 학교를 돌아다니며 모은 눈덩이를 재화로 사용하여 삼십여 가지의 아이템을 선택 할 수 있습니다.

**(마이홈)** 마이홈 아이콘을 누르면 나오는 모습입니다. 버튼을 통해 옷장을 열고 닫을 수 있으며, 옷장을 열면 캐릭터의 인벤토리가 나옵니다. 인벤토리에서 장착하고 싶은 아이템을 드래그 하여 오른쪽 위 장비창에 갖다 놓으면 눈송이에게 아이템을 입혀줄 수 있습니다.

![Untitled 4](https://user-images.githubusercontent.com/53874628/125563271-c09549a0-96dc-4863-a5f8-285e12183ed8.png)

**(설정)** 설정 아이콘을 누르면 나오는 모습입니다. 로그아웃 버튼을 눌러 로그아웃 할 수 있으며, 설정과 사운드 기능은 추후 추가될 예정입니다. 하단에는 개발자와 버전 정보가 있습니다.

**(종료)** 종료 아이콘을 누르면 나오는 모습입니다. 더 할래!를 누르면 게임을 이어서 할 수 있으며 네를 누르면 게임을 종료할 수 있습니다.

![Untitled 5](https://user-images.githubusercontent.com/53874628/125563276-632b72c8-07b4-47c4-8511-8eda02e7031d.png)


