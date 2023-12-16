using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Order
{
    List<string> comment;
    Cup coffee;
    public int count;
    public string name;

    public Order(string name)
    {
        comment = new List<string>();
        count = 0;
        this.name = name;
    }

    public void setComment(string str)
    {
        comment.Add(str);
        count++;
    }

    public void setCoffee(LiquidType liquidType, float temp, CupType cupType, float liquidAmount, float shotNum)
    {
        //coffee.setLiquid
    }

    public string getComment(int i)
    {
        return comment[i];
    }
}


public class OrderManager : MonoBehaviour
{
    [SerializeField] Button what_Btn;
    [SerializeField] Button oK_Btn;
    [SerializeField] TMP_Text comments;

    List<Order> orders;
    int commentNum = 0;

    private void Start()
    {
        orders = new List<Order>();
        setOrders();
    }

    public void nextComment()
    {
        commentNum++;
        if (commentNum < TycoonGameManager.instance.curOrder.count)
            comments.text = TycoonGameManager.instance.curOrder.getComment(commentNum);
        else
            what_Btn.gameObject.SetActive(false);
        
    }

    public void chooseOrder(int i)
    {
        TycoonGameManager.instance.curOrder = orders[i];
        commentNum = 0;
        comments.text = orders[i].getComment(commentNum);
    }

    private void setOrders()
    {
        Order _o = new Order("소심한 주부 코기");
        _o.setComment("안녕하세요.\n" +
                      "비도 오는데 따뜻한게\n땡기네요.\n" +
                      "따뜻한 아이스 아메리카노로 주시겠어요?");
        _o.setComment("아이쿠 실수했네요ㅎㅎ\n" +
                      "따뜻한 아메리카노로\n주세요.");
        _o.setComment("그냥 아메리카노로\n주시면 될 것 같아요\n" +
                      "더 물어볼 게 있나요?");
        _o.setComment("더 말할게 없어요...");
        _o.setComment("죄송해요.\n" +
                      "다음에 올게요.");

        orders.Add(_o);

        _o = new Order("설탕 중독 코기");
        _o.setComment("안녕\n" +
                      "난 단게 좋아.\n");
        _o.setComment("달달한거 뭐가 있지..\n" +
                      "아아 프라푸치노 먹고 싶어!");
        _o.setComment("아 자바칩도!!\n" +
                      "잊지마!");
        _o.setComment("당근 아이스지!\n" +
                      "얼어 죽어도 아이스야!");
        _o.setComment("아참, 그리고...\n");
        _o.setComment("더블 샷은 써서 못 먹어...");
        _o.setComment("또? 뭐가 있지...?");
        _o.setComment("그만 질문해");

        orders.Add(_o);

        _o = new Order("꽃집 코기");
        _o.setComment("처음 보는 카페다!\n" +
                      "카페라떼 하나 해주세요.\n" +
                      "우와~ 기대된다");
        _o.setComment("오늘 날씨가 꽤 덥네요..\n"+
                      "등이 땀으로 다 젖었어요..");
        _o.setComment("마지막으로 휘핑 크림도 추가할게요.");
        _o.setComment("죄송해요.\n" +
                      "다음에 올게요.");

        orders.Add(_o);


    }

}
