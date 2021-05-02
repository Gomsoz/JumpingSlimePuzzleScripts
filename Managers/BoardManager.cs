using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    BoardSetting _boardSetting;

    int curCol;
    int curRow;
    int maxCol;
    int maxRow;
    int level = 1;
    int numOfBlockSprites;

    bool isExpendBlock;

    public void SetBoard(int level)
    {
        StageInfo _stageInfo = StageReader.LoadStage(level);

        if (_stageInfo.col == curCol && _stageInfo.row == curRow)
        {
            isExpendBlock = true;
        }

        LoadBoardData(_stageInfo);
    }
    void LoadBoardData(StageInfo stageInfo)
    {
        curCol = stageInfo.col;
        curRow = stageInfo.row;
        numOfBlockSprites = stageInfo.numOfBlocks;
    }
}
