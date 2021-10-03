
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class MazeController : MonoBehaviour
{

	public Tilemap tilemap;//���õ�Tilemap
	public Tile baseTile;//ʹ�õ��������Tile���������ǰ�ɫ�飬Ȼ������������ò�ͬ��ɫ���ɲ�ͬTile
	Tile[] arrTiles;//���ɵ�Tile����
	
	private int row = 11;
	private int column = 6;
	private int[,] LabId;//����Թ�������
	private Vector2[,] Pos;

	//��ʼ��һ����ͼ  Ĭ������·��ͨ
	//row�б�ʾ���Ǹտ�ʼ�հ׸��ӵ�������������֮�仹��ǽ�ڣ��������ղ����Ķ�ά�����Сʵ��Ϊ(2row+1) * (2colum+1)
	//��������������������˼·Ҳ���ԣ�����ƾ�Լ����ã����� �ռ䣺O��row*col)��ʱ�䣺O��ǽ��=��row-1��*col+row*��col - 1����


	public static MazeController Instance;

	public void Init()
	{
		tilemap.RefreshAllTiles();
		int r = 2 * row + 1, c = 2 * column + 1;
		LabId = new int[r, c];
		for (int i = 0; i < r; i++) //�����и��Ӷ���Ϊǽ
			for (int j = 0; j < c; j++)
				LabId[i, j] = 0;//0 Ϊǽ 1Ϊ·

		//�м���ӷ�Ϊ1
		for (int i = 0; i < row; i++)
			for (int j = 0; j < column; j++)
				LabId[2 * i + 1, 2 * j + 1] = 1;//0 Ϊǽ 1Ϊ·
		//����ķ�㷨
		accLabPrime();
		//������������㷨
		//accLabDFS();
		LabId[0, 11] = 1;
		LabId[r-1, 1] = 1;
		arrTiles = new Tile[1];
		
		arrTiles[0] = ScriptableObject.CreateInstance<Tile>();//����Tile��ע�⣬Ҫʹ�����ַ�ʽ
		arrTiles[0].sprite = baseTile.sprite;
		for (int i = 0; i < r; i++) {//��Ҫ�߽�
			for (int j = 0; j < c; j++){
				if (LabId[i, j] == 0)
				{//0 Ϊǽ 1Ϊ·
					tilemap.SetTile(new Vector3Int(i-12, j-7, 0), arrTiles[0]);
				}
			}	
		}
		
	}

	/// <summary>
	/// ͨ��Prim�㷨�������� ���������Թ�
	/// ˼·: ���������ĵ����(ÿ����ֻ����һ��,ֱ�����������е�·),������һ���������е��·(����),���������һ�����ʱ,��֮ǰ���ڵ���������֮���ǽ�ڴ�ͨ
	/// </summary>
	public void accLabPrime()
	{
		//acc����ѷ��ʶ��У�noacc���û�з��ʶ���
		int[] acc, noacc;
		int count = row * column;
		int accsize = 0;//��¼���ʹ��������

		acc = new int[count];
		noacc = new int[count];

		//row�ϸ������ƫ��  column�������ƫ��  0�� 1�� 3�� 4��
		int[] offR = { -1, 1, 0, 0 };
		int[] offC = { 0, 0, 1, -1 };
		//�ĸ������ƫ�� ��������
		int[] offS = { -1, 1, row, -row };

		for (int i = 0; i < count; i++)
		{
			acc[i] = 0;
			noacc[i] = 0;
		}

		//���
		acc[0] = Random.Range(0, count);
		int pos = acc[0];
		//��һ�������
		noacc[pos] = 1;
		while (accsize < count)
		{
			//ȡ�����ڵĵ�
			int x = pos % row;
			int y = pos / row;
			int offpos = -1;//���ڼ�¼ƫ����
			int w = 0;
			//�ĸ����򶼳���һ�� ֱ����ͨΪֹ
			while (++w < 5)
			{
				//�����������ĵ�
				int point = Random.Range(0, 4);  //����min��������max��
				int repos;
				int move_x, move_y;
				//������ƶ���λ
				repos = pos + offS[point];
				move_x = x + offR[point];
				move_y = y + offC[point];

				//�ж��ƶ��Ƿ�Ϸ�
				if (move_y > -1 && move_x > -1 && move_x < row && move_y < column && repos >= 0 && repos < count && noacc[repos] != 1)
				{
					noacc[repos] = 1;
					acc[++accsize] = repos;
					pos = repos;
					offpos = point;
					//���ڵĸ����м��λ�÷�1 
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
			{//�ܱ�û���ҵ����ߵ�·��   ���߹���·�������Ҹ���� 
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


