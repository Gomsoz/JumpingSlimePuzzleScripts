using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class BoardSetting : MonoBehaviour
{
    public GameObject cell;
    public GameObject block;

    public BlockBehavior blockBehavior;
    public StageInfo stageInfo;

    public Sprite[,] blockSprites;
    public GameObject[,] blocks;
    public Vector2[,] cellVector;
    public GameObject[,] cells;

    public int numOfBlockSprites;

    public int col;
    public int maxCol;//행
    public int row;
    public int maxRow;//렬

    int curCol;
    int curRow;

    bool isExpendBlock;

    private Transform cellHolder;
    private Transform blockHolder;

    public Vector2 boardBase;


    private void Awake()
    {
        BoardSettingInit();
    }

    private void Start()
    {
        //SearchSprite();
    }

    public void BoardSettingInit()
    {
        cellHolder = new GameObject("CellHolder").transform;
        blockHolder = new GameObject("BlockHolder").transform;
        boardBase = new Vector2(-7, -5);
        maxCol = 8;
        maxRow = 12;
        cellVector = new Vector2[maxCol + 2, maxRow + 2];
        cells = new GameObject[maxCol + 2, maxRow + 2];
        SetBoard(1);
    }

    public void SetBoard(int level)
    {
        StageInfo _stageInfo = StageReader.LoadStage(level);

        if (_stageInfo.col == curCol && _stageInfo.row == curRow)
        {
            isExpendBlock = true;
        }

        LoadBoardData(_stageInfo);
        SetBoardLevel(_stageInfo);
    }

    void LoadBoardData(StageInfo stageInfo)
    {
        curCol = stageInfo.col;
        curRow = stageInfo.row;
        numOfBlockSprites = stageInfo.numOfBlocks;
    }

    public void LevelSetting()
    {

        if (!GameManager.isGameStart)
        {
            blockSprites = new Sprite[maxCol + 2, maxRow + 2];
            blocks = new GameObject[maxCol + 2, maxRow + 2];
            SetBoard();
        }
        else if (GameManager.isExpendBlock)
        {
            CheckNullBlock();
        }

        MakeSetCell();
    }

    public void SetBoard()
    {
        MakeCell();
        MakeBlock();   
    }

    public void SetBoardLevel(StageInfo stageInfo)
    {
        col = stageInfo.col;
        row = stageInfo.row;
    }

    public void MakeCell()
    {
        Vector2 cellPos = boardBase;
        for (int m_col = 1; m_col <= maxCol; m_col++)
        {
            for (int m_row = 1; m_row <= maxRow; m_row++)
            {
                cellPos = new Vector2(m_row * cell.transform.localScale.x, m_col * cell.transform.localScale.y) + boardBase;
                GameObject instance = Instantiate(cell, cellPos, Quaternion.identity);// cell 생성
                cells[m_col, m_row] = instance;
                cellVector[m_col, m_row] = cellPos;
                instance.transform.SetParent(cellHolder);
            }
        }
    }

    public void MakeSetCell()
    {
        if (!GameManager.isGameStart)
        {
            for (int mCol = col + 1; mCol <= maxCol; mCol++)
            {
                for (int mRow = 1; mRow <= maxRow; mRow++)
                {
                    cells[mCol, mRow].SetActive(false);
                }
            }

            for (int mCol = 1; mCol <= col; mCol++)
            {
                for (int mRow = row + 1; mRow <= maxRow; mRow++)
                {
                    cells[mCol, mRow].SetActive(false);
                }
            }
        }
        else if(GameManager.isGameStart)
        {
            for (int mCol = 1; mCol <= col; mCol++)
            {
                for (int mRow = 1; mRow <= row; mRow++)
                {
                    cells[mCol, mRow].SetActive(true);
                }
            }
        }
    }

    public void MakeBlock()
    {
        Debug.Log("The Board Setting... : Set Blocks");
        Vector2 blockPos = cellVector[0,0];
        for (int m_col = 1; m_col <= col; m_col++)
        {
            for (int m_row = 1; m_row <= row; m_row++)
            {
                NewBlock(m_col, m_row);
            }
        }
    }

    public void SetBlockPos(int m_col, int m_row)
    {
        //Debug.Log(m_row + "," + m_col);
        int targetCol = 1;

        if (blocks[m_col + targetCol, m_row] == null)
        {
            bool isChange = false;
            for (int isNull = targetCol + 1; isNull <= col - m_col; isNull++)
            {
                if (blocks[m_col + isNull, m_row] != null)
                {
                    targetCol = isNull;
                    ChangeBlock(m_col, m_row, targetCol);
                    isChange = true;
                    break;
                }
            }
            if (!isChange)
                NewBlock(m_col, m_row);
        }
        else
            ChangeBlock(m_col, m_row, targetCol);
        

        if (++m_col < col)
            SetBlockPos(m_col, m_row);
        else if(m_col == col)
            NewBlock(m_col, m_row);

        return;
    }

    public void ChangeBlock(int mCol, int mRow, int targetCol)
    {
        Debug.Log("Changing Block... : From[" + (mCol + targetCol) + "," + mRow + "] to[" + mCol + "," + mRow +"]");
        BlockBehavior blockBehavior;

        blocks[mCol + targetCol, mRow].transform.position = cellVector[mCol, mRow];
        blocks[mCol, mRow] = blocks[mCol + targetCol, mRow];
        blockSprites[mCol, mRow] = blockSprites[mCol + targetCol, mRow];

        blocks[mCol + targetCol, mRow] = null;
        blockSprites[mCol + targetCol, mRow] = null;

        blockBehavior = blocks[mCol, mRow].GetComponent<BlockBehavior>();
        blockBehavior.myCol = mCol;
    }

    public void NewBlock(int m_col,int m_row)
    {
        Vector2 blockPos = cellVector[m_col, m_row];
        GameObject instance = Instantiate(block, blockPos, Quaternion.identity);

        instance.transform.SetParent(blockHolder);// block 생성
        blockBehavior = instance.GetComponent<BlockBehavior>();
        blockBehavior.SetColor(numOfBlockSprites);
        blockSprites[m_col, m_row] = blockBehavior.GetSprite();
        blockBehavior.myCol = m_col;
        blockBehavior.myRow = m_row;
        blocks[m_col, m_row] = instance;
    }

    public void CheckNullBlock()
    {
        for(int mCol = 1; mCol <= col; mCol++)
        {
            for(int mRow = 1; mRow <= row; mRow++)
            {
                if (blockSprites[mCol, mRow] == null)
                {
                    NewBlock(mCol, mRow);
                }
            }
        }
    }
}
