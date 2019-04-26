using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public YongYong yong;
    public CanvasGroup deathScene;
    public CanvasGroup jogingGague;
    float timer;

    private void Awake()
    {
        instance = this;
        yong = FindObjectOfType<YongYong>();
    }

    public float mainTimer;

    public Image warningImg;
    public Image deadImg;
    public Text boxText;
    public Text timeText;
    public GameObject apple;
    public GameObject feedBtn;

    public bool sayRandomText = true;
    public bool isFeed = false;

    string[] randomText = new string[] 
    { "스트레스는 쓰다듬어서 낮출 수 있다.",
        "빌게이츠가 노래를 어떻게 부를까?  마이크로 소프트하게 엌ㅋㅋㅋㅋㅋㅋㅋㅋㅋ",
        "새우가 주인공인 드라마는? 대하 드라마 엌ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ",
        "수박 한통에 오천 원인데, 두통엔? 게보린",
        "대머리가 사랑에 빠지면 위험한 이유가 뭔지 아세요? 헤어(Hair) 나올 수가 없어서 ㅠㅠㅠ",
        "대머리는 어떻게 걷지? 두발이 없는데",
        "논리적인 사람이 총을 쏘면? 타당타당!",
        "몸에 해로운 청바지는? 유해진",
        "베를린 갔을 때 남이 주는거 아무거나 먹지마 독일수도",
        "문어가 돈을 안갚는 이유는? 연체동물이라서",
        "횐님덜 플레이 해주셔서 감사합니다,,,울 횐님들을 위해,,행복 가득,,꿀잼~드립~~을 준비했읍니다,,,!! 오늘두 존 하루~~*^^",
        "신하가 왕에게 공을 던지면? 송구하옵니다!",
        "자가용의 반대말은? 커용",
        "뽑으면 우는 식물은? 우엉",
        "칼이 정색하면? 검정색",
        "반성문을 영어로 하면? 글로벌",
        "세상에서 가장 억울한 도형은? 원통",
        "'미소'의 반대말은? 당기소",
        "세상에서 가장 장사를 잘 하는 동물은? 판다",
        "빵이 시골로 간 까닭은? 소보로",
        "처음 만나는 소가 하는 말은? 반갑소",
        "과소비가 심한 동물은? 사자",
        "마녀가 높이 올라가면? 위치 에너지가 증가함",
        "사슴의 눈이 좋으면? 굿 아이디어",
        "용을 오래 키우면 선물을 준데용!",
        "절.대.태.보.해"};

    public void Feed()
    {
        if(GameManager.instance.heartCount >= 5 && !isFeed)
        {
            GameManager.instance.heartCount -= 5;
            var temp = Instantiate(apple, new Vector3(0, 0.15f, -8.47f), Quaternion.identity);
            yong.FindFood(temp);
            isFeed = true;
        }
    }

    public Text deadText;

    private void Update()
    {
        mainTimer += Time.deltaTime;

        timeText.text = Mathf.Round(mainTimer).ToString() + "초";

        if (yong.stress >= 50)
        {
            Color c = warningImg.color;
            c.a = (yong.stress - 50) / 50;
            warningImg.color = c;
        }
        else
        {
            Color c = warningImg.color;
            c.a = 0;
            warningImg.color = c;
        }
        timer += Time.deltaTime;

        if (timer > 6 && sayRandomText)
        {
            StartCoroutine(ChangeTextBox(randomText[Random.Range(0, randomText.Length)]));
            timer = 0;
        }

        if(deathScene.alpha >= 1 && Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("MainScene");
        }

    }

    public IEnumerator ChangeTextBox(string str)
    {
        timer = 0;

        for (int i = 0; i <= str.Length; i++)
        {
            boxText.text = str.Substring(0, i);
            yield return new WaitForSeconds(0.07f);
        }
    }

    public IEnumerator ViewImage(CanvasGroup image)
    {
        for (int i = 1; i <= 100; i++)
        {
            float c = image.alpha;
            c = i / 100.0f;
            image.alpha = c;
            yield return null;
        }
    }
}
