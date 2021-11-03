# AR_TFT_LolChess
AR기말 팀 프로젝트 롤토체스

### 구현중 현재 구현 률 0% 

## 구현 단계
0. 챔피언 카드 2장, 아이템카드 2장 제작(프린팅)
1. 챔피언 카드 2장으로 서로 AR 전투 (우선, 애니메이션 생략)
2. 아이템 카드 2장으로 붙여 뒀을 때 AR로 새로운 아이템 생성
3. 아이템, 챔피언 별 고유 능력 부여
4. 챔피언 카드 추가 (6~8)장
5. 아이템 카드 추가 (15~20)장 동일 카드도 있음
6. 게임 룰 추가 및 챔피언,아이템 밸런스 조정
7. 챔피언 전투 애니메이션 추가
8. 사운드 추가
9. 오류수정 및 추가 밸런스 조정
10. PPT 제작 

## 게임 룰 설명(임시)
1. 2인게임
2. 최초 챔피언 뭉치에서 카드 3장씩 뽑음( 챔피언 카드는 총 6장이상 중복 X) 
3. 각 턴때 레드팀, 블루팀으로 나뉘고 본인턴 때 아이템을 하나씩 드롭(아이템 카드뭉치에서 한장씩)한다. 그 후 턴 카드를 뒤집음(블루 -> 레드 -> 블루 앞 뒷면 마커)
4. 드롭한 아이템을 사용자는 아이템을 원하는 챔피언에 장착 시킬 수 있다.
5. 각 챔피언별 고유 능력치가 있고, ap, ad, 탱커로 나뉘어짐 ad캐릭에 ap아이템주면 효율 낮음
6. 아이템 2개는 조합해서 새로운 특별능력을 가진 아이템을 만들 수 있다. -> 대천사, 가엔, 등등 조합표는 주어진다.
7. 아이템 카드에서 아이템만 나오는게 아닌 HP회복, 챔피언 추가, 챔피언 변경, 아이템 자석(아이템제거기) 등 능력들도 나옴
8. 매 턴마다 사용자의 챔피언들은 전투하고, 패배시 사용자의 체력이 떨어짐(롤토체스와 동일)
9. 사용자의 체력이 0이 되면 게임이 끝남
