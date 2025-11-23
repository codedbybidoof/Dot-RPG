using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; // 퀘스트 대화순서 변수 생성
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList; // 퀘스트 아이디, 퀘스트 데이터

    public static QuestManager instance = null;
    void Awake()
    {
        if (instance == null)
            instance = this;
        questList = new Dictionary<int, QuestData>();
        GenerateData();

    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 대화하기.", new int[] { 1000, 2000 })); //10 : 퀘스트 아이디, 해당 퀘스트에 연관된 npc 아이디
        questList.Add(20, new QuestData("루도의 동전 찾아주기.", new int[] { 5000, 2000 }));
        questList.Add(30, new QuestData("퀘스트 올 클리어!", new int[] { 0 })); //오류 방지용 더미데이터
    }

    public int GetQuestTalkIndex(int id) // NPC Id를 받음
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // Next Talk Target
        if (id == questList[questId].npcId[questActionIndex]) // 순서에 맞게 대화했을 때만 퀘스트 대화순서를 올림
            questActionIndex++;

        //Control Quest Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length) //퀘스트 대화순서가 끝에 도달했을 때 퀘스트번호 증가
            NextQuest();

        //Quest Name
        return questList[questId].questName;
    }

    public string CheckQuest() // 메서드 오버로딩 : 매개변수에 따라 함수 호출
    {
        //Quest Name
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void ControlObject() //퀘스트 오브젝트를 관리할 함수 생성
    {
        switch (questId)
        {
            case 10:
                if(questActionIndex==2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if(questActionIndex==1)
                    questObject[0].SetActive(false);
                break;
        }
    }

}
