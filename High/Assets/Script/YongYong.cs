using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YongYong : MonoBehaviour
{
    public float stress;
    public Animator dragonAni;

    public GameObject egg;
    public GameObject dragon;
    public Light mainLight;

    public int hatchCount = 0;
    public float timer;
    float deathTimer;

    bool loudnessCheck = true;
    float patternTimer;
    float patternTime;

    public bool isTakeAWalk = false;
    bool isHungry = false;

    bool isAngry;

    float takeAWalkTimer = 0;
    float hungryTimer = 0;

    bool isDead = false;

    private Coroutine walkC;

    private void Start()
    {
        patternTime = Random.Range(10, 15.0f);
        StartCoroutine(UIManager.instance.ChangeTextBox("안녕 나는 대화 상자야! 5252, 용은 소리에 민감하니 주.의 하라구(쑻)"));
    }

    void Update()
    {
        if (isTakeAWalk)
            takeAWalkTimer += Time.deltaTime;
        if (isHungry)
            hungryTimer += Time.deltaTime;

        if(takeAWalkTimer > 8 && !isAngry)
        {
            isAngry = true;
            StartCoroutine(UIManager.instance.ChangeTextBox("빨리 걸어라 닝겐"));
        }

        if (hungryTimer > 4 && !isAngry)
        {
            isAngry = true;
            StartCoroutine(UIManager.instance.ChangeTextBox("왜 밥을 주지 않는 것? 드디어 미쳐버린?"));
        }

        if (isAngry)
            ChangeStress(10 * Time.deltaTime);

        if (dragonAni.GetBool("isDead"))
            return;

        timer += Time.deltaTime;
        if (timer > 7 * (hatchCount + 1) && hatchCount < 3)
        { Hatch(); timer = 0; }

        float loudness = MicInput.instance.level;

        if (loudness > 0.2f)
            ChangeStress(loudness * 8);

        if (loudness >= 0.9f && loudnessCheck)
        {
            StartCoroutine(UIManager.instance.ChangeTextBox("미쳤습니까 Korean 휴먼? 지금 시끄럽습니다. 매우"));
            loudnessCheck = false;
        }

        GetTouch();
        if (hatchCount < 3) Move();
        else
            patternTimer += Time.deltaTime;

        if (patternTime < patternTimer && !isTakeAWalk && !isHungry)
        {
            SetRandomState();
        }

        CheckDie();
    }

    void SetRandomState()
    {
        UIManager.instance.sayRandomText = false;

            StartCoroutine(UIManager.instance.ChangeTextBox("밥줘용 밥"));
            isHungry = true;

    }

    void CheckDie()
    {
        if (stress == 100)
            deathTimer += Time.deltaTime;
        else
            deathTimer = 0;

        if(deathTimer >= 0.2f)
        {
            Death();
        }
    }

    void Death()
    {
        if (!isDead)
        {
            Soundclip_Changer.hotsix = 5;

            float t = UIManager.instance.mainTimer;
            UIManager.instance.deadText.text = "지나친 스트레스로 인해 사망 " + Mathf.Round(UIManager.instance.mainTimer).ToString() + "초";

            isDead = true;
            dragonAni.SetBool("isDead", true);
            StartCoroutine(UIManager.instance.ViewImage(UIManager.instance.deathScene));

            UIManager.instance.sayRandomText = false;
            StartCoroutine(UIManager.instance.ChangeTextBox("당신은 하나의 생물을 죽였습니다."));
            if (walkC != null)
                StopCoroutine(walkC);
        }
    }

    public void FindFood(GameObject food)
    {
        StopCoroutine(walkC);
        StartCoroutine(FoodMove(food));
    }

    IEnumerator FoodMove(GameObject food)
    {
        dragon.transform.LookAt(food.transform);
        Jump(100);
        yield return new WaitForSeconds(0.5f);
        Jump(100);
        yield return new WaitForSeconds(0.8f);

        dragonAni.SetBool("isWalk", true);

        while (true)
        {
            float step = 2 * Time.deltaTime;
            dragon.transform.position = Vector3.MoveTowards(dragon.transform.position, food.transform.position, step);

            if (Vector3.Distance(dragon.transform.position, food.transform.position) < 0.8f)
            {
                Destroy(food);
                ChangeStress(-20);

                StartCoroutine(UIManager.instance.ChangeTextBox("충격, 실화) 사과를 먹는 용이 있다? 뿌슝뿌슝빠슝"));

                Soundclip_Changer.hotsix = 2;

                isAngry = false;
                if(isHungry)
                {
                    isHungry = false;
                    hungryTimer = 0;
                    patternTime = Random.Range(10, 15.0f);
                    patternTimer = 0;
                    UIManager.instance.sayRandomText = true;
                    UIManager.instance.isFeed = false;
                }

                break;
            }

            yield return null;
        }

        dragonAni.SetBool("isWalk", false);

        yield return new WaitForSeconds(0.5f);
        Jump(100);
        yield return new WaitForSeconds(0.5f);
        Jump(100);
        yield return new WaitForSeconds(1.5f);
        walkC = StartCoroutine(DragonMove());
    }

    void Move()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.z = Input.acceleration.y;

        if (Mathf.Abs(dir.x) + Mathf.Abs(dir.y) >= 1f)
            ChangeStress(1);

        if (dir.sqrMagnitude > 1)
            dir.Normalize();
            
        dir *= Time.deltaTime;

        transform.Translate(dir * 2, Space.World);
        transform.Rotate(dir * 2 * 10, Space.World);
    }

    Vector3 goalPos;

    IEnumerator DragonMove()
    {
        while (true)
        {
            float walkTimer = 0;
            int walkTime = Random.Range(1, 3);

            dragonAni.SetBool("isWalk", true);
            goalPos = dragon.transform.position;
            goalPos += new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-3.0f, 3.0f));

            while (true)
            {
                if (dragonAni.GetBool("isDead"))
                    StopCoroutine(walkC);

                dragon.transform.LookAt(goalPos);

                float step = 1 * Time.deltaTime;
                dragon.transform.position = Vector3.MoveTowards(dragon.transform.position, goalPos, step);

                walkTimer += Time.deltaTime;

                if (Vector3.Distance(dragon.transform.position, goalPos) < 0.001f || walkTimer > walkTime)
                {
                    break;
                }
                yield return null;
            }

            dragonAni.SetBool("isWalk", false);
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
        }
    }

    void GetTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchedPos = new Vector3(0, 0, 0);

            if (touch.phase == TouchPhase.Began)
            {
                touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                //eggAni.SetBool("isSweeping", true);
            }
            if (touch.phase == TouchPhase.Moved)
            { 
                Vector3 movedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                float distance = Vector3.Distance(touchedPos, movedPos);

                ChangeStress(-distance);

                if (Random.Range(0, 200) == 0)
                    HeartCanvas.instance.MakeHeart();
            }

            //if (touch.phase == TouchPhase.Ended)
              //  eggAni.SetBool("isSweeping", false);
        }
    }

    public void Jump(int power)
    {
        var rig = transform.GetChild(0).GetComponent<Rigidbody>();
        var rig2 = transform.GetChild(1).GetComponent<Rigidbody>();
        rig.AddForce(Vector3.up * power);
        rig2.AddForce(Vector3.up * power);
    }

    void ChangeStress(float amount)
    {
        if (stress + amount > 100 && stress < 100)
            Soundclip_Changer.hotsix = 1;

        if (stress + amount > 100)
            stress = 100;
        else if (stress + amount < 0)
            stress = 0;
        else
            stress += amount;
    }

    public void Hatch()
    {
        hatchCount++;
        print(hatchCount);

        if (hatchCount < 3)
        {
            if(hatchCount == 1)
                StartCoroutine(UIManager.instance.ChangeTextBox("알의 공중날기! 효과가 없었다."));
            else if(hatchCount == 2)
                StartCoroutine(UIManager.instance.ChangeTextBox("읏 나와 나온다아앗!! (용이 알에서)"));
            Jump(200);
        }
        if (hatchCount == 3)
            StartCoroutine(Evolution());
    }

    IEnumerator Evolution()
    {
        egg.transform.position = new Vector3(0, 0, -7.12f);
        egg.transform.eulerAngles = new Vector3(-90, 0, 0);

        Jump(230);
        for (int i = 0; i < 20; i++)
        {
            mainLight.intensity += 0.1f;
            yield return null;
        }

        egg.GetComponent<Rigidbody>().isKinematic = true;
        dragon.GetComponent<Rigidbody>().isKinematic = true;
        egg.GetComponent<MeshCollider>().isTrigger = true;
        dragon.GetComponent<BoxCollider>().isTrigger = true;

        dragon.transform.localScale = new Vector3(0, 0, 0);
        dragon.transform.position = egg.transform.position;
        dragon.SetActive(true);

        StartCoroutine(ChangeScale(15));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(ChangeScale(10));
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(ChangeScale(5));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(ChangeScale(5));
        yield return new WaitForSeconds(0.4f);
        StartCoroutine(ChangeScale(10));
        yield return new WaitForSeconds(0.6f);
        StartCoroutine(ChangeScale(15));
        yield return new WaitForSeconds(0.8f);

        for (int i = 1; i <= 15; i++)
        {
            float persent = i / 15;

            egg.transform.localScale = Vector3.Lerp(egg.transform.localScale, new Vector3(0, 0, 0), persent);
            dragon.transform.localScale = Vector3.Lerp(dragon.transform.localScale, dragonScale, persent);
            yield return null;
        }

        for (int i = 0; i < 20; i++)
        {
            mainLight.intensity -= 0.1f;
            yield return null;
        }

        dragon.GetComponent<Rigidbody>().isKinematic = false;
        dragon.GetComponent<BoxCollider>().isTrigger = false;

        StartCoroutine(UIManager.instance.ChangeTextBox("하와와 용고생쟝 알에서 깨어난 거시와요,,!"));

        UIManager.instance.feedBtn.SetActive(true);

        yield return new WaitForSeconds(1f);

        Jump(100);
        yield return new WaitForSeconds(0.5f);
        Jump(100);
        yield return new WaitForSeconds(1.0f);

        walkC = StartCoroutine(DragonMove());
    }

    Vector3 eggScale = new Vector3(1, 1, 1);
    Vector3 dragonScale = new Vector3(0.4f, 0.4f, 0.4f);

    IEnumerator ChangeScale(float count)
    {
        for(int i = 1; i <= count; i++)
        {
            float persent = i / count;

            egg.transform.localScale = Vector3.Lerp(egg.transform.localScale, new Vector3(0, 0, 0), persent);
            dragon.transform.localScale = Vector3.Lerp(dragon.transform.localScale, dragonScale, persent);
            yield return null;
        }
        for (int i = 1; i <= count; i++)
        {
            float persent = i / count;

            egg.transform.localScale = Vector3.Lerp(egg.transform.localScale, new Vector3(1, 1, 1), persent);
            dragon.transform.localScale = Vector3.Lerp(dragon.transform.localScale, new Vector3(0, 0, 0), persent);
            yield return null;
        }
    }
}
