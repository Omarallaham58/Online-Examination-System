using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectV1
{
    public class Constants
    {
        public  const  string ADMIN_USERNAME = "admin";
        public const string ADMIN_PASSWORD = "admin";
        //create an SQL database and put your connection string here
        public const string DB_CONNECTION_STRING = "YOUR DB_CONNECTION_STRING";
        /*  public const int ADMIN_ROLE = 1;
          public const int TEACHER_ROLE = 2;
          public const int STUDENT_ROLE = 3;
          public static void authenticate(int role)
          {

          }*/

        /* public static string shuffle(string str)
         {
             string shuffled= new string(str.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
             return shuffled;
         }*/
        /* public static string Shuffle(String str)
         {

             int n = str.Length;
             char[] s = str.ToCharArray();
             //String res="";
             char[] res = new char[n];
             int l = 0;
             int[] selected_indices = new int[n];
             int nb = 0;
             while (l < n)
             {
                 Random rand = new Random();
                 //int randomIndex = rand.nextInt((n - 1) + 1);
                 int randomIndex=rand.Next((n - 1) + 1);
                 if (In_array(randomIndex, selected_indices) == false)
                 {
                     res[l++] = s[randomIndex];
                     selected_indices[nb++] = randomIndex;
                 }
             }

             return new string(res,0,n);
         }

         public static bool In_array(int x, int[] arr)
         {

             for (int i = 0; i < arr.Length; i++)
             {
                 if (x == arr[i]) return true;
             }
             return false;
         }*/
        public static string Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                char value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return new string(array);
        }

    }
}