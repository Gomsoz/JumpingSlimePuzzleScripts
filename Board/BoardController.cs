using UnityEngine;

public class BoardController : MonoBehaviour
{
    public BoardSetting boardSet;
    public GameManager gameManager;
    public int tempScore;

    public enum IsBlockTrue
    {
        left = 1,
        up = 2,
        right = 4,
        down = 8
    };

    //public Sprite[,] boardSet.blockSprites;

    // Start is called before the first frame update
    void Start()
    {
        BoardControllerInit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BoardControllerInit()
    {
        //boardSet.blockSprites = new Sprite[boardSet.col, boardSet.row];

        //boardSet.blockSprites = boardSet.blockSprites;
    }

    public bool AttemptBlock(int m_col, int m_row)
    {
        // 블록이 매칭이 되는 지 체크한다.
        if (CheckBlock(m_col, m_row, true))
        {
            // 매칭에 성공하면 점수를 올리고, 블록을 정렬한다.
            gameManager.totalScore += tempScore;
            SortBlcok();
            return true;
        }
        else
            return false;

    }

    public bool CheckBlock(int m_col, int m_row, bool isFirst)
    {
        // 해당 좌표의 sprite 를 가져온다.
        Sprite target = boardSet.blockSprites[m_col, m_row];

        // 검사하는 부분이 처음이라면 
        if (isFirst)
        {
            if (boardSet.blockSprites[m_col - 1, m_row] != target && boardSet.blockSprites[m_col + 1, m_row] != target &&
                boardSet.blockSprites[m_col, m_row + 1] != target && boardSet.blockSprites[m_col, m_row - 1] != target)
            {
                return false;
            }
        }

        ClearBlock(m_col, m_row);

        // 처음이 아니라면 재귀함수를 이용하여 검사한다.
        if (boardSet.blockSprites[m_col - 1, m_row] == target || boardSet.blockSprites[m_col + 1, m_row] == target ||
            boardSet.blockSprites[m_col, m_row + 1] == target || boardSet.blockSprites[m_col, m_row - 1] == target)
        {
            if (boardSet.blockSprites[m_col - 1, m_row] != null && boardSet.blockSprites[m_col - 1, m_row] == target)
            {
                CheckBlock(m_col - 1, m_row, false);
            }
            if (boardSet.blockSprites[m_col + 1, m_row] != null && boardSet.blockSprites[m_col + 1, m_row] == target)
            {
                CheckBlock(m_col + 1, m_row, false);
            }
            if (boardSet.blockSprites[m_col, m_row + 1] != null && boardSet.blockSprites[m_col, m_row + 1] == target)
            {
                CheckBlock(m_col, m_row + 1, false);
            }
            if (boardSet.blockSprites[m_col, m_row - 1] != null && boardSet.blockSprites[m_col, m_row - 1] == target)
            {
                CheckBlock(m_col, m_row - 1, false);
            }
        }
        else
            return false;

        return true;
    }

    public void ClearBlock(int m_col, int m_row)
    {
        // 이펙트를 내보내고, 점수를 올린다.
        BlockBehavior BlockBehavior = boardSet.blocks[m_col, m_row].GetComponent<BlockBehavior>();
        BlockBehavior.OnBlockEffect();
        tempScore = tempScore + (10 * GameManager.Combo.comboBonus);
        GameObject tempBlock = boardSet.blocks[m_col, m_row];

        // 제거된 블록을 블록 배열과 sprite 배열에서 제거하고, 블록을 파괴한다.
        boardSet.blockSprites[m_col, m_row] = null;
        boardSet.blocks[m_col, m_row] = null;
        Destroy(tempBlock);
    }

    public void SortBlcok()
    {
        // 1,1 부터 차례로 순회한다.
        for(int m_col = 1; m_col <= boardSet.col; m_col++)
        {
            for (int m_row = 1; m_row <= boardSet.row; m_row++)
            {
                // 블록이 제거된 부분일 경우 블록의 포지션을 다시 설정한다.
                if (boardSet.blocks[m_col, m_row] == null)
                {
                    boardSet.SetBlockPos(m_col, m_row);
                }
            }
        }   
    }
}
