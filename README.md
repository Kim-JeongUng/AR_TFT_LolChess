# AR_TFT_LolChess  
### AR기말 팀 프로젝트 롤토체스 AR
리그오브레전드 전략적 팀 게임을 AR보드게임으로 만나보세요   
캐릭터를 뽑고, 아이템을 장착시켜 상황별 플레이를 할 수 있습니다.
<img width="120%" src="https://user-images.githubusercontent.com/82865325/146878213-e830340f-e737-42c1-96a5-fca74a472d94.gif">   

#### [최종 플레이 영상(Youtube)](https://youtu.be/ipIZVzVQ0Qk)
#### [코드설명 및 발표자료(PPT DownLoad)](https://github.com/Kim-JeongUng/AR_TFT_LolChess/raw/main/%ED%85%80%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%EA%B9%80%EC%A0%95%EC%9B%85%2C%EC%9D%B4%EC%8A%B9%EC%96%B8%2C%EC%9D%B4%ED%99%94%EC%A4%80.pptx)

<br><br>
## 게임 요약
- 라운드제로 진행 되는 게임으로, 챔피언 카드와 아이템 카드를 뽑아 전략을 구사하여 상대방과 AR가상 전투를 하는 게임
- [TFT(롤토체스)](https://namu.wiki/w/%EB%A6%AC%EA%B7%B8%20%EC%98%A4%EB%B8%8C%20%EB%A0%88%EC%A0%84%EB%93%9C/%EC%A0%84%EB%9E%B5%EC%A0%81%20%ED%8C%80%20%EC%A0%84%ED%88%AC "TFT(롤토체스)")를 모티브로 AR구현해 제작한 [TCG](https://namu.wiki/w/%ED%8A%B8%EB%A0%88%EC%9D%B4%EB%94%A9%20%EC%B9%B4%EB%93%9C%20%EA%B2%8C%EC%9E%84?from=TCG) 장르

## 가상버튼
<img width="100%" src="https://user-images.githubusercontent.com/82865325/146875990-aa5838b3-141e-4da4-ad24-17675ae84e0f.gif">
- 마커가 가상버튼 2개를 동시에 가리면 해당 마커가 올려진 보드판의 자식으로 할당 및 게임오브젝트 할당
      
## 게임 룰 설명
1. 2인 보드 게임
2. 최초 챔피언 뭉치에서 챔피언 카드를 뽑음(챔피언 카드는 총 6장 중복 X) 
3. 각 턴때 레드팀, 블루팀으로 나뉘고 본인턴 때 아이템을 드롭(아이템 카드뭉치에서 한장씩)한다. 그 후 라운드 시작 카드를 인식해서 게임을 시작한다.
4. 드롭한 아이템을 사용자는 아이템을 원하는 챔피언에 장착 시킬 수 있다.
5. 각 챔피언별 고유 능력치가 있고, ap, ad, 서포터로 나누어짐.
6. 아이템 2개는 조합해서 새로운 특별능력을 가진 아이템을 만들 수 있다. -> 대천사, 가엔, 등등 조합표는 주어진다.
7. 매 라운드마다 사용자의 챔피언들은 전투하고, 챔피언이 모두 죽으면 패배하며, 패배시 플레이어의 체력이 떨어짐(롤토체스와 동일)
8. 플레이어(꼬물이) 체력이 0이 되면 게임이 끝남
 
   
## 각 캐릭터 챔피언 별 스킬
- **케이틀린(블루팀 상단)** : 강력한 물리 계수를 가진 투사체를 발사합니다.
- **소라카 (블루팀 중단)** : 일정 시간마다 아군 전체를 치유합니다.
- **트위스티드페이트 (블루팀 하단)** : 3초간 상대를 기절시키는 스킬을 사용합니다.
- **베인 (레드팀 상단)** : 상대 체력에 비례하는 추가 데미지를 입힙니다.
- **잔나 (레드팀 중단)** : 아군 전체 스킬 및 일반공격을 1회 막는 보호막 제공합니다.
- **니달리 (레드팀 하단)** : 거리비례데미지의 강력한 마법 계수를 가진 투사체를 발사합니다.
<img width="100%" src="https://user-images.githubusercontent.com/82865325/145038933-51cf60d3-4865-491b-90ba-bf9eda502dbf.png"> 

### 캐릭터별 렌더링 및 에니메이션 제작(블렌더이용)
<img width="100%" src="https://user-images.githubusercontent.com/82865325/146876945-26798eb4-53f9-4466-bec6-b4ffe0dbd21a.gif">

## 각 아이템 별 설명
<img width="80%" src="https://user-images.githubusercontent.com/82865325/145039171-4da12921-dba6-4da7-a970-48a82ca01268.png">
   
## 제작과정 
1. 버츄얼버튼(vb)가 클릭(위에카드가 올라감) 될시 에서 CardAttachGE.AttachCard(vb.name) 호출, 가장 최근에 인식된 마커 챔피언 카드를 attach (보드판의 자식으로 등록)
   - 버튼이 잘못 눌러지는것을 방지하기 위한 트리거 조건 추가(1초이상, 2개 가상 버튼 동시)
2. 케이틀린 챔피언 렌더링및 에니메이션 추출 완료
3. 캐릭터의 아이템 슬롯에 아이템이 장착되고, 자식으로 할당  -> https://www.youtube.com/watch?v=NX20udAyfS0
4. 챔피언마다 체력 Canvas 제작   
5. 챔피언 인식성 향상
6. 전투시작마커가 인식되면 게임 시작, 각 챔피언은 자신에게 가장 가까운 유닛을 바라봄
7. 라운드마다 패배시 해당 플레이어 체력 하락
8. 가장 가까운 적 챔피언 탐색, 바라보기
9. 가장 가까운 적에게 평타(유도탄) 발사
10. 스킬 이펙트(잔나, 소라카)
11. 일정 시간마다 스킬사용
12. HP Bar 추가
13. 아이템 추가
14. 전투 이펙트 일부 추가
15. 챔피언간 전투 -> https://youtu.be/FUtW8kX9Yog
16. 아이템 능력 및 챔피언 스킬 구현   


#### [최종 플레이 영상(Youtube)](https://youtu.be/ipIZVzVQ0Qk)
#### [코드설명 및 발표자료(PPT DownLoad)](https://github.com/Kim-JeongUng/AR_TFT_LolChess/raw/main/%ED%85%80%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%EA%B9%80%EC%A0%95%EC%9B%85%2C%EC%9D%B4%EC%8A%B9%EC%96%B8%2C%EC%9D%B4%ED%99%94%EC%A4%80.pptx)
