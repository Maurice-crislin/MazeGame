
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class MazeController : MonoBehaviour
{

	public Tilemap tilemap;//引用的Tilemap
	public Tile baseTile;//使用的最基本的Tile，我这里是白色块，然后根据数据设置不同颜色生成不同Tile
	Tile[] arrTiles;//生成的Tile数组
	
	private int row = 11;
	private int column = 6;
	private int[,] LabId;//存放迷宫的数组
	private Vector2[,] Pos;

	//初始化一个地图  默认所有路不通
	//row行表示的是刚开始空白格子的行数，而格子之间还有墙壁，所以最终产生的二维数组大小实际为(2row+1) * (2colum+1)
	//看到其他博客有其他的思路也可以，可以凭自己爱好：例如 空间：O（row*col)，时间：O（墙数=（row-1）*col+row*（col - 1））


	public static MazeController Instance;

	public void Init()
	{
		tilemap.RefreshAllTiles();
		int r = 2 * row + 1, c = 2 * column + 1;
		LabId = new int[r, c];
		for (int i = 0; i < r; i++) //将所有格子都设为墙
			for (int j = 0; j < c; j++)
				LabId[i, j] = 0;//0 为墙 1为路

		//中间格子放为1
		for (int i = 0; i < row; i++)
			for (int j = 0; j < column; j++)
				LabId[2 * i + 1, 2 * j + 1] = 1;//0 为墙 1为路
		//普里姆算法
		accLabPrime();
		//深度优先搜索算法
		//accLabDFS();
		LabId[0, 11] = 1;
		LabId[r-1, 1] = 1;
		arrTiles = new Tile[1];
		
		arrTiles[0] = ScriptableObject.CreateInstance<Tile>();//创建Tile，注意，要使用这种方式
		arrTiles[0].sprite = baseTile.sprite;
		for (int i = 0; i < r; i++) {//不要边界
			for (int j = 0; j < c; j++){
				if (LabId[i, j] == 0)
				{//0 为墙 1为路
					tilemap.SetTile(new Vector3Int(i-12, j-7, 0), arrTiles[0]);
				}
			}	
		}
		
	}

	/// <summary>
	/// 通过Prim算法处理数组 生成最后的迷宫
	/// 思路: 随机找最近的点访问(每个点只访问一次,直到访问完所有的路),会生成一条访问所有点的路(无序),在随机找下一个点的时,把之前相邻的两个格子之间的墙壁打通
	/// </summary>
	public void accLabPrime()
	{
		//acc存放已访问队列，noacc存放没有访问队列
		int[] acc, noacc;
		int count = row * column;
		int accsize = 0;//记录访问过点的数量

		acc = new int[count];
		noacc = new int[count];

		//row上各方向的偏移  column各方向的偏移  0左 1右 3上 4下
		int[] offR = { -1, 1, 0, 0 };
		int[] offC = { 0, 0, 1, -1 };
		//四个方向的偏移 左右上下
		int[] offS = { -1, 1, row, -row };

		for (int i = 0; i < count; i++)
		{
			acc[i] = 0;
			noacc[i] = 0;
		}

		//起点
		acc[0] = Random.Range(0, count);
		int pos = acc[0];
		//第一个点存入
		noacc[pos] = 1;
		while (accsize < count)
		{
			//取出现在的点
			int x = pos % row;
			int y = pos / row;
			int offpos = -1;//用于记录偏移量
			int w = 0;
			//四个方向都尝试一遍 直到挖通为止
			while (++w < 5)
			{
				//随机访问最近的点
				int point = Random.Range(0, 4);  //包含min但不包含max。
				int repos;
				int move_x, move_y;
				//计算出移动方位
				repos = pos + offS[point];
				move_x = x + offR[point];
				move_y = y + offC[point];

				//判断移动是否合法
				if (move_y > -1 && move_x > -1 && move_x < row && move_y < column && repos >= 0 && repos < count && noacc[repos] != 1)
				{
					noacc[repos] = 1;
					acc[++accsize] = repos;
					pos = repos;
					offpos = point;
					//相邻的格子中间的位置放1 
					LabId[2 * x + 1 + offR[point], 2 * y + 1 + offC[point]] = 1;
					break;

				}
				else
				{
					if (accsize == count - 1)
						return;
					continue;
				}

			}

			if (offpos < 0)
			{//周边没有找到能走的路了   从走过的路里重新找个起点 
				pos = acc[Random.Range(0, accsize + 1)];

			}
		}

	}
	void Awake()
	{
		Instance = this;
	}
	void Start()
	{
		Instance = this;
		Init();
		
	}
    private void Update()
    {
		
		
	}


}


