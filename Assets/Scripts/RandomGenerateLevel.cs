using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerateLvlScript : MonoBehaviour
{
    [SerializeField] Transform Targets;
    [Range(-30,30)] public int TargetRL;
    [Range(2,5)] public int TargetScaleCount;

    [SerializeField] Transform Ramp;
    [Range(-50, 50)] public int RampRL;
    [Range(-60, -80)] public int RampDistanse;
    [Range(2, 10)] public int RampScaleCount;

    [SerializeField] Transform Sklon;
    [Range(10,30)] public int SklonRotate;
    [Range(70,150)] public int SklonScaleX;

    [SerializeField] Transform SnowBoard;
    [Range(-30, 30)] public int SnowBoardDistanceRL;
    [Range(-70, 0)] public int SnowBoardDistance;

    [SerializeField] GameObject MiniWall;
    [Range(0, 20)] public int BlockCount;
    [SerializeField] GameObject[] BlockObjects;
    [SerializeField] Transform DecorSklon;
    
    // Start is called before the first frame update
    private void Awake()
    {
        //GenerateMap();
        GenerateRandomMap();
    }

    private void Start()
    {
        GlobalEventScript.SendRestartGame();
    }
    private void GenerateMap()
    {

        // Трамплин
        Ramp.position = new Vector3(RampRL, Ramp.position.y, RampDistanse); // Положение левее/правее и расстояние от мишени
        Ramp.localScale = new Vector3(RampScaleCount, RampScaleCount, RampScaleCount); // Множитель размера


        Sklon.localScale = new Vector3(SklonScaleX, 1, Sklon.localScale.z); // Ширина склона

        //Генерация минизаборчиков по перименту (0.5 от края)
        for (int i = 0; i <= 20; i++)
        {
            Instantiate(MiniWall, Sklon.position + new Vector3(SklonScaleX / 2 - 2, 0.5f, i * 5), MiniWall.transform.rotation, DecorSklon);
            Instantiate(MiniWall, Sklon.position + new Vector3(-SklonScaleX / 2 + 2, 0.5f, i * 5), MiniWall.transform.rotation, DecorSklon);
            Instantiate(MiniWall, Sklon.position + new Vector3(SklonScaleX / 2 - 2, 0.5f, i * -5), MiniWall.transform.rotation, DecorSklon);
            Instantiate(MiniWall, Sklon.position + new Vector3(-SklonScaleX / 2 + 2, 0.5f, i * -5), MiniWall.transform.rotation, DecorSklon);
        }
        // Генераця препятствий
        for (int i = 0; i < BlockCount; i++)
        {
            float PosX = Sklon.position.x + Random.Range(-SklonScaleX / 4 + 5, SklonScaleX / 4 - 5);
            float PosY = Sklon.position.y + 0.5f;
            float PosZ = Random.Range(Sklon.position.z + SnowBoardDistance + 10, RampDistanse);
            Quaternion RotZ = BlockObjects[0].transform.rotation * Quaternion.Euler(0, Random.Range(-180f, 180f), 0);
            Instantiate(BlockObjects[0], new Vector3(PosX, PosY, PosZ), RotZ, DecorSklon);
        }


        // Изменение позиции сноуборда право/лево ближе/дальше
        SnowBoard.position += new Vector3(SnowBoardDistanceRL, 0, SnowBoardDistance);


        // Привязка всех к декорклону для поворота
        SnowBoard.SetParent(DecorSklon.transform);
        Ramp.SetParent(DecorSklon.transform);

        //Поворот склона и декора
        DecorSklon.Rotate(Vector3.right * SklonRotate);
        Sklon.transform.Rotate(Vector3.right * SklonRotate);

        SnowBoard.SetParent(null);

        // Мишень для прыжка
        Targets.position += Vector3.right * TargetRL; // Положение левее/правее
        Targets.localScale = new Vector3(TargetScaleCount, 1, TargetScaleCount); // Множитель размера
        Targets.position = new Vector3(Targets.position.x, Ramp.position.y - (SklonRotate/2), Targets.position.z); // Поправка на трамплин и угол склона

    }
    private void GenerateRandomMap()
    {
        TargetRL = Random.Range(-30,30);
        TargetScaleCount = Random.Range(2, 5);

        RampRL = TargetRL + Random.Range(-15, 15);
        RampDistanse = Random.Range(-60, -80);
        RampScaleCount = Random.Range(2, 10);

        SklonRotate = Random.Range(10, 30);
        SklonScaleX = Random.Range(70, 150);

        SnowBoardDistanceRL = Random.Range(-30, 30);
        SnowBoardDistance = Random.Range(-70, 0);

        BlockCount = Random.Range(0, 20);

        GenerateMap();

    }
}
