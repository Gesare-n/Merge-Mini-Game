using UnityEngine;
using UnityEngine.SceneManagement;
public class DailyRewards : MonoBehaviour
{
    public int lastDay;

    public int day_1;
    public int day_2;
    public int day_3;
    public int day_4;
    public int day_5;
    public int day_6;
    public int day_7;

    public GameObject off_1;
    public GameObject active_1;
    public GameObject check_1;

    public GameObject off_2;
    public GameObject active_2;
    public GameObject check_2;

    public GameObject off_3;
    public GameObject active_3;
    public GameObject check_3;

    public GameObject off_4;
    public GameObject active_4;
    public GameObject check_4;

    public GameObject off_5;
    public GameObject active_5;
    public GameObject check_5;

    public GameObject off_6;
    public GameObject active_6;
    public GameObject check_6;

    public GameObject off_7;
    public GameObject active_7;
    public GameObject check_7;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        day_1 = PlayerPrefs.GetInt("Day_1");
        day_2 = PlayerPrefs.GetInt("Day_2");
        day_3 = PlayerPrefs.GetInt("Day_3");
        day_4 = PlayerPrefs.GetInt("Day_3");
        day_5 = PlayerPrefs.GetInt("Day_3");
        day_6 = PlayerPrefs.GetInt("Day_3");
        day_7 = PlayerPrefs.GetInt("Day_3");

        lastDay = PlayerPrefs.GetInt("LastDate");

        Reward();

        if(lastDay != System.DateTime.Now.Day)
        {
            if (day_1 == 0)
            {
                day_1 = 1;
            } 
            else if (day_2 == 0)
            {
                day_2 = 1;
            }
            else if (day_3 == 0)
            {
                day_3 = 1;
            }
            else if (day_4 == 0)
            {
                day_4 = 1;
            }
            else if (day_5 == 0)
            {
                day_5 = 1;
            }
            else if (day_6 == 0)
            {
                day_6 = 1;
            }
            else if (day_7 == 0)
            {
                day_7 = 1;
            }
            Reward();
        }
    }


    public void Reward()
    {
        if (day_1 == 0)
        {
            off_1.SetActive(true);
            active_1.SetActive(false);
            check_1.SetActive(false);
        }
        if (day_1 == 1)
        {
            off_1.SetActive(false);
            active_1.SetActive(true);
            check_1.SetActive(false);
        } 
        if (day_1 == 2)
        {
            off_1.SetActive(false);
            active_1.SetActive(false);
            check_1.SetActive(true);
        }

        if (day_2 == 0)
        {
            off_2.SetActive(true);
            active_2.SetActive(false);
            check_2.SetActive(false);
        }
        if (day_2 == 1)
        {
            off_2.SetActive(false);
            active_2.SetActive(true);
            check_2.SetActive(false);
        }
        if (day_2 == 2)
        {
            off_2.SetActive(false);
            active_2.SetActive(false);
            check_2.SetActive(true);
        }

        if (day_3 == 0)
        {
            off_3.SetActive(true);
            active_3.SetActive(false);
            check_3.SetActive(false);
        }
        if (day_3 == 1)
        {
            off_3.SetActive(false);
            active_3.SetActive(true);
            check_3.SetActive(false);
        }
        if (day_3 == 2)
        {
            off_3.SetActive(false);
            active_3.SetActive(false);
            check_3.SetActive(true);
        }

        if (day_4 == 0)
        {
            off_4.SetActive(true);
            active_4.SetActive(false);
            check_4.SetActive(false);
        }
        if (day_4 == 1)
        {
            off_4.SetActive(false);
            active_4.SetActive(true);
            check_4.SetActive(false);
        }
        if (day_4 == 2)
        {
            off_4.SetActive(false);
            active_4.SetActive(false);
            check_4.SetActive(true);
        }

        if (day_5 == 0)
        {
            off_5.SetActive(true);
            active_5.SetActive(false);
            check_5.SetActive(false);
        }
        if (day_5 == 1)
        {
            off_5.SetActive(false);
            active_5.SetActive(true);
            check_5.SetActive(false);
        }
        if (day_5 == 2)
        {
            off_5.SetActive(false);
            active_5.SetActive(false);
            check_5.SetActive(true);
        }

        if (day_6 == 0)
        {
            off_6.SetActive(true);
            active_6.SetActive(false);
            check_6.SetActive(false);
        }
        if (day_6 == 1)
        {
            off_6.SetActive(false);
            active_6.SetActive(true);
            check_6.SetActive(false);
        }
        if (day_6 == 2)
        {
            off_6.SetActive(false);
            active_6.SetActive(false);
            check_6.SetActive(true);
        }

        if (day_7 == 0)
        {
            off_7.SetActive(true);
            active_7.SetActive(false);
            check_7.SetActive(false);
        }
        if (day_7 == 1)
        {
            off_7.SetActive(false);
            active_7.SetActive(true);
            check_7.SetActive(false);
        }
        if (day_7 == 2)
        {
            off_7.SetActive(false);
            active_7.SetActive(false);
            check_7.SetActive(true);
        }
    }

    public void GetReward()
    {
        lastDay = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", lastDay);

        if (active_1.activeSelf == true)
        {
            print("Reward 1");
            day_1 = 2;
            PlayerPrefs.SetInt("Day_1", day_1);
        }
        else if (active_2.activeSelf == true)
        {
            print("Reward 2");
            day_2 = 2;
            PlayerPrefs.SetInt("Day_2", day_2);
        }
        else if (active_3.activeSelf == true)
        {
            print("Reward 3");
            day_3 = 2;
            PlayerPrefs.SetInt("Day_3", day_3);
        }
        else if (active_4.activeSelf == true)
        {
            print("Reward 4");
            day_4 = 2;
            PlayerPrefs.SetInt("Day_4", day_4);
        }
        else if (active_5.activeSelf == true)
        {
            print("Reward 5");
            day_5 = 2;
            PlayerPrefs.SetInt("Day_5", day_5);
        }
        else if (active_6.activeSelf == true)
        {
            print("Reward 6");
            day_6 = 2;
            PlayerPrefs.SetInt("Day_6", day_6);
        }
        else if (active_7.activeSelf == true)
        {
            print("Reward 7");
            day_7 = 2;
            PlayerPrefs.SetInt("Day_7", day_7);
        }

        Reward();
    }

    public void ResetRewards()
    {
        lastDay = System.DateTime.Now.Day;
        PlayerPrefs.SetInt("LastDate", lastDay);

        print("Rewards reset successfully!");

        day_1 = 1;
        PlayerPrefs.SetInt("Day_1", day_1);

        Reward();
    }

    public void ReturnToGame()
    {
        SceneManager.LoadScene("GameScene");
    }


}
