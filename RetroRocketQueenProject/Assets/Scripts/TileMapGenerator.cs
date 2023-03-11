using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    [Range(1, 100)]
    public int mineralChance;

    public int maxGroundLevel;
    public int maxRockLevel;
    public int minRockLevel;
    public int groundLevel;
    public int rockLevel;

    public int distanceGenerate;
    public int distanceDestroy;

    public Transform playerTransform;
    public GameObject player;

    public Tilemap groundTilemap;
    public Tilemap rockTilemap;
    public Tilemap mineralTilemap;
    public Tilemap grassTilemap;
    
    public RuleTile groundRuleTile;
    public RuleTile rockRuleTile;
    public AnimatedTile mineralAnimatedTile;
    public RandomTile buriedMineralRandomTile;
    public RandomTile grassRandomTile;
    
    
    public int mountainsLevel;
    public int maxMountainsLevel;
    public int minMountainsLevel;
    public Tilemap mountainsTilemap;
    public RuleTile mountainsRuleTile;

    public int nearMountainsLevel;
    public int maxNearMountainsLevel;
    public int minNearMountainsLevel;
    public Tilemap nearMountainsTilemap;
    public RuleTile nearMountainsRuleTile;

    public int farMountainsLevel;
    public int maxFarMountainsLevel;
    public int minFarMountainsLevel;
    public Tilemap farMountainsTilemap;
    public RuleTile farMountainsRuleTile;

    public int cloudsHeigh;
    public Tilemap cloudsTilemap;
    public RandomTile cloudsRandomTile;

    public Tilemap starsTilemap;
    public RuleTile starsRuleTile;
    public AnimatedTile smallStarAnimatedTile;
    public AnimatedTile bigStarAnimatedTile;
    public Tile moonTile;

    void Start()
    {
        for (int i = - distanceDestroy; i < distanceGenerate; i++)
        {
            GenerateClouds(i);
            GenerateStars(i);
        }
        for (int i = -(distanceDestroy * 8 / 6); i < (distanceGenerate * 8 / 6); i++)
        {
            //GenerateFarMountains(i);
            //GenerateNearMountains(i);
            GenerateMountains(i);
        }
        GenerateMoon();
    }

    void Update()
    {

        float xMountains = playerTransform.position.x * 0.6f;
        float xClouds = playerTransform.position.x * 0.9f;

  

        mountainsTilemap.transform.position = new Vector3(xMountains, mountainsTilemap.transform.position.y, mountainsTilemap.transform.position.z);
        //nearMountainsTilemap.transform.position = new Vector3(playerTransform.position.x / 2f, nearMountainsTilemap.transform.position.y, nearMountainsTilemap.transform.position.z);
        //farMountainsTilemap.transform.position = new Vector3(playerTransform.position.x / 1.5f, farMountainsTilemap.transform.position.y, farMountainsTilemap.transform.position.z);
        cloudsTilemap.transform.position = new Vector3(xClouds, cloudsTilemap.transform.position.y, cloudsTilemap.transform.position.z);
        //nearMountainsTilemap.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x / 1.25f, 0);
        //farMountainsTilemap.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x / 1.125f, 0);

        // GENERAR TERRENO ALEATORIAMENTE
        int playerPosition8 = (int)(playerTransform.position.x / 8f);
        if (!rockTilemap.HasTile(new Vector3Int(playerPosition8 + distanceGenerate, -10, 0)))
        {
            GenerateGround(playerPosition8);
        }

        // GENERAR FONDO MONTAÑAS ALEATOREAMENTE
        int playerPosition6 = (int)(playerTransform.position.x / 6f * 0.4f );
        if (!mountainsTilemap.HasTile(new Vector3Int(playerPosition6 + (distanceGenerate * 8 / 6), -15, 0)))
        {
            GenerateMountains(playerPosition6 + (distanceGenerate * 8 / 6));
        }

        //// GENERAR FONDO CERCANO ALEATOREAMENTE
        //int playerPosition6 = (int)(playerTransform.position.x / 6f / 2f);
        //if (!nearMountainsTilemap.HasTile(new Vector3Int(playerPosition6 + distanceGenerate, -15, 0)))
        //{
        //    GenerateNearMountains(playerPosition6 + distanceGenerate);
        //}

        //// GENERAR FONDO LEJANO ALEATOREAMENTE
        //int playerPosition4 = (int)(playerTransform.position.x / 4f / 1.5f);
        //if (!farMountainsTilemap.HasTile(new Vector3Int(playerPosition4 + distanceGenerate, -23, 0)))
        //{
        //    GenerateFarMountains(playerPosition4 + distanceGenerate);
        //}

        // GENERAR NUBES ALEATOREAMENTE
        int playerPositionClouds = (int)(playerTransform.position.x / 8f * 0.1f);
        if (!cloudsTilemap.HasTile(new Vector3Int(playerPositionClouds + distanceGenerate, cloudsHeigh, 0)))
        {
            GenerateClouds(playerPositionClouds + distanceGenerate);
        }

        // ELIMINAR TILES DEJADOS ATRAS
        for (int i = 1; i <= 20; i++)
        {
            Vector3Int tilePosition = new Vector3Int(playerPosition8 - distanceDestroy, i - 14, 0);

            if (rockTilemap.HasTile(tilePosition))
            {
                rockTilemap.SetTile(tilePosition, null);
            }
            if (groundTilemap.HasTile(tilePosition))
            {
                groundTilemap.SetTile(tilePosition, null);
            }
            if (mineralTilemap.HasTile(tilePosition))
            {
                mineralTilemap.SetTile(tilePosition, null);
            }
            if (grassTilemap.HasTile(tilePosition))
            {
                grassTilemap.SetTile(tilePosition, null);
            }

            Vector3Int tilePositionMountains = new Vector3Int(playerPosition6 - (distanceDestroy * 8 / 6), i - 16, 0);
            if (mountainsTilemap.HasTile(tilePositionMountains))
            {
                mountainsTilemap.SetTile(tilePositionMountains, null);
            }

            Vector3Int tilePositionClouds = new Vector3Int(playerPositionClouds - distanceDestroy, i - 14, 0);
            if (cloudsTilemap.HasTile(tilePositionClouds))
            {
                cloudsTilemap.SetTile(tilePositionClouds, null);
            }
        }
    }

    void FixedUpdate()
    {
       
    }

    private void GenerateClouds(int positionX)
    {
        cloudsTilemap.SetTile(new Vector3Int(positionX, cloudsHeigh, 0), cloudsRandomTile);
    }

    private void GenerateStars(int positionX)
    {
        for (int i = 0; i <= 9; i++)
        {
            int probabilityStars = Random.Range(0, 99);

            if (probabilityStars < 8)
            {
                starsTilemap.SetTile(new Vector3Int(positionX, i, 0), starsRuleTile);
            }
            else if (probabilityStars < 14) 
            {
                
                starsTilemap.SetTile(new Vector3Int(positionX, i, 0), smallStarAnimatedTile);
            }
            else if (probabilityStars < 16)
            {
                starsTilemap.SetTile(new Vector3Int(positionX, i, 0), bigStarAnimatedTile);
            }
        }
    }

    private void GenerateMoon()
    {
        int positionX = Random.Range(-20, 20);
        int positionY = Random.Range(0, 10);
        starsTilemap.SetTile(new Vector3Int(positionX, positionY, 0), moonTile);
    }

    private void GenerateFarMountains(int positionX)
    {
        int modFarMountainsLevel;

        // BAJAR PROBABILIDAD DE CAMBIO DE NIVEL DEL TERRENO
        //  10
        //  20
        //  30
        //  50
        //  80
        // 130
        // 320

        int probabilityFarMountains = Random.Range(0, 320);

        if (probabilityFarMountains < 10)
        {
            modFarMountainsLevel = 5;
        }
        else if (probabilityFarMountains < 30)
        {
            modFarMountainsLevel = 4;
        }
        else if (probabilityFarMountains < 60)
        {
            modFarMountainsLevel = 3;
        }
        else if (probabilityFarMountains < 110)
        {
            modFarMountainsLevel = 2;
        }
        else if (probabilityFarMountains < 190)
        {
            modFarMountainsLevel = 1;
        }
        else
        {
            modFarMountainsLevel = 0;
        }

        if (probabilityFarMountains % 2 == 0)
        {
            modFarMountainsLevel = -modFarMountainsLevel;
        }

        farMountainsLevel += modFarMountainsLevel;
        if (farMountainsLevel > maxFarMountainsLevel)
        {
            farMountainsLevel = maxFarMountainsLevel;
        }
        else if (farMountainsLevel < minFarMountainsLevel)
        {
            farMountainsLevel = minFarMountainsLevel;
        }

        for (int i = 1; i <= farMountainsLevel; i++)
        {
            int cota = i - 24;

            farMountainsTilemap.SetTile(new Vector3Int(positionX, cota, 0), farMountainsRuleTile);

        }
    }

    private void GenerateNearMountains(int positionX)
    {
        int modNearMountainsLevel;

        // BAJAR PROBABILIDAD DE CAMBIO DE NIVEL DEL TERRENO
        //  10
        //  20
        //  30
        //  50
        //  80
        // 130
        // 320

        int probabilityNearMountains = Random.Range(0, 320);

        if (probabilityNearMountains < 12)
        {
            modNearMountainsLevel = 5;
        }
        else if (probabilityNearMountains < 30)
        {
            modNearMountainsLevel = 4;
        }
        else if (probabilityNearMountains < 60)
        {
            modNearMountainsLevel = 3;
        }
        else if (probabilityNearMountains < 110)
        {
            modNearMountainsLevel = 2;
        }
        else if (probabilityNearMountains < 190)
        {
            modNearMountainsLevel = 1;
        }
        else
        {
            modNearMountainsLevel = 0;
        }

        if (probabilityNearMountains % 2 == 0)
        {
            modNearMountainsLevel = -modNearMountainsLevel;
        }

        nearMountainsLevel += modNearMountainsLevel;
        if (nearMountainsLevel > maxNearMountainsLevel)
        {
            nearMountainsLevel = maxNearMountainsLevel;
        }
        else if (nearMountainsLevel < minNearMountainsLevel)
        {
            nearMountainsLevel = minNearMountainsLevel;
        }

        for (int i = 1; i <= nearMountainsLevel; i++)
        {
            int cota = i - 16;
            nearMountainsTilemap.SetTile(new Vector3Int(positionX, cota, 0), nearMountainsRuleTile);
        }
    }

    private void GenerateMountains(int positionX)
    {
        int modMountainsLevel;

        // BAJAR PROBABILIDAD DE CAMBIO DE NIVEL DEL TERRENO
        //  10
        //  20
        //  30
        //  50
        //  80
        // 130
        // 320

        int probabilityMountains = Random.Range(0, 320);

        if (probabilityMountains < 12)
        {
            modMountainsLevel = 5;
        }
        else if (probabilityMountains < 30)
        {
            modMountainsLevel = 4;
        }
        else if (probabilityMountains < 60)
        {
            modMountainsLevel = 3;
        }
        else if (probabilityMountains < 110)
        {
            modMountainsLevel = 2;
        }
        else if (probabilityMountains < 190)
        {
            modMountainsLevel = 1;
        }
        else
        {
            modMountainsLevel = 0;
        }

        if (probabilityMountains % 2 == 0)
        {
            modMountainsLevel = -modMountainsLevel;
        }

        mountainsLevel += modMountainsLevel;
        if (mountainsLevel > maxMountainsLevel)
        {
            mountainsLevel = maxMountainsLevel;
        }
        else if (mountainsLevel < minMountainsLevel)
        {
            mountainsLevel = minMountainsLevel;
        }

        for (int i = 1; i <= mountainsLevel; i++)
        {
            int cota = i - 16;
            mountainsTilemap.SetTile(new Vector3Int(positionX, cota, 0), mountainsRuleTile);
        }
    }

    private void GenerateGround(int positionX)
    {
        int modRockLevel = Random.Range(-1, 2);
        int modGroundLevel = Random.Range(-2, 3);

        // BAJAR PROBABILIDAD DE CAMBIO DE NIVEL DEL TERRENO
        if (Random.Range(0, 1000) > positionX)
        {
            modGroundLevel = 0;
        }
        if (Random.Range(0, 1000) > positionX)
        {
            modRockLevel = 0;
        }

        rockLevel = rockLevel + modRockLevel;
        if (rockLevel > maxRockLevel)
        {
            rockLevel = maxRockLevel;
        }
        else if (rockLevel < minRockLevel)
        {
            rockLevel = minRockLevel;
        }

        groundLevel = groundLevel + modGroundLevel;
        if (groundLevel > maxGroundLevel)
        {
            groundLevel = maxGroundLevel;
        }
        else if (groundLevel < rockLevel + 1)
        {
            groundLevel = rockLevel + 1;
        }

        for (int i = 1; i <= groundLevel; i++)
        {
            int cota = i - 14;

            if (i <= rockLevel)
            {
                rockTilemap.SetTile(new Vector3Int(positionX + distanceGenerate, cota, 0), rockRuleTile);
                groundTilemap.SetTile(new Vector3Int(positionX + distanceGenerate, cota, 0), groundRuleTile);
            }
            else
            {
                groundTilemap.SetTile(new Vector3Int(positionX + distanceGenerate, cota, 0), groundRuleTile);

                if (Random.Range(1, 100) <= mineralChance)
                {
                    mineralTilemap.SetTile(new Vector3Int(positionX + distanceGenerate, cota, 0), buriedMineralRandomTile);
                }
            }
        }
        grassTilemap.SetTile(new Vector3Int(positionX + distanceGenerate, groundLevel - 13, 0), grassRandomTile);
    }

    //private RuleTile dameRockRuleTile()
    //{
    //    if (Random.Range(1, 3) == 1)
    //    {
    //        return this.rock1RuleTile;
    //    }
    //    else
    //    {
    //        return this.rock2RuleTile;
    //    }
    //}
}
