using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{

    public GameObject Container;
    private GameObject background;

    public GameObject Dirt;
    public GameObject Grass;
    public GameObject Stone;
    public GameObject Bedrock;
    public GameObject CorkOre;
    public GameObject AntidoteOre;
    public GameObject Trap;
    public GameObject Cancer;   

    public int MineEntryX = 0;

    public int HorizontalSize = 100;
    public int VerticalSize = 35;

    public int CorkAmount = 40;
    public int AntidoteAmount = 150;
    public int TrapAmount = 50;
    public int CancerAmount = 20;

    public bool DebugBlocks = false;

    private GameObject[,] BlockList; 

    private void Start()
    {
        BlockList = new GameObject[HorizontalSize, VerticalSize];
        background = Container.transform.GetChild(0).gameObject;
        background.transform.position = new Vector3(-0.5f, -VerticalSize / 2 + 0.5f, 3);


        GenerateStone();
        GenerateDirt();
        GenerateCancer();
        GenerateOres();
        GenerateBedrock();
        BreakMineEntry();
    }

    private void BreakMineEntry()
    {
        Destroy(BlockList[MineEntryX, 0]);
        Destroy(BlockList[MineEntryX + 1, 0]);
    }

    private void GenerateStone()
    {
        for (int x = 0; x < HorizontalSize; x++)
        {
            for (int y = 0; y < VerticalSize; y++)
            {
                SetBlock(x, y, Instantiate(Stone));
            }
        }
    }

    private void GenerateDirt()
    {
        for (int x = 0; x < HorizontalSize; x++)
        {
            SetBlock(x, 0, Instantiate(Grass));
            BlockList[x, 0].GetComponent<Block>().Breakable = false;

            SetBlock(x, 1, Instantiate(Dirt));

            if (Random.Range(0, 10) >= 6)
            {
                SetBlock(x, 2, Instantiate(Dirt));
            }
        }
    }

    private void GenerateOres()
    {
        for (int i = 0; i < AntidoteAmount; i++)
        {
            int x = Random.Range(2, HorizontalSize - 3);
            int y = Random.Range(4, VerticalSize - 1);
            SetBlock(x, y, Instantiate(AntidoteOre));
        }

        for (int i = 0; i < CorkAmount; i++)
        {
            int x = Random.Range(2, HorizontalSize - 3);
            int y = Random.Range(8, VerticalSize - 4);
            SetBlock(x, y, Instantiate(CorkOre));
        }
    }

    private List<bool[]> cancerBlockMaps = new()
    {
        new bool[] {false, true, true, true, true, true, true, true, false},
        new bool[] {true, false, false, true, true, false, false, false, false },
        new bool[] {true, true, false, true, true, false, false, false, false }
    };

    private void GenerateCancer()
    {
        for (int i = 0; i < TrapAmount; i++)
        {
            int x = Random.Range(2, HorizontalSize - 3);
            int y = Random.Range(4, VerticalSize - 1);
            SetBlock(x, y, Instantiate(Trap));
        }

        for (int i = 0; i < CancerAmount; i++)
        {
            int bx = Random.Range(2, HorizontalSize - 3);
            int by = Random.Range(6, VerticalSize - 1);


            bool[] map = cancerBlockMaps[Random.Range(0, cancerBlockMaps.Count - 1)];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    bool block = map[x + 3 * y];
                    if (block)
                    {
                        SetBlock(bx + x, by + y, Instantiate(Cancer));
                    }
                }
            }
        }
    }

    private void GenerateBedrock()
    {
        for (int x = 0; x < HorizontalSize; x++)
        {
            // Bottom layer
            SetBlock(x, VerticalSize - 1, Instantiate(Bedrock));

            float random = Random.Range(0, 10);
            if (random >= 4)
            {
                SetBlock(x, VerticalSize - 2, Instantiate(Bedrock));
                if (random >= 7.5)
                {
                    SetBlock(x, VerticalSize - 3, Instantiate(Bedrock));
                }
            }
        }

        for (int y = 0; y < VerticalSize; y++)
        {
            SetBlock(0, y, Instantiate(Bedrock));
            SetBlock(HorizontalSize - 1, y, Instantiate(Bedrock));

            if (Random.Range(0, 10) >= 5)
            {
                SetBlock(1, y, Instantiate(Bedrock));
            }

            if (Random.Range(0, 10) >= 5)
            {
                SetBlock(HorizontalSize - 2, y, Instantiate(Bedrock));
            }
        }
    }

    public void MineBlock(int x, int y)
    {
        x -= (int)Container.transform.position.x - HorizontalSize / 2;
        y -= (int)Container.transform.position.y;
        y = -y;

        if (x <= 0 || y <= 0 || x > HorizontalSize || y > VerticalSize) return;

        GameObject blockObject = BlockList[x, y];
        if (blockObject == null) return;

        Block block = blockObject.GetComponent<Block>();
        
        if (block.Mine())
        {
            Tuple<int, int>[] adjacents = {
                new( 1,  0),
                new(-1,  0),
                new( 0,  1),
                new( 0, -1)
            };

            foreach (Tuple<int, int> adjacent in adjacents)
            {
                GameObject adjacentObject = BlockList[x + adjacent.Item1, y + adjacent.Item2];
                if (adjacentObject == null) continue;

                adjacentObject.GetComponent<Block>().Reveal();
            }
        }
    }

    public GameObject GetBlock(int x, int y)
    {
        x -= (int)Container.transform.position.x - HorizontalSize / 2;
        y -= (int)Container.transform.position.y;
        y = -y;

        if (x < 0 || y < 0 || x > HorizontalSize || y > VerticalSize) return null;

        return BlockList[x, y];
    }

    private void SetBlock(int x, int y, GameObject block)
    {
        if (x < 0 || x >= HorizontalSize || y < 0 || y >= VerticalSize) return;

        if (BlockList[x, y] != null)
        {
            Destroy(BlockList[x, y]);
        }

        block.name = $"Block[{x},{y}]";

        BlockList[x, y] = block;
        block.transform.SetParent(Container.transform, false);
        block.transform.position = new Vector3((-HorizontalSize / 2) + x, -y, -1);

        if (DebugBlocks) block.GetComponent<Block>().Reveal();
    }
}
