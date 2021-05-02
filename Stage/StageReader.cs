using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

public static class StageReader
{  
    public static void SaveResult(GameResult gameResult)
    {
        JObject saveData = new JObject();

        saveData["score"] = gameResult.score;
        saveData["maxCombo"] = gameResult.maxCombo;

        // 파일로 저장 
        string savestring = JsonConvert.SerializeObject(saveData, Formatting.Indented); 
        // JObject를 Serialize하여 json string 생성 
        File.WriteAllText("Assets/Resources/Stage/gameResult.json", savestring); // 생성된 string을 파일에 쓴다 }

        //출처: https://blog.komastar.kr/232 [World of Komastar]
    }

    public static GameResult LoadResult()
    {
        GameResult gameResult = new GameResult();

        string loadString = File.ReadAllText("Assets/Resources/Stage/GameResult.json");
        JObject loadData = JObject.Parse(loadString);

        gameResult.score = (int)loadData["score"];
        gameResult.maxCombo = (int)loadData["maxCombo"];

        return gameResult;
    }
    public static StageInfo LoadStage(int nStage)
    {
        Debug.Log("Load Data... : Start Load the Data");
        string loadString = File.ReadAllText("Assets/Resources/Stage/stageInfo.json");
        JObject loaddata = JObject.Parse(loadString); // JObject 파싱 
        // key 값으로 데이터 접근하여 적절히 사용 
        //Debug.Log("key-value 개수 : " + loaddata.Count); 
        //Debug.Log("----------------------------"); 
        //Debug.Log(loaddata["stage"+nStage]); 
        //Debug.Log("----------------------------");

        StageInfo stageInfo = new StageInfo();
        stageInfo.level = (int)loaddata["stage" + nStage]["level"];
        stageInfo.col = (int)loaddata["stage" + nStage]["col"];
        stageInfo.row = (int)loaddata["stage" + nStage]["row"];
        stageInfo.numOfBlocks = (int)loaddata["stage" + nStage]["numOfBlocks"];
        stageInfo.goalScore = (int)loaddata["stage" + nStage]["goalScore"];

        foreach(int item in loaddata["stage" + nStage]["rank"])
        {
            Debug.Log(item);
            stageInfo.rankList.Add(item);
        }

        Debug.Log("Load Data... : Finish Load the Data");
        return stageInfo;

        //출처: https://blog.komastar.kr/232 [World of Komastar]
    }
}
