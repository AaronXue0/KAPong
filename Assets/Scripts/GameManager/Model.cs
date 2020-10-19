using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagerSpace
{
    public class Model
    {
        private Model()
        {
            score = 0;
        }
        private static Model model;
        public static Model Instance
        {
            get
            {
                model = new Model();
                return model;
            }
        }


        float score = 0;
        public float Score { set { score += value; } get { return score; } }

        public Vector2 InstantiatePlace(int mode)
        {
            Vector2 randomPlace;
            float randomX = 0;
            float randomY = 0;
            if (mode == 1)
            {
                bool useable = false;
                randomX = Random.Range(-12.5f, 11.5f);
                randomY = Random.Range(-6.5f, 8.5f);
                while (useable == false)
                {
                    if ((randomX < 11 && randomX > -11.7) && (randomY < 8 && randomY > -6))
                    {
                        randomX = Random.Range(-12.5f, 11.5f);
                        randomY = Random.Range(-6.5f, 8.5f);
                    }
                    else useable = true;
                }
            }
            else if (mode == 2)
            {
                randomX = Random.Range(-10.5f, 10f);
                randomY = Random.Range(-6f, 6f);
            }

            randomPlace = new Vector2(randomX, randomY);
            return randomPlace;
        }

        //  public void InstantiateTrap()
        // {
        //     int which;
        //     int bombNum;
        //     which = Random.Range(1, 3);
        //     bombNum = Random.Range(4, 7);
        //     switch (which)
        //     {
        //         case 1:
        //             for (int i = 0; i < bombNum; i++)
        //                 Instantiate(bomb, InstantiatePlace(2), new Quaternion());
        //             break;
        //         case 2:
        //             for (int i = 0; i < bombNum; i++)
        //                 Instantiate(bomb, InstantiatePlace(2), new Quaternion());
        //             break;
        //         case 3:
        //             for (int i = 0; i < bombNum; i++)
        //                 Instantiate(bomb, InstantiatePlace(2), new Quaternion());
        //             break;
        //     }

        // }
    }
}